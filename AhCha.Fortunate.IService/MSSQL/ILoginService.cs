using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.LoginDto;

namespace AhCha.Fortunate.IService.MSSQL
{
    public interface ILoginService : IBaseServices<SysUser>
    {

        /// <summary>
        /// 登录（使用账号密码进行登录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> LoginAccount(LoginInput input);

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        Task<CaptchaOutput> GetVerificationCode();

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GetSendSms(LoginMobileInput input);

        /// <summary>
        /// 手机号码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> LoginPhone(LoginMobileInput input);

    }
}
