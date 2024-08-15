using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.Common.Const;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;


namespace AhCha.Fortunate.Api.Controllers.MySQL
{
    /// <summary>
    /// Task任务操作
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.UndefinedModules)]
    public class TaskController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetTaskData()
        {
            var StartTime = DateTime.Now;
            var stringBag = new ConcurrentBag<string>();
            List<Task> tasks = new List<Task>();
            List<string> data = new List<string>();
            Random random = new Random();
            for (int i = 0; i < 9999999; i++)
            {
                data.Add(string.Concat(i, random.Next(100, 999)));
            }
            int size = 100; int taskCount = (data.Count() + size - 1) / size;
            for (int i = 0; i < taskCount; i++)
            {//动态创建任务
                string taskId = i.ToString();
                //给任务分配数据
                var taskData = data.Skip(i * size).Take(size).ToList();
                tasks.Add(Task.Run(() =>
                {
                    //任务数据合并
                    taskData.ForEach(item => stringBag.Add(GetTaskName(taskId, item)));
                }));
            }
            //等待任务全部完成
            await Task.WhenAll(tasks);
            var EndTime = DateTime.Now;
            return $"开始时间：{StartTime}，结束时间：{EndTime}，时间差：{EndTime - StartTime}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetData()
        {
            var StartTime = DateTime.Now;
            var result = new List<string>();
            List<string> data = new List<string>();
            Random random = new Random();
            for (int i = 0; i < 9999999; i++)
            {
                data.Add(string.Concat(i, random.Next(100, 999)));
            }
            int size = 100; int taskCount = (data.Count() + size - 1) / size;
            for (int i = 0; i < taskCount; i++)
            {
                string taskId = i.ToString();
                var taskData = data.Skip(i * size).Take(size).ToList();
                taskData.ForEach(item => result.Add(GetTaskName(taskId, item)));
            }
            var EndTime = DateTime.Now;
            return $"开始时间：{StartTime}，结束时间：{EndTime}，时间差：{EndTime - StartTime}";
        }

    }
}
