using AhCha.Fortunate.Entity;
using AhCha.Fortunate.IService;
using AhCha.Fortunate.Common.Enum;
using AhCha.Fortunate.Common.Global;

namespace AhCha.Fortunate.Service
{
    /// <summary>
    /// BaseServices，所有的Services都应该继承BaseServices，其中有通用属性,禁止非其它地方访问protected修饰
    /// 且实现接口后，【禁止瞎几把乱改接口名】导致未实现接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 用户id
        /// </summary>
        protected long GetUserId => CurrentUser.GetUserId;

        /// <summary>
        /// 用户名称
        /// </summary>
        protected string? GetUserName => CurrentUser.GetUserName;

        /// <summary>
        /// 当前用户角色id
        /// </summary>
        protected long GetUserRoleId => CurrentUser.GetUserRoleId;

        /// <summary>
        /// 获取用户角色
        /// </summary>
        protected string GetUserRoleName => CurrentUser.GetUserRoleName;

        public List<string> PermissionTypes => Enum.GetNames(typeof(PermissionType)).ToList();

        /// <summary>
        /// 获取Mysql链接字符串（单例模式）
        /// </summary>
        protected DatabaseConfig GetMySQLConn => AhChaFortunateGlobalContext.DatabaseConfigs.Where(x => x.ConfigId == ConstConfigId.MySqlAhChaFortunate).FirstOrDefault();

        /// <summary>
        /// 获取SQL server链接字符串（单例模式）
        /// </summary>
        protected DatabaseConfig GetMSSQLConn => AhChaFortunateGlobalContext.DatabaseConfigs.Where(x => x.ConfigId == ConstConfigId.MSSQLAhChaFortunate).FirstOrDefault();

    }
}
