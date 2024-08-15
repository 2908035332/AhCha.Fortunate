using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.IService.MySQL;
using AhCha.Fortunate.Common.Utility;
using Microsoft.AspNetCore.Mvc.Filters;
using AhCha.Fortunate.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using AhCha.Fortunate.ModelsDto.MySQL.ApiExceptionLogDto;

namespace AhCha.Fortunate.Api.AppCode
{
    /// <summary>
    /// 接口异常记录，处理
    /// </summary>
    public class ExceptionLogFilter : Attribute, IAsyncExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            ControllerActionDescriptor? descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            // 异常未处理
            if (context.ExceptionHandled == false)
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(Response.Mistake(context.Exception.Message.ToString()))
                };
            }

            string InputParams = string.Empty;
            //记录接口输入参数
            if (context.HttpContext.Request.Method == "GET")
            {
                if (context.HttpContext.Request.Query.Count() > 0)
                {
                    Dictionary<string, string?> inputs = new Dictionary<string, string?>();
                    foreach (var item in context.HttpContext.Request.Query)
                    {
                        inputs.Add(item.Key, item.Value);
                    }
                    InputParams = JsonConvert.SerializeObject(inputs);
                }
            }
            if (context.HttpContext.Request.Method == "POST")
            {
                //倒带启动后只读取一次后报错，使用下列方式解决
                //context.HttpContext.Request.EnableBuffering();
                //context.HttpContext.Request.Body.Position = 0;
                //using var reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8);
                //InputParams = JsonConvert.SerializeObject(reader.ReadToEndAsync().Result);

                context.HttpContext.Request.EnableBuffering();
                context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                using StreamReader stream = new StreamReader(context.HttpContext.Request.Body);
                InputParams = await stream.ReadToEndAsync();
                context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            }
            InputParams = string.IsNullOrWhiteSpace(InputParams) ? "无输入参数" : InputParams;


            #region 输出文件记录日志
            string Content = $"\n ip：{context.HttpContext.GetIp()}，ControllerName：{descriptor?.ControllerName}，ActionName：{descriptor?.ActionName}，InputParams：{InputParams} ,\n  Exception：{context.Exception.ToString()}\n \n ";
            LogUtil.Error(Content);
            #endregion

            #region 异常添加MySql梦数据库
            IApiExceptionLogService iApiExceptionLogService = AhChaFortunateGlobalContext.GetService<IApiExceptionLogService>();
            PostApiExceptionLogInput input = new PostApiExceptionLogInput()
            {
                ActionName = descriptor?.ActionName,
                ControllerName = descriptor?.ControllerName,
                Ip = context.HttpContext.GetIp(),
                ExceptionText = context.Exception.Message.ToString(),
            };
            await iApiExceptionLogService.PostApiExceptionLog(input);
            #endregion

            // 设置异常已经被处理
            context.ExceptionHandled = true;
        }


    }
}
