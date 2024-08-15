using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysRoleDto;

namespace AhCha.Fortunate.IService.MSSQL
{
    public interface ISysRoleService : IBaseServices<SysRole>
    {

        /// <summary>
        /// 角色集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SqlSugarPagedList<SysRoleOutput>> GetRolePage(QuerySysRoleInput input);

        /// <summary>
        /// 获取该角色拥有的菜单
        /// </summary>
        /// <returns></returns>
        Task<List<RoleTreeMenuOutput>> GetRoleTreeMenu(SysRoleInput input);

        /// <summary>
        /// 根据角色id获取角色详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SysRoleOutput> GetSysRoleDetail(SysRoleInput input);

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddSysRole(AddSysRoleInput input);

        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysRole(PutSysRoleInput input);

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteSysRole(List<DeleteSysRoleInput> inputs);

        /// <summary>
        /// 设置角色可使用菜单
        /// </summary>
        /// <returns></returns>
        Task<bool> SettingRoleMenu(List<SettingRoleMenuInput> inputs);

        /// <summary>
        /// 获取角色数据权限(部门数据，树形结构)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<RoleTreeDeptOutput>> GetRoleTreeDept(SysRoleInput input);

        /// <summary>
        /// 获取角色数据集合
        /// </summary>
        /// <returns></returns>
        Task<List<SelectHelper>> GetRoleList();

        /// <summary>
        /// 当前用户权限
        /// </summary>
        /// <returns></returns>
        Task<List<string>> UserJurisdiction(string ControllerName);
    }
}
