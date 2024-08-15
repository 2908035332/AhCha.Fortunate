using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AhCha.Fortunate.Api.AppCode
{
    /// <summary>
    /// 接口统一封装后进行输出
    /// </summary>
    public class ResultFilter : Attribute, IAsyncResultFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            #region 方案一，文件下载有问题（不推荐）或做单独判定
            //ObjectResult? result = context.Result as ObjectResult;
            //ObjectResult? objectResult = null;
            //switch (context.HttpContext.Response.StatusCode)
            //{
            //    case StatusCodes.Status403Forbidden:
            //        objectResult = new ObjectResult(Response.Mistake(Message: "无权限访问"));
            //        break;
            //    case StatusCodes.Status200OK:
            //        objectResult = new ObjectResult(Response.Correct(Data: result?.Value));
            //        break;
            //    default:
            //        objectResult = new ObjectResult(Response.Mistake(Message: "系统异常"));
            //        break;
            //}
            //context.Result = objectResult;
            #endregion

            #region 方案二，已解决文件下载问题（推荐）
            //状态码
            //int Status = context.Result is EmptyResult ? StatusCodes.Status204NoContent : StatusCodes.Status200OK;
            switch (context.Result)
            {
                // 处理内容结果
                case ContentResult contentResult:
                    context.Result = new JsonResult(Response.Correct(contentResult.Content));
                    break;
                // 处理对象结果
                case ObjectResult objectResult:
                    context.Result = new JsonResult(Response.Correct(objectResult.Value));
                    break;
                case EmptyResult:
                    break;
            }
            #endregion

            await next.Invoke();
        }
    }
}
