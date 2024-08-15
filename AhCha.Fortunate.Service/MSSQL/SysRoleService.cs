using Mapster;
using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.SysRoleDto;


namespace AhCha.Fortunate.Service.MSSQL
{
    public class SysRoleService : BaseServices<SysRole>, ISysRoleService
    {
        private readonly SqlSugarRepository<SysRole> _TEntityRep;
        private readonly SqlSugarRepository<SysMenu> _SysMenuRep;
        private readonly SqlSugarRepository<SysRoleMenu> _SysRoleMenuRep;
        private readonly SqlSugarRepository<SysDept> _SysDeptRep;

        public SysRoleService(SqlSugarRepository<SysRole> TEntityRep, SqlSugarRepository<SysMenu> SysMenuRep
            , SqlSugarRepository<SysRoleMenu> SysRoleMenuRep, SqlSugarRepository<SysDept> SysDeptRep)
        {
            _TEntityRep = TEntityRep;
            _SysMenuRep = SysMenuRep;
            _SysRoleMenuRep = SysRoleMenuRep;
            _SysDeptRep = SysDeptRep;
        }

        /// <summary>
        /// 分页获取角色数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<SysRoleOutput>> GetRolePage(QuerySysRoleInput input)
        {
            var query = await _TEntityRep.AsQueryable()
                .WhereIF(!string.IsNullOrWhiteSpace(input.Name), x => SqlFunc.Contains(x.Name, input.Name))
                .WhereIF(input.StartQueryTime != null && input.EndQueryTime != null, x => SqlFunc.Between(x.CreateTime.Value, input.StartQueryTime.Value, input.EndQueryTime.Value))
                .Select<SysRoleOutput>()
                .ToPagedListAsync(input.PageIndex, input.PageSize);
            return query;
        }

        /// <summary>
        /// 根据角色id获取角色详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SysRoleOutput> GetSysRoleDetail(SysRoleInput input)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));
            return entity.Adapt<SysRoleOutput>();
        }

        /// <summary>
        /// 获取该角色拥有的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleTreeMenuOutput>> GetRoleTreeMenu(SysRoleInput input)
        {
            var tree = await _SysMenuRep.AsQueryable()
                  .LeftJoin<SysRoleMenu>((m, rm) => m.Id == rm.MenuId && rm.RoleId == input.Id)
                  .OrderBy((m, rm) => m.Sort)
                  .Select((m, rm) => new RoleTreeMenuOutput()
                  {
                      Id = m.Id,
                      Title = m.Title,
                      ParentId = m.ParentId,
                      Type = m.Type,
                      IsAuth = rm.Id != 0 ? true : false,
                      IsHide = m.IsHide
                  }).ToTreeAsync(s => s.children, s => s.ParentId, 0, s => s.Id);
            return tree;
        }

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddSysRole(AddSysRoleInput input)
        {
            var entity = input.Adapt<SysRole>();
            return await _TEntityRep.InsertAsync(entity) > 0;
        }

        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysRole(PutSysRoleInput input)
        {
            var entity = input.Adapt<SysRole>();
            return await _TEntityRep.UpdateAsync(entity) > 0;
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSysRole(List<DeleteSysRoleInput> inputs)
        {
            var ids = inputs.Select(x => x.Id).ToList();
            return await _TEntityRep.DeleteAsync(x => ids.Contains(x.Id)) > 0;
        }

        /// <summary>
        /// 设置角色可使用菜单
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SettingRoleMenu(List<SettingRoleMenuInput> inputs)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                //先删除该角色所有菜单,再重新添加该角色菜单数据信息
                await _SysRoleMenuRep.DeleteAsync(x => SqlFunc.Equals(x.RoleId, inputs[0].RoleId));
                var entitys = inputs.Adapt<List<SysRoleMenu>>();
                await _SysRoleMenuRep.InsertAsync(entitys);
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception($"设置角色菜单异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取角色数据权限(部门数据，树形结构)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<RoleTreeDeptOutput>> GetRoleTreeDept(SysRoleInput input)
        {
            var rolePermission = _TEntityRep.FirstOrDefault(t => t.Id == input.Id)?.Permission;
            string[] permission = rolePermission?.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return await _SysDeptRep.AsQueryable().Select(d =>
              new RoleTreeDeptOutput
              {
                  Id = d.Id,
                  ParentId = d.ParentId,
                  Name = d.Name,
                  Desc = d.Desc,
                  IsAuth = permission.Contains(d.Id.ToString()) ? true : false,
              }).ToTreeAsync(s => s.children, s => s.ParentId, 0, s => s.Id);
        }

        /// <summary>
        /// 获取角色数据集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectHelper>> GetRoleList()
        {
            return await _TEntityRep.AsQueryable().OrderBy(x => x.CreateTime.Value).Select(x => new SelectHelper()
            {
                value = x.Id.ToString(),
                label = x.Name,
            }).ToListAsync();
        }

        /// <summary>
        /// 当前用户权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> UserJurisdiction(string ControllerName)
        {
            var roleData = await _SysRoleMenuRep.AsQueryable()
                .LeftJoin<SysMenu>((rm, m) => rm.MenuId == m.Id)
                .Where((rm, m) => SqlFunc.Equals(rm.RoleId, GetUserRoleId) && SqlFunc.Equals(m.ApiTag, ControllerName))
                .Select((rm, m) => m.Permission).ToListAsync();
            return roleData;
        }

    }
}
