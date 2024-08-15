using System.ComponentModel;

namespace AhCha.Fortunate.ModelsDto.MSSQL.LoginDto
{
    public class CaptchaOutput
    {
        /// <summary>
        /// 验证码key
        /// </summary>
        public string CaptchaKey { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; set; }

    }

    public class LoginInput : CaptchaOutput
    {
        /// <summary>
        /// 账户
        /// </summary>
        [DefaultValue("admin")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DefaultValue("123456")]
        public string Password { get; set; }

    }

    public class LoginMobileInput : CaptchaOutput
    {
        public string Phone { get; set; }

        public string Captcha { get; set; }
    }
}
