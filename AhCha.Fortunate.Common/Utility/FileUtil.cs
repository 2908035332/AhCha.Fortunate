﻿using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Common.Utility
{
    public class FileUtil
    {
        /// <summary>
        /// 获取系统根目录,仅获取到wwwroot目录下
        /// </summary>
        public static string GetSystemDirectory
        {
            get
            {
                string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (BaseDirectory.Contains("bin"))
                {
                    BaseDirectory = BaseDirectory.Remove(BaseDirectory.IndexOf("bin"));
                }
                return Path.Combine(BaseDirectory, "wwwroot");
            }
        }

        #region 检测指定目录是否存在

        /// <summary> 
        /// 检测指定目录是否存在 
        /// </summary> 
        /// <param name="directoryPath">目录的绝对路径</param>         
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        #endregion

        #region 检测指定文件是否存在 

        /// <summary> 
        /// 检测指定文件是否存在 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        #endregion

        #region 检测指定目录是否为空 

        /// <summary> 
        /// 检测指定目录是否为空 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //判断是否存在文件 
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    return false;
                }

                //判断是否存在文件夹 
                string[] directoryNames = GetDirectories(directoryPath);
                if (directoryNames.Length > 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        #endregion

        #region 检测目录中是否存在指定文件

        /// <summary> 
        /// 检测目录中是否存在指定文件
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。范例："Log*.xml"表示搜索所有以Log开头的Xml文件</param> 
        public static bool Contains(string directoryPath, string searchPattern)
        {
            try
            {
                //获取指定的文件列表 
                string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

                //判断指定文件是否存在 
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary> 
        /// 检测指定目录中是否存在指定的文件 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>  
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定的文件列表 
                string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

                //判断指定文件是否存在 
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region 创建目录

        /// <summary> 
        /// 创建目录 
        /// </summary> 
        /// <param name="directoryPath">目录的绝对路径</param> 
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录 
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        #endregion

        #region 创建文件 

        /// <summary> 
        /// 创建文件
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件 
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象 
                    FileInfo file = new FileInfo(filePath);

                    //创建文件 
                    FileStream fs = file.Create();

                    //关闭文件流 
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary> 
        /// 创建并写入文件
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="buffer">二进制流数据</param> 
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件 
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象 
                    FileInfo file = new FileInfo(filePath);

                    //创建文件 
                    FileStream fs = file.Create();

                    //写入二进制流 
                    fs.Write(buffer, 0, buffer.Length);

                    //关闭文件流 
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="obj">需要存储的内容</param>
        /// <param name="path">保存路径</param>
        /// <param name="filename">文件名称</param>
        public static void CreatedFile(object obj, string path, string filename)
        {
            if (!string.IsNullOrEmpty(path) && IsExistDirectory(path))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write))
                {
                    byte[] data = Encoding.Default.GetBytes(string.Concat(JsonConvert.SerializeObject(obj), "\r\n"));
                    fs.Position = fs.Length;
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }

        }

        #endregion

        #region 获取文件行数

        /// <summary> 
        /// 获取文件行数 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中 
            string[] rows = File.ReadAllLines(filePath);

            //返回行数 
            return rows.Length;
        }

        #endregion

        #region 获取文件大小

        /// <summary> 
        /// 获取文件大小
        /// 单位为Byte 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static int GetFileSize(string filePath)
        {
            //创建一个文件对象 
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小 
            return (int)fi.Length;
        }

        /// <summary> 
        /// 获取文件大小
        /// 单位为KB 
        /// </summary> 
        /// <param name="filePath">文件的路径</param>         
        public static double GetFileSizeByKB(string filePath)
        {
            //创建一个文件对象 
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小 
            return Convert.ToDouble(Convert.ToDouble(fi.Length) / 1024);
        }

        /// <summary> 
        /// 获取文件大小
        /// 单位为MB 
        /// </summary> 
        /// <param name="filePath">文件的路径</param>         
        public static double GetFileSizeByMB(string filePath)
        {
            //创建一个文件对象 
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小 
            return Convert.ToDouble(Convert.ToDouble(fi.Length) / 1024 / 1024);
        }

        /// <summary> 
        /// 获取指定目录文件大小
        /// 单位为KB 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static double GetDirectoryFileSizeByKB(string directoryPath)
        {
            var files = GetFileNames(directoryPath);
            double fileSize = 0;

            foreach (var file in files)
            {
                //创建一个文件对象 
                FileInfo fi = new FileInfo(file);
                //获取文件的大小 
                fileSize += Convert.ToDouble(Convert.ToDouble(fi.Length) / 1024);
            }

            return fileSize;
        }

        /// <summary> 
        /// 获取指定目录文件大小
        /// 单位为MB 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static double GetDirectoryFileSizeByMB(string directoryPath)
        {
            var files = GetFileNames(directoryPath);
            double fileSize = 0;

            foreach (var file in files)
            {
                //创建一个文件对象 
                FileInfo fi = new FileInfo(file);
                //获取文件的大小 
                fileSize += Convert.ToDouble(Convert.ToDouble(fi.Length) / 1024 / 1024);
            }

            return fileSize;
        }

        /// <summary> 
        /// 获取指定目录文件大小
        /// 单位为MB 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">指定目录的绝对路径</param>
        public static double GetDirectoryFileSizeByMB(string directoryPath, string searchPattern, bool isSearchChild)
        {
            var files = GetFileNames(directoryPath, searchPattern, isSearchChild);
            double fileSize = 0;

            foreach (var file in files)
            {
                //创建一个文件对象 
                FileInfo fi = new FileInfo(file);
                //获取文件的大小 
                fileSize += Convert.ToDouble(Convert.ToDouble(fi.Length) / 1024 / 1024);
            }

            return fileSize;
        }

        /// <summary> 
        /// 获取指定目录文件大小
        /// 单位为GB 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static double GetDirectoryFileSizeByGB(string directoryPath)
        {
            var files = GetFileNames(directoryPath);
            double fileSize = 0;

            foreach (var file in files)
            {
                //创建一个文件对象 
                FileInfo fi = new FileInfo(file);
                //获取文件的大小 
                fileSize += Convert.ToDouble(Convert.ToDouble(fi.Length) / 1024 / 1024 / 1024);
            }

            return fileSize;
        }

        #endregion

        #region 获取指定目录中的文件列表

        /// <summary> 
        /// 获取指定目录中所有文件列表 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常 
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            //获取文件列表 
            return Directory.GetFiles(directoryPath);
        }

        /// <summary> 
        /// 获取指定目录及子目录中所有文件列表 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常 
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 获取指定目录中的子目录列表

        /// <summary> 
        /// 获取指定目录中所有子目录列表
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static string[] GetDirectories(string directoryPath)
        {
            try
            {
                return Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary> 
        /// 获取指定目录及子目录中所有子目录列表 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 写入内容

        /// <summary> 
        /// 写入内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="content">写入的内容</param>         
        public static void WriteText(string filePath, string content)
        {
            //向文件写入内容 
            File.WriteAllText(filePath, content);
        }

        #endregion

        #region 追加内容

        /// <summary> 
        /// 追加内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="content">写入的内容</param> 
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        #endregion

        #region 复制文件

        /// <summary> 
        /// 复制文件 
        /// </summary> 
        /// <param name="sourceFilePath">源文件的绝对路径</param> 
        /// <param name="destFilePath">目标文件的绝对路径</param> 
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }

        #endregion

        #region 移动目录 

        /// <summary> 
        /// 移动目录 
        /// </summary> 
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param> 
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param> 
        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            //获取源文件的名称 
            string sourceFileName = GetFileName(sourceFilePath);

            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除 
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }

                //将文件移动到指定目录 
                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }

        #endregion

        #region 将流读取到缓冲区中 

        /// <summary> 
        /// 将流读取到缓冲区中 
        /// </summary> 
        /// <param name="stream">原始流</param> 
        public static byte[] StreamToBytes(Stream stream)
        {
            try
            {
                //创建缓冲区 
                byte[] buffer = new byte[stream.Length];

                //读取流 
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

                //返回流 
                return buffer;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //关闭流 
                stream.Close();
            }
        }

        #endregion

        #region 将文件读取到缓冲区中

        /// <summary> 
        /// 将文件读取到缓冲区中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static byte[] FileToBytes(string filePath)
        {
            //获取文件的大小  
            int fileSize = GetFileSize(filePath);

            //创建一个临时缓冲区 
            byte[] buffer = new byte[fileSize];

            //创建一个文件流 
            FileInfo fi = new FileInfo(filePath);

            FileStream fs = fi.Open(FileMode.Open);

            try
            {
                //将文件流读入缓冲区 
                fs.Read(buffer, 0, fileSize);

                return buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                //关闭文件流 
                fs.Close();
            }
        }

        #endregion

        #region 将文件读取到字符串中 

        /// <summary> 
        /// 将文件读取到字符串中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static string FileToString(string filePath)
        {
            return FileToString(filePath, Encoding.UTF8);
        }

        /// <summary> 
        /// 将文件读取到字符串中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="encoding">字符编码</param> 
        public static string FileToString(string filePath, Encoding encoding)
        {
            //创建流读取器 
            StreamReader reader = new StreamReader(filePath, encoding);
            try
            {
                //读取流 
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //关闭流读取器 
                reader.Close();
            }
        }

        #endregion

        #region 获取文件名(包含扩展名) 

        /// <summary> 
        /// 获取文件名
        /// 包含扩展名
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static string GetFileName(string filePath)
        {
            //获取文件的名称 
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }

        #endregion

        #region 获取文件名(不包含扩展名) 

        /// <summary> 
        /// 获取文件名
        /// 不包含扩展名
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static string GetFileNameWithNoExtension(string filePath)
        {
            //获取文件的名称 
            return Path.GetFileNameWithoutExtension(filePath);
        }

        #endregion

        #region 获取扩展名

        /// <summary> 
        /// 获取扩展名 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static string GetExtension(string filePath)
        {
            //获取文件的名称 
            //FileInfo fi = new FileInfo(filePath);
            //return fi.Extension;
            return Path.GetExtension(filePath);
        }

        #endregion

        #region 清空指定目录 

        /// <summary> 
        /// 清空指定目录下所有文件及子目录,但该目录依然保存. 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static void ClearDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                //删除目录中所有的文件 
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    DeleteFile(fileNames[i]);
                }

                //删除目录中所有的子目录 
                string[] directoryNames = GetDirectories(directoryPath);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    DeleteDirectory(directoryNames[i]);
                }
            }
        }

        #endregion

        #region 清空文件内容

        /// <summary> 
        /// 清空文件内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void ClearFile(string filePath)
        {
            //删除文件 
            File.Delete(filePath);

            //重新创建该文件 
            CreateFile(filePath);
        }

        #endregion

        #region 删除指定文件

        /// <summary> 
        /// 删除指定文件 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
            {
                File.Delete(filePath);
            }
        }

        #endregion

        #region 删除指定目录 

        /// <summary> 
        /// 删除指定目录及其所有子目录 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        #endregion

        #region 保存上传文件到临时目录
        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="file">表单附件</param>
        /// <returns>路径</returns>
        public static String SaveFormFile(IFormFile file)
        {
            //获取文件拓展名
            var extension = Path.GetExtension(file.FileName);
            //文件名
            var fileName = Guid.NewGuid() + extension;
            //存储路径
            var path = Path.Combine(GetSystemDirectory, AhChaFortunateGlobalContext.DirectoryConfig.TempPath);
            //文件路径
            var filePath = Path.Combine(path, fileName);
            //文件读写流
            var stream = new FileStream(filePath, FileMode.Create);
            //保存文件
            file.CopyTo(stream);
            //关闭流
            stream.Close();

            return filePath;
        }
        #endregion

        #region 获取表单文件大小

        /// <summary>
        /// 获取表单文件大小
        /// </summary>
        /// <param name="file">表单文件</param>
        /// <param name="remark">单位</param>
        /// <returns>文件大小</returns>
        public static Double GetFileSize(IFormFile file, out String remark)
        {
            Double fileSize = 0;

            var size = file.Length;
            if (size < 1024)
            {
                fileSize = size;
                remark = "B";
            }
            else
            {
                if (size / 1024 < 1024)
                {
                    fileSize = Math.Round((Double)size / 1024, 2);
                    remark = "KB";
                }
                else if (size / 1024 * 1024 < 1024)
                {
                    fileSize = Math.Round((Double)size / 1024 * 1024, 2);
                    remark = "MB";
                }
                else
                {
                    fileSize = Math.Round((Double)size / 1024 * 1024 * 1024, 2);
                    remark = "GB";
                }
            }

            return fileSize;
        }

        #endregion

        #region 保存业务附件
        /// <summary>
        /// 保存业务附件
        /// </summary>
        /// <param name="file">表单附件</param>
        /// <param name="id">附件编号</param>
        /// <returns>附件路径</returns>
        public static string SaveBusinessAttachment(IFormFile file, string id)
        {
            //获取文件拓展名
            var extension = Path.GetExtension(file.FileName);
            //文件名
            var fileName = id + extension;
            //存储路径
            var path = Path.Combine(GetSystemDirectory, AhChaFortunateGlobalContext.DirectoryConfig.UploadFilePath);
            //文件路径
            var filePath = Path.Combine(path, fileName);
            //文件读写流
            var stream = new FileStream(filePath, FileMode.Create);
            //保存文件
            file.CopyTo(stream);
            //关闭流
            stream.Close();
            //路径赋值
            return filePath;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="Code">随机编码</param>
        public static string SaveFile(IFormFile file, string Code)
        {
            //文件类型
            string FileType = FileUtil.GetExtension(file.FileName);
            //拼接路径
            string SavePath = Path.Combine(GetSystemDirectory, AhChaFortunateGlobalContext.DirectoryConfig.UploadFilePath);
            //创建目录
            CreateDirectory(SavePath);
            //文件名
            string FileName = string.Concat(GetFileNameWithNoExtension(file.FileName), "_", Code, FileType);
            //文件路径
            var filePath = Path.Join(SavePath, FileName);
            //文件读写流
            var stream = new FileStream(filePath, FileMode.Create);
            //保存文件
            file.CopyTo(stream);
            //关闭流
            stream.Close();
            return filePath;
        }
        #endregion
    }
}
