using Newtonsoft.Json;
using System.Diagnostics;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.IService.MySQL;
using Microsoft.AspNetCore.Mvc.Filters;
using AhCha.Fortunate.Common.Extensions;
using AhCha.Fortunate.Api.Controllers.MSSQL;
using AhCha.Fortunate.ModelsDto.MySQL.ApiRequestLogDto;




namespace AhCha.Fortunate.Api.AppCode
{
    /// <summary>
    /// 记录接口请求日志
    /// </summary>
    public class ApiRequestLogFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //ControllerActionDescriptor? descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            #region 记录所有接口请求日志以及参数
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;
            var sw = new Stopwatch();
            sw.Start();
            var actionContext = await next();
            sw.Stop();

            string ControllerName = actionContext.RouteData.Values["Controller"].ToString();
            string ActionName = actionContext.RouteData.Values["Action"].ToString();

            //登录与上传文件未记录请求日志
            //如需记录需重写改判定
            if (!typeof(SysFileController).Name.StartsWith(ControllerName) && !typeof(LoginController).Name.StartsWith(ControllerName))
            {
                string InputParams = string.Empty;
                //记录接口输入参数
                if (context.HttpContext.Request.Method == "GET")
                {
                    if (context.HttpContext.Request.Query.Count() > 0)
                    {
                        Dictionary<string, string> inputs = new Dictionary<string, string>();
                        foreach (var item in context.HttpContext.Request.Query)
                        {
                            inputs.Add(item.Key, item.Value);
                        }
                        InputParams = JsonConvert.SerializeObject(inputs);
                    }
                }

                if (context.HttpContext.Request.Method == "POST" || context.HttpContext.Request.Method == "PUT" || context.HttpContext.Request.Method == "DELETE")
                {
                    context.HttpContext.Request.EnableBuffering();
                    context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                    using StreamReader stream = new StreamReader(context.HttpContext.Request.Body);
                    InputParams = await stream.ReadToEndAsync();
                    context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                }

                #region 添加MySql梦数据库
                IApiRequestLogService apiRequestLogService = AhChaFortunateGlobalContext.GetService<IApiRequestLogService>();
                PostApiRequestLogInput entity = new PostApiRequestLogInput()
                {
                    ControllerName = ControllerName,
                    ActionName = ActionName,
                    Method = httpRequest.Method,
                    Ip = context.HttpContext.GetIp(),
                    Param = InputParams,
                    Host = httpRequest.Host.ToString(),
                    Path = httpRequest.Path,
                };
                await apiRequestLogService.PostApiRequestLog(entity);
                #endregion
            }

            #endregion
        }
    }

}
