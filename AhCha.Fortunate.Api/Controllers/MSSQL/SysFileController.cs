using Aliyun.OSS;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.Common.Global;
using System.Collections.Concurrent;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.Common.Extensions;

namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 文件相关
    /// 使用接口统一封装输出导致无法使用File进行下载，现已解决（2024-05-19）
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysFileController : BaseApiController
    {
        /// 阿里云oss上传下载，（需自己配置阿里云所需的配置）配置泄漏不归本人管理
        /// endpoint 
        /// accessKeyId 
        /// accessKeySecret 
        /// bucketName = bucket列表中的名称
        /// path = 存储路径


        /// <summary>
        /// 阿里云oss上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PutObjectResult PostOssUpload(IFormFile file)
        {
            PutObjectResult result = AliyunOssUtil.OssUpload(file.FileName, file.OpenReadStream());
            return result;
        }

        /// <summary>
        /// 阿里云oss文件下载
        /// </summary>
        /// <param name="objectName">oss根据文件名称来下载</param>
        /// <returns></returns>
        [HttpGet("/DowOssFile")]
        public FileStreamResult DownLoadOssFile(string objectName)
        {
            OssObject? obj = AliyunOssUtil.DownloadOssFile(objectName);
            return File(obj.Content, "application/octet-stream", obj.Key);
        }

        /// <summary>
        /// 文件上传输出绝对路径
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> PostFileUpload(IFormFile file)
        {
            if (file.Length > AhChaFortunateGlobalContext.DirectoryConfig.limitSiza)
            {
                string filecontent = Convert.ToString(((float)file.Length) / 1024 / 1024)[..4];
                throw new Exception($"文件大小超出限制（{AhChaFortunateGlobalContext.DirectoryConfig.FileMax}）当前 {filecontent} M");
            }
            string Name = Guid.NewGuid().ToString("N");
            //返回绝对路径
            return await Task.FromResult(FileUtil.SaveBusinessAttachment(file, Name));
        }

        /// <summary>
        /// 创建一个Execl
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string CreateExecl()
        {
            string SaveUrl = Path.Combine(FileUtil.GetSystemDirectory, "CreateExecl");
            FileUtil.CreateDirectory(SaveUrl);
            string path = Path.Combine(SaveUrl, string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".xls"));
            HSSFWorkbook workbook = new HSSFWorkbook();
            FileStream filestream = new FileStream(path, FileMode.Create);
            workbook.CreateSheet("测试 A");
            workbook.CreateSheet("测试 B");
            workbook.CreateSheet("测试 C");
            workbook.Write(filestream);
            workbook.Close();
            filestream.Close();
            filestream.Dispose();
            return "创建成功";
        }

        /// <summary>
        /// 创建Execl并写入内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string CreateExeclWriteText()
        {
            string SaveUrl = Path.Combine(FileUtil.GetSystemDirectory, "CreateExecl");
            FileUtil.CreateDirectory(SaveUrl);
            string path = Path.Combine(SaveUrl, string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), ".xls"));
            HSSFWorkbook workbook = new HSSFWorkbook();
            FileStream filestream = new FileStream(path, FileMode.Create);
            // 测试。 
            ISheet sheet1 = workbook.CreateSheet("测试 A");
            //依次创建行和列
            for (int i = 0; i < 10; i++)
            {
                IRow row1 = sheet1.CreateRow(i);
                for (int j = 0; j < 10; j++)
                {
                    ICell cell1 = row1.CreateCell(j);
                    cell1.SetCellValue("第" + (i + 1) + "行，第" + (j + 1) + "列");
                }
            }
            workbook.Write(filestream);
            workbook.Close();

            filestream.Close();
            filestream.Dispose();
            return "数据添加成功";
        }

        /// <summary>
        /// 下载导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult DownLoadExcelTemplate()
        {
            FileStream file = new FileStream(Path.Combine(FileUtil.GetSystemDirectory, "Template", "Template.xls"), FileMode.Open, FileAccess.Read);//读入excel模板
            HSSFWorkbook book = new HSSFWorkbook(file);
            System.IO.MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            string dt = DateTime.Now.ToYear();
            string filename = "导入数据的excel模板" + dt + ".xls";
            return File(ms, "application/vns.ms-excel", filename);
        }

        /// <summary>
        /// 导入Execl
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost]
        public Task<string> ImportExecl(IFormFile file)
        {
            string fileExt = Path.GetExtension(file.FileName).ToLower();
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            IWorkbook book;
            if (fileExt == ".xlsx")
            {
                book = new XSSFWorkbook(ms);
            }
            else if (fileExt == ".xls")
            {
                book = new HSSFWorkbook(ms);
            }
            else
            {
                throw new Exception("文件类型不支持，仅支持【.xls、.xlsx】");
            }
            ISheet sheet = book.GetSheetAt(0);
            int CountRow = sheet.LastRowNum + 1;//获取总行数
            if (CountRow - 1 == 0)
            {
                throw new Exception("Excel列表数据项为空");
            }
            for (int i = 1; i < CountRow; i++)
            {
                //实例化实体对象
                IRow? row = sheet.GetRow(i);
                short index = row.LastCellNum;//总列数
                string Value1 = row.GetCell(0).GetCellValue();
                string Value2 = row.GetCell(1).GetCellValue();
                string Value3 = row.GetCell(2).GetCellValue();
                string Value4 = row.GetCell(3).GetCellValue();
                string Value5 = row.GetCell(4).GetCellValue();
                string Value6 = row.GetCell(5).GetCellValue();
                string Value7 = row.GetCell(6).GetCellValue();
                string Value8 = row.GetCell(7).GetCellValue();
                string Value9 = row.GetCell(8).GetCellValue();
                string Value10 = row.GetCell(9).GetCellValue();
            }
            return Task.FromResult($"数据导入成功,共导入{CountRow - 1}条数据。");

        }

        /// <summary>
        /// 导出Execl
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult ExportExecl()
        {
            HSSFWorkbook excelBook = new HSSFWorkbook();
            //创建Excel工作表
            ISheet sheet1 = excelBook.CreateSheet("测试导出");
            //给Sheet(头）添加第一行的头部标题
            IRow row1 = sheet1.CreateRow(0);

            row1.CreateCell(0).SetCellValue("标题头1");
            row1.CreateCell(1).SetCellValue("标题头2");
            row1.CreateCell(2).SetCellValue("标题头3");
            row1.CreateCell(3).SetCellValue("标题头4");
            row1.CreateCell(4).SetCellValue("标题头5");
            row1.CreateCell(5).SetCellValue("标题头6");
            row1.CreateCell(6).SetCellValue("标题头7");
            row1.CreateCell(7).SetCellValue("标题头8");
            row1.CreateCell(8).SetCellValue("标题头9");
            row1.CreateCell(9).SetCellValue("标题头10");
            //添加数据行：将表格数据逐步写入sheet1各个行中（也就是给每一个单元格赋值）
            for (int i = 0; i < 10; i++)
            {
                //sheet1.CreateRow(i).
                //创建行
                IRow rowTemp = sheet1.CreateRow(i + 1);
                rowTemp.CreateCell(0).SetCellValue("阿奎请问收asd 到123" + i);
                rowTemp.CreateCell(1).SetCellValue("按时发放铁体育 请问奎收请问 到123" + i);
                rowTemp.CreateCell(2).SetCellValue("按时发放铁体育 该语句奎收请问请问到123" + i);
                rowTemp.CreateCell(3).SetCellValue("按时发阿萨体育 该语句奎收到请问asd123" + i);
                rowTemp.CreateCell(4).SetCellValue("按时发阿萨体育 该语句青蛙奎收到123" + i);
                rowTemp.CreateCell(5).SetCellValue("按时发阿萨体育 该语句奎请问请问收到123" + i);
                rowTemp.CreateCell(6).SetCellValue("按时发阿萨体育 78的人奎收到123" + i);
                rowTemp.CreateCell(7).SetCellValue("按时发放铁体育 ui奎sz请问afd 收到123" + i);
                rowTemp.CreateCell(8).SetCellValue("阿奎收请问到123" + i);
                rowTemp.CreateCell(9).SetCellValue("fdtghrtb awdwqaRAW" + i);
            }
            //输出的文件名称
            string fileName = "导出信息" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xls";
            //把Excel转为流，输出
            //创建文件流
            MemoryStream bookStream = new MemoryStream();
            //将工作薄写入文件流
            excelBook.Write(bookStream);
            //输出之前调用Seek（偏移量，游标位置) 把0位置指定为开始位置
            bookStream.Seek(0, System.IO.SeekOrigin.Begin);
            //Stream对象,文件类型,文件名称
            return File(bookStream, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// 获取二维码（Base64）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetQrCode(string input)
        {
            return QRCodeUtil.SaveQrCode(input);
            //return QRCodeUtil.BitmapToBase64(QRCodeUtil.CreateQrCode(input));
        }

        /// <summary>
        /// Execl文件上传，后在导出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<FileResult> FileUploadExport(IFormFile file)
        {
            string fileExt = Path.GetExtension(file.FileName).ToLower();
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);
            IWorkbook book;
            if (fileExt == ".xlsx")
            {
                book = new XSSFWorkbook(ms);
            }
            else if (fileExt == ".xls")
            {
                book = new HSSFWorkbook(ms);
            }
            else
            {
                throw new Exception("文件类型不支持，仅支持【.xls、.xlsx】");
            }
            ISheet sheet = book.GetSheetAt(0);
            int CountRow = sheet.LastRowNum + 1;//获取总行数
            if (CountRow - 1 == 0)
            {
                throw new Exception("Excel列表数据项为空");
            }

            #region 开启多线程
            var stringBag = new ConcurrentBag<string>();
            List<Task> tasks = new List<Task>();
            List<string> ctest = new List<string>();
            Random random = new Random();
            //创建模拟数据
            for (int i = 0; i < 1000; i++)
            {
                ctest.Add(string.Concat(i, random.Next(100, 999)));
            }
            int size = 100; int taskCount = (ctest.Count() + size - 1) / size;
            for (int i = 0; i < taskCount; i++)
            {//动态开启线程
                string taskId = i.ToString();
                //给线程分配数据
                var taskData = ctest.Skip(i * size).Take(size).ToList();
                tasks.Add(Task.Run(async () =>
                {
                    //线程数据合并
                    taskData.ForEach(item => stringBag.Add(GetTaskName(taskId, item)));
                }));
            }
            //等待线程全部完成
            await Task.WhenAll(tasks);
            #endregion

            var data = stringBag.ToList();
            HSSFWorkbook excelBook = new HSSFWorkbook();
            ISheet sheet1 = excelBook.CreateSheet("数据导出");
            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("ICCID");
            for (int index = 0; index < data.Count; index++)
            {
                IRow rowTemp = sheet1.CreateRow(index + 1);
                rowTemp.CreateCell(0).SetCellValue(data[index]);
            }
            string fileName = "导出信息.xls";
            MemoryStream bookStream = new MemoryStream();
            excelBook.Write(bookStream);
            bookStream.Seek(0, System.IO.SeekOrigin.Begin);
            return File(bookStream, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// 读取本地Execl数据，然后导出在本地路径
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task LocalhostExport() {

            var stringBag = new ConcurrentBag<string>();
            IWorkbook workbook;
            using (FileStream file = new FileStream("D:\\模板.xls", FileMode.Open, FileAccess.Read))
            {
                workbook = new HSSFWorkbook(file);
                ISheet sheet = workbook.GetSheetAt(0);
                int CountRow = sheet.LastRowNum + 1;

                #region 开启多线程
                List<Task> tasks = new List<Task>();
                List<string> ctest = new List<string>();
                Random random = new Random();
                //创建模拟数据
                for (int i = 0; i < 1000; i++)
                {
                    ctest.Add(string.Concat(i, random.Next(100, 999)));
                }
                int size = 100; int taskCount = (ctest.Count() + size - 1) / size;
                for (int i = 0; i < taskCount; i++)
                {//动态开启线程
                    string taskId = i.ToString();
                    //给线程分配数据
                    var taskData = ctest.Skip(i * size).Take(size).ToList();
                    tasks.Add(Task.Run(async () =>
                    {
                        //线程数据合并
                        taskData.ForEach(item => stringBag.Add(GetTaskName(taskId, item)));
                    }));
                }
                //等待线程全部完成
                await Task.WhenAll(tasks);
                #endregion
            }

            HSSFWorkbook excelBook = new HSSFWorkbook();
            ISheet sheet1 = excelBook.CreateSheet("测试导出");
            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(1).SetCellValue("文本");
            var dics = stringBag.ToList();
            for (int index = 0; index < dics.Count; index++)
            {
                IRow rowTemp = sheet1.CreateRow(index + 1);
                rowTemp.CreateCell(0).SetCellValue(dics[index]);
            }
            string fileName = "D:\\本地数据导出.xls";
            using (MemoryStream bookStream = new MemoryStream())
            {
                excelBook.Write(bookStream);
                bookStream.Seek(0, SeekOrigin.Begin);
                using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    bookStream.CopyTo(file);
                }
            }

        }

    }
}
