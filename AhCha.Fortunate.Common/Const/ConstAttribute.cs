namespace AhCha.Fortunate.Common.Const
{

    //系统统一管理 常量  【虽然我自己未遵循】

    public class ClaimConst
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public const string CLAINM_USERID = "UserId";

        /// <summary>
        /// 账号
        /// </summary>
        public const string CLAINM_ACCOUNT = "Account";

        /// <summary>
        /// 名称
        /// </summary>
        public const string CLAINM_NAME = "Name";

        /// <summary>
        /// 角色id
        /// </summary>
        public const string CLAINM_ROLE_ID = "RoleId";

        /// <summary>
        /// 角色名称
        /// </summary>
        public const string CLAINM_ROLE_Name = "RoleName";

        /// <summary>
        /// 设备id
        /// </summary>
        public const string CLAINM_DEVICE_ID = "DeviceId";
    }
    
    public class ClaimEntity
    {

        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string? Account { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public string? RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string? DeviceId { get; set; }

    }

    /// <summary>
    /// Swagger版本控制()=>Controllers标识
    /// </summary>
    public class SwaggerGroupName
    {

        /// <summary>
        /// 系统模块
        /// </summary>
        public const string SystemModules = "SystemModules";

        /// <summary>
        /// 业务模块
        /// </summary>
        public const string BusinessModules = "BusinessModules";

        /// <summary>
        /// 生成的Controller默认分配的Swagger版本
        /// </summary>
        public const string UndefinedModules = "UndefinedModules";

    }

}
