using SqlSugar;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AhCha.Fortunate.Common.Cache;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.Common.Extensions;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.LoginDto;


namespace AhCha.Fortunate.Service.MSSQL
{
    public class LoginService : BaseServices<SysUser>, ILoginService
    {
        private readonly SqlSugarRepository<SysUser> _TEntityRep;
        private readonly SqlSugarRepository<SysUserRole> _SysUserRoleRep;
        private readonly SqlSugarRepository<SysLoginLog> _SysLoginLogRep;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public LoginService(SqlSugarRepository<SysUser> TEntityRep, SqlSugarRepository<SysLoginLog> SysLoginLogRep,
            SqlSugarRepository<SysUserRole> SysUserRoleRep, IHttpContextAccessor httpContextAccessor)
        {
            _TEntityRep = TEntityRep;
            _SysUserRoleRep = SysUserRoleRep;
            _SysLoginLogRep = SysLoginLogRep;
            _HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 登录（使用账号密码进行登录）
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Token</returns>
        public async Task<string> LoginAccount(LoginInput input)
        {
            if (!MemoryCacheHelper.Exists(input.CaptchaKey) || MemoryCacheHelper.Get<string>(input.CaptchaKey).ToLower() != input.Captcha.ToLower())
            {
                throw new Exception("图形验证码已过期或输入错误，请重新输入。");
            }
            LogUtil.Info("账户：" + JsonConvert.SerializeObject(input));
            string Salt = PasswordUtil.GetSalt(await GetAccountSalt(input.Account));
            //加盐后的密码
            string passwordSalt = PasswordUtil.GenEncodingPassword(input.Password, Salt);
            var entity = await _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(x.Account, input.Account) && SqlFunc.Equals(x.Password, passwordSalt)).FirstAsync();
            if (entity == null)
            {
                throw new Exception("账户或密码错误，请检查。");
            }
            //当前用户角色
            var UserRole = _SysUserRoleRep.AsQueryable()
                .LeftJoin<SysRole>((ur, sr) => SqlFunc.Equals(ur.RoleId, sr.Id))
                .Where((ur, sr) => SqlFunc.Equals(ur.UserId, entity.Id))
                .Select((ur, sr) => new SysRole()
                {
                    Id = sr.Id,
                    Name = sr.Name,
                })
                .First();
            ClaimEntity claimEntity = new ClaimEntity()
            {
                Id = entity.Id.ToString(),
                Account = entity.Account,
                Name = entity.Name,
                RoleId = UserRole.Id.ToString(),
                RoleName = UserRole.Name,
                DeviceId = input.DeviceId,
            };
            string token = TokenUtility.GetToken(claimEntity);

            #region 记录登录日志
            SysLoginLog loginLog = new SysLoginLog();
            loginLog.UserId = entity.Id;
            loginLog.IP = _HttpContextAccessor.HttpContext.GetIp();
            loginLog.UAStr = _HttpContextAccessor.HttpContext.GetUserAgent();
            var UAInfo = HttpUtil.GetUserAgentInfo(loginLog.UAStr);
            loginLog.Browser = UAInfo.Browser;
            loginLog.OS = UAInfo.OS;
            loginLog.Device = UAInfo.Device;
            loginLog.LoginTime = DateTime.Now;
            _SysLoginLogRep.Insert(loginLog);
            #endregion

            return token;
        }

        /// <summary>
        /// 手机号码登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> LoginPhone(LoginMobileInput input)
        {
            if (!MemoryCacheHelper.Exists(input.Phone) || MemoryCacheHelper.Get<string>(input.Phone).ToLower() != input.Captcha.ToLower())
            {
                throw new Exception("手机验证码已过期或输入错误，请重新输入。");
            }
            var entitys = await _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(x.Phone, input.Phone)).ToListAsync();
            if (entitys == null || entitys.Count <= 0)
            {
                throw new Exception("手机号码输入错误。");
            }

            if (entitys.Count > 1)
            {
                throw new Exception("该手机号码绑定多个账号，请联系管理员。");
            }

            var entity = entitys.FirstOrDefault();

            //当前用户角色
            var UserRole = _SysUserRoleRep.AsQueryable()
                .LeftJoin<SysRole>((ur, sr) => SqlFunc.Equals(ur.RoleId, sr.Id))
                .Where((ur, sr) => SqlFunc.Equals(ur.UserId, entity.Id))
                .Select((ur, sr) => new SysRole()
                {
                    Id = sr.Id,
                    Name = sr.Name,
                })
                .First();
            ClaimEntity claimEntity = new ClaimEntity()
            {
                Id = entity.Id.ToString(),
                Account = entity.Account,
                Name = entity.Name,
                RoleId = UserRole.Id.ToString(),
                RoleName = UserRole.Name,
                DeviceId = input.DeviceId,
            };
            string token = TokenUtility.GetToken(claimEntity);

            #region 记录登录日志
            SysLoginLog loginLog = new SysLoginLog();
            loginLog.UserId = entity.Id;
            loginLog.IP = _HttpContextAccessor.HttpContext.GetIp();
            loginLog.UAStr = _HttpContextAccessor.HttpContext.GetUserAgent();
            var UAInfo = HttpUtil.GetUserAgentInfo(loginLog.UAStr);
            loginLog.Browser = UAInfo.Browser;
            loginLog.OS = UAInfo.OS;
            loginLog.Device = UAInfo.Device;
            loginLog.LoginTime = DateTime.Now;
            _SysLoginLogRep.Insert(loginLog);
            #endregion

            return token;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public async Task<CaptchaOutput> GetVerificationCode()
        {
            CaptchaOutput output = new CaptchaOutput();
            string Code = VerifyCodeUtility.GetCode();
            output.CaptchaKey = Guid.NewGuid().ToString();
            MemoryCacheHelper.Set(output.CaptchaKey, Code, new TimeSpan(hours: 0, minutes: 10, seconds: 0));
            var stream = VerifyCodeUtility.CreateValidGraphic(Code);
            output.Captcha = string.Concat("data:image/png;base64,", Convert.ToBase64String(stream.ToArray()));
            stream.Dispose();
            return await Task.FromResult(output);
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<string> GetSendSms(LoginMobileInput input)
        {
            string Code = VerifyCodeUtility.GetCode();
            MemoryCacheHelper.Set(input.Phone, Code, new TimeSpan(hours: 0, minutes: 10, seconds: 0));
            if (AhChaFortunateGlobalContext.isTest)
            {
                return Task.FromResult($"验证码已发送。验证码：{Code}");
            }
            else
            {
                return Task.FromResult("验证码已发送至您的手机，请注意查收。");
            }
        }

        /// <summary>
        /// 根据账号获取当前用户密码盐
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private async Task<string> GetAccountSalt(string Account)
        {
            var UsersList = await _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(x.Account, Account)).ToListAsync();
            if (UsersList?.Count() == 1)
            {
                return string.Concat("ok", PasswordUtil.SaltCode, UsersList.FirstOrDefault().Salt);
            }
            else
            {
                throw new Exception("账号密码输入错误，请检查。");
            }
        }

    }
}
