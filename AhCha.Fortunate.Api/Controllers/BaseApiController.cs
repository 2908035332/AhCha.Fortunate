using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AhCha.Fortunate.Common.Utility;

namespace AhCha.Fortunate.Api.Controllers
{
    /// <summary>
    /// BaseController，所有新增或生成的Controller都应该继承BaseController
    /// 且定义api后，【禁止瞎几把乱改接口名】导致对接接口404
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]//需要验证token
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// 获取月的天数
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        protected int MonthDays(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 2:
                    if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                        return 29;//闰年
                    else
                        return 28;
                default:
                    return 30;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId">线程id</param>
        /// <param name="item">数据</param>
        /// <returns></returns>
        protected string GetTaskName(string taskId, string item)
        {
            return $"线程id：{taskId} -Value-{item} -Text-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        /// <summary>
        /// 获取密钥（Aes通用密钥）
        /// </summary>
        /// <returns></returns>
        protected string GetAesKey => GenerateKey.GetAesKey;
    }
}
