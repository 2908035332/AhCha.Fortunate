using Microsoft.AspNetCore.Http;
using AhCha.Fortunate.Common.Const;

namespace AhCha.Fortunate.Common.Global
{
    /// <summary>
    /// 当前登录人用户消息
    /// </summary>
    public static class CurrentUser
    {
        public static object? factory;

        //
        private static HttpContext _Context
        {
            get
            {
                HttpContext context = ((IHttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

        /// <summary>
        /// 当前登录id
        /// </summary>
        public static long GetUserId => long.Parse(_Context.User.FindFirst(ClaimConst.CLAINM_USERID)?.Value ?? "0");

        /// <summary>
        /// 当前登录用户角色ID
        /// </summary>
        public static long GetUserRoleId => long.Parse(_Context.User.FindFirst(ClaimConst.CLAINM_ROLE_ID)?.Value ?? "0");

        /// <summary>
        /// 当前登录用户角色
        /// </summary>
        public static string? GetUserRoleName => _Context.User.FindFirst(ClaimConst.CLAINM_ROLE_Name)?.Value;

        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public static string? GetUserName => _Context.User.FindFirst(ClaimConst.CLAINM_NAME)?.Value;

        /// <summary>
        /// 当前登录用户账号
        /// </summary>
        public static string? GetUserAccount => _Context.User.FindFirst(ClaimConst.CLAINM_ACCOUNT)?.Value;

        /// <summary>
        /// 获取登录设备id
        /// </summary>
        public static string? GetUserDeviceId => _Context.User.FindFirst(ClaimConst.CLAINM_DEVICE_ID)?.Value;

    }
}
