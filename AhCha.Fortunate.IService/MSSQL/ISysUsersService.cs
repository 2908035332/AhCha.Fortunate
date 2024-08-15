using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysUsersDto;

namespace AhCha.Fortunate.IService.MSSQL
{

    /// <summary>
    /// 用户信息
    /// </summary>
    public interface ISysUsersService : IBaseServices<SysUser>
    {

        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        /// <returns></returns>
        Task<List<UserTreeMenuOutput>> GetUserRoute();

        /// <summary>
        /// 获取当前用户按钮
        /// </summary>
        /// <returns></returns>
        Task<List<UserButton>> GetUserBtns();

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        Task<LoginUserInfo> GetLoginUserInfo();

        /// <summary>
        /// 获取用户数据集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SqlSugarPagedList<SysUserOutput>> GetSysUserPage(QuerySysUsersInput input);

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="Users"></param>
        /// <returns></returns>
        Task<bool> AddSysUser(AddSysUserInput input);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysUser(PutSysUserInput input);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteSysUser(List<DeleteSysUserInput> inputs);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> RestUserPwd(SysUserInput input);

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SysUserOutput> GetSysUserDetail(SysUserInput input);

        /// <summary>
        /// 当前用户修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysUserPwd(PutSysUserPwdInput input);
    }
}
