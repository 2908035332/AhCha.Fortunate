using Mapster;
using SqlSugar;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.SysMenuDto;


namespace AhCha.Fortunate.Service.MSSQL
{
    public class SysMenuService : BaseServices<SysMenu>, ISysMenuService
    {

        private readonly SqlSugarRepository<SysMenu> _TEntityRep;
        private readonly SqlSugarRepository<SysRoleMenu> _SysRoleMenuRep;

        public SysMenuService(SqlSugarRepository<SysMenu> TEntityRep, SqlSugarRepository<SysRoleMenu> sysRoleMenuRep)
        {
            _TEntityRep = TEntityRep;
            _SysRoleMenuRep = sysRoleMenuRep;
        }

        /// <summary>
        /// 删除菜单数据
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteMenus(DeleteSysMenuInput input)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                List<long> deleteIds = new List<long>();
                //当前本级
                deleteIds.Add(input.Id);
                this.GetRecursiveMenu(deleteIds, input.Id);
                //移除拥有该菜单的角色数据
                await _SysRoleMenuRep.DeleteAsync(x => deleteIds.Contains(x.MenuId));
                //删除菜单数据
                await _TEntityRep.DeleteAsync(x => deleteIds.Contains(x.Id));
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception($"删除菜单异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 递归获取需要删除的菜单子集id集合
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="ParentId"></param>
        private void GetRecursiveMenu(List<long> menus, long ParentId)
        {
            List<SysMenu>? entitys = _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(x.ParentId, ParentId)).ToList();
            foreach (SysMenu entity in entitys)
            {
                menus.Add(entity.Id);
                this.GetRecursiveMenu(menus, entity.Id);
            }
        }

        /// <summary>
        /// 获取菜单数据(树形结构)
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeMenu>> GetMenus(QuerySysMenuInptu inptu)
        {
            return await _TEntityRep.AsQueryable()
                   .WhereIF(!string.IsNullOrWhiteSpace(inptu.Title), x => SqlFunc.Contains(x.Title, inptu.Title))
                   .Select(t => new TreeMenu()
                   {
                       ParentId = t.ParentId,
                       Id = t.Id,
                       Component = t.Component,
                       Title = t.Title,
                       Name = t.Name,
                       Sort = t.Sort,
                       Icon = t.Icon,
                       IsHide = t.IsHide,
                       Path = t.Path,
                       Type = t.Type,
                       ApiTag = t.ApiTag,
                       Permission = t.Permission,
                       Url = t.Url,
                   }).OrderBy(s => s.Sort)
                   .ToTreeAsync(s => s.children, s => s.ParentId, 0, s => s.Id);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddSysMenu(AddSysMenuInput input)
        {
            var entity = input.Adapt<SysMenu>();
            return await _TEntityRep.InsertAsync(entity) > 0;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysMenu(PutSysMenuInput input)
        {
            var entity = input.Adapt<SysMenu>();
            return await _TEntityRep.UpdateAsync(entity) > 0;
        }

        /// <summary>
        /// 获取菜单详情数据
        /// </summary>
        /// <param name="inptu"></param>
        /// <returns></returns>
        public async Task<SysMenuOutput> GetSysMenuDetail(SysMenuInptu inptu)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, inptu.Id));
            return entity.Adapt<SysMenuOutput>();
        }

        /// <summary>
        /// 给页面添加按钮(控制权限)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddPageButtons(ButtonsSysMenuInput input)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "Add", "新增" },
                { "Edit", "编辑" },
                { "Delete", "删除" }
            };
            var parent = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));
            var btns = await _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(x.ParentId, input.Id) && SqlFunc.Equals(x.Type, "Button")).ToListAsync();
            foreach (var item in dic)
            {
                if (!btns.Exists(t => t.Name == item.Key))
                {
                    SysMenu sysMenu = new SysMenu
                    {
                        ParentId = parent.Id,
                        Title = item.Value,
                        Name = item.Key,
                        Path = parent.Path,
                        Component = parent.Component,
                        Type = "Button",
                        ApiTag = input.ApiTag,
                        Permission = string.Join(",", PermissionTypes.Where(s => s == item.Key)) + ",",
                    };
                    return await _TEntityRep.InsertAsync(sysMenu) > 0;
                }
            }
            return false;
        }
    }
}
