using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysMenuDto;

namespace AhCha.Fortunate.IService.MSSQL
{
    public interface ISysMenuService : IBaseServices<SysMenu>
    {

        /// <summary>
        /// 获取菜单数据(树形结构)
        /// </summary>
        /// <returns></returns>
        Task<List<TreeMenu>> GetMenus(QuerySysMenuInptu inptu);

        /// <summary>
        /// 删除菜单数据
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteMenus(DeleteSysMenuInput input);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddSysMenu(AddSysMenuInput input);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysMenu(PutSysMenuInput input);

        /// <summary>
        /// 获取菜单详情数据
        /// </summary>
        /// <param name="inptu"></param>
        /// <returns></returns>
        Task<SysMenuOutput> GetSysMenuDetail(SysMenuInptu inptu);

        /// <summary>
        /// 给页面添加按钮(控制权限)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddPageButtons(ButtonsSysMenuInput input);
    }
}
