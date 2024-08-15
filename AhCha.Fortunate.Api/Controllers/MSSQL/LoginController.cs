using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using Microsoft.AspNetCore.Authorization;
using AhCha.Fortunate.ModelsDto.MSSQL.LoginDto;


namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 系统登录模块
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.BusinessModules)]
    public class LoginController : BaseApiController
    {
        private readonly ILoginService _iLoginService;
        public LoginController(ILoginService iLoginService)
        {
            _iLoginService = iLoginService;
        }

        /// <summary>
        /// 获取登录密钥
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetKey()
        {
            return GetAesKey;
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<CaptchaOutput> PostVerificationCode()
        {
            return await _iLoginService.GetVerificationCode();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> LoginAccount(LoginInput input)
        {
            return await _iLoginService.LoginAccount(input);
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SendSms(LoginMobileInput input)
        {
            return await _iLoginService.GetSendSms(input);
        }

        /// <summary>
        /// 手机号码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> LoginPhone(LoginMobileInput input)
        {
            return await _iLoginService.LoginPhone(input);
        }
    }
}
