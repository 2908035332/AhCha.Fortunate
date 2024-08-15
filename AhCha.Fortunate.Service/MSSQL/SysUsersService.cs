using Mapster;
using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.SysUsersDto;

namespace AhCha.Fortunate.Service.MSSQL
{
    public class SysUsersService : BaseServices<SysUser>, ISysUsersService
    {
        private readonly SqlSugarRepository<SysUser> _TEntityRep;
        private readonly SqlSugarRepository<SysRoleMenu> _SysRoleMenuRep;
        private readonly SqlSugarRepository<SysMenu> _SysMenuRep;
        private readonly SqlSugarRepository<SysRole> _SysRoleRep;
        private readonly SqlSugarRepository<SysLoginLog> _SysLoginLogRep;
        private readonly SqlSugarRepository<SysUserRole> _SysUserRoleRep;

        public SysUsersService(SqlSugarRepository<SysUser> UsersRep, SqlSugarRepository<SysRole> SysRoleRep
            , SqlSugarRepository<SysRoleMenu> SysRoleMenuRep, SqlSugarRepository<SysLoginLog> SysLoginLogRep
            , SqlSugarRepository<SysMenu> SysMenuRep, SqlSugarRepository<SysUserRole> SysUserRoleRep)
        {
            _TEntityRep = UsersRep;
            _SysRoleMenuRep = SysRoleMenuRep;
            _SysMenuRep = SysMenuRep;
            _SysRoleRep = SysRoleRep;
            _SysLoginLogRep = SysLoginLogRep;
            _SysUserRoleRep = SysUserRoleRep;
        }

        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserTreeMenuOutput>> GetUserRoute()
        {
            var userMenus = await _SysRoleMenuRep.AsQueryable()
                  .LeftJoin<SysMenu>((rm, m) => rm.MenuId == m.Id)
                    .Where((rm, m) => SqlFunc.Equals(m.Type, "Menu") && !m.IsHide && SqlFunc.Equals(rm.RoleId, GetUserRoleId))
                    .OrderBy((rm, m) => m.Sort)
                    .Select((rm, m) => new UserMenu
                    {
                        userId = CurrentUser.GetUserId,
                        roleId = GetUserRoleId,
                        menuId = m.Id,
                        menuParentId = m.ParentId.Value,
                        menuName = m.Name,
                        menuComponent = m.Component,
                        menuIcon = m.Icon,
                        menuIsAffix = m.IsAffix,
                        menuIsHide = m.IsHide,
                        menuIsIframe = m.IsIframe,
                        menuIsKeepAlive = m.IsKeepAlive,
                        menuIsLink = m.IsLink,
                        menuPath = m.Path,
                        menuRedirect = m.Redirect,
                        menuTitle = m.Title,
                        menuLinkUrl = m.Url,

                    }).ToListAsync();

            return ConverTreeMenu(userMenus, 0);
        }
        private List<UserTreeMenuOutput> ConverTreeMenu(List<UserMenu> userMenus, long parentId)
        {
            List<UserTreeMenuOutput> userTreeMenus = new List<UserTreeMenuOutput>();
            var findAll = userMenus?.FindAll(s => s.menuParentId == parentId);
            if (findAll.Count > 0)
            {
                foreach (var item in findAll)
                {
                    UserTreeMenuOutput userTreeMenu = new UserTreeMenuOutput();
                    userTreeMenu.path = item.menuPath;
                    userTreeMenu.name = item.menuName;
                    userTreeMenu.component = item.menuComponent;
                    userTreeMenu.redirect = item.menuRedirect;
                    userTreeMenu.meta = new MenuMeta
                    {
                        title = item.menuTitle,
                        icon = item.menuIcon,
                        isAffix = item.menuIsAffix,
                        isHide = item.menuIsHide,
                        isIframe = item.menuIsIframe,
                        isKeepAlive = item.menuIsKeepAlive,
                        isLink = item.menuIsLink,
                        url = item.menuLinkUrl
                    };
                    userTreeMenu.children = ConverTreeMenu(userMenus, item.menuId);
                    userTreeMenus.Add(userTreeMenu);
                }

            }
            return userTreeMenus;
        }

        /// <summary>
        /// 获取用户按钮
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserButton>> GetUserBtns()
        {
            var btns = await _SysRoleMenuRep.AsQueryable()
                .LeftJoin<SysMenu>((rm, m) => SqlFunc.Equals(rm.MenuId, m.Id))
                .Where((rm, m) => SqlFunc.Equals(rm.RoleId, GetUserRoleId) && SqlFunc.Equals(m.Type, "Button"))
                .Select((rm, m) => new UserButton { Name = m.Name, Path = m.Path }).ToListAsync();
            return btns;
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<LoginUserInfo> GetLoginUserInfo()
        {
            SysLoginLog? entity = await _SysLoginLogRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.UserId, GetUserId) && x.LoginTime > DateTime.Now.Date);
            if (entity != null)
            {
                LoginUserInfo loginfo = entity.Adapt<LoginUserInfo>();
                loginfo.Name = GetUserName;
                loginfo.RoleName = GetUserRoleName;
                return loginfo;
            }
            return await Task.FromResult(new LoginUserInfo());
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<bool> AddSysUser(AddSysUserInput input)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                var entity = input.Adapt<SysUser>();
                bool any = _TEntityRep.Any(x => SqlFunc.Equals(x.Account, entity.Account));
                if (any)
                {
                    throw new Exception("该账号已存在。");
                }
                entity.Salt = PasswordUtil.GetPasswordSalt();
                entity.Password = PasswordUtil.GenEncodingPassword(input.Password, entity.Salt);
                long userId = _TEntityRep.InsertReturnSnowflakeId(entity);
                _SysUserRoleRep.Insert(new SysUserRole()
                {
                    RoleId = input.RoleId,
                    UserId = userId,
                });
                _TEntityRep.Ado.CommitTran();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception($"新增用户信息错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysUser(PutSysUserInput input)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                var entity = input.Adapt<SysUser>();
                await _TEntityRep.UpdateIgnoreNullAsync(entity);
                _SysUserRoleRep.Delete(x => SqlFunc.Equals(x.UserId, entity.Id));
                _SysUserRoleRep.Insert(new SysUserRole()
                {
                    RoleId = input.RoleId,
                    UserId = entity.Id,
                });
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception($"编辑角色信息错误");
            }

        }

        /// <summary>
        /// 获取用户数据集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<SysUserOutput>> GetSysUserPage(QuerySysUsersInput input)
        {
            var query = await _TEntityRep.AsQueryable()
              .LeftJoin<SysUserRole>((u, ur) => SqlFunc.Equals(u.Id, ur.UserId))
              .LeftJoin<SysRole>((u, ur, r) => SqlFunc.Equals(ur.RoleId, r.Id))
              .WhereIF(!string.IsNullOrWhiteSpace(input.Name), (u, ur, r) => SqlFunc.Contains(u.Name, input.Name) || SqlFunc.Contains(u.Account, input.Name))
              .WhereIF(input.StartQueryTime != null && input.EndQueryTime != null, (u, ur, r) => SqlFunc.Between(u.CreateTime.Value, input.StartQueryTime.Value, input.EndQueryTime.Value))
              .Select((u, ur, r) => new SysUserOutput()
              {
                  Id = u.Id,
                  Account = u.Account,
                  Name = u.Name,
                  Address = u.Address,
                  IdCard = u.IdCard,
                  Phone = u.Phone,
                  Remark = u.Remark,
                  UserState = u.UserState,
                  CreateTime = u.CreateTime,
                  RoleId = r.Id,
                  RoleName = r.Name
              })
              .ToPagedListAsync(input.PageIndex, input.PageSize);
            return query;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSysUser(List<DeleteSysUserInput> inputs)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                List<long> userIds = inputs.Select(x => x.Id).ToList();
                var entity = await _TEntityRep.AsQueryable().Where(x => SqlFunc.Equals(x.Account, "admin")).FirstAsync();
                userIds.Remove(entity.Id);
                await _SysUserRoleRep.DeleteAsync(x => userIds.Contains(x.UserId));
                await _TEntityRep.DeleteAsync(x => SqlFunc.ContainsArray(userIds, x.Id));
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception($"删除用户数据异常：{ex.Message}");
            }
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> RestUserPwd(SysUserInput input)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));
            if (entity == null)
            {
                throw new Exception("用户不存在");
            }

            entity.Salt = PasswordUtil.GetPasswordSalt();
            entity.Password = PasswordUtil.GenEncodingPassword(AhChaFortunateGlobalContext.RestUserPwd, entity.Salt);
            await _TEntityRep.UpdateAsync(entity);
            return $"重置成功，新密码为: {AhChaFortunateGlobalContext.RestUserPwd}";
        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SysUserOutput> GetSysUserDetail(SysUserInput input)
        {
            var entity = _TEntityRep.FirstOrDefault(x => SqlFunc.Equals(x.Id, input.Id));
            var entityOut = entity.Adapt<SysUserOutput>();
            //获取用户角色
            var role = await _SysRoleRep.AsQueryable()
                    .LeftJoin<SysUserRole>((r, ur) => SqlFunc.Equals(r.Id, ur.RoleId))
                    .Where((r, ur) => SqlFunc.Equals(ur.UserId, entity.Id))
                    .FirstAsync();
            if (role != null)
            {
                entityOut.RoleId = role.Id;
                entityOut.RoleName = role.Name;
            }
            return entityOut;
        }

        /// <summary>
        /// 当前用户修改密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysUserPwd(PutSysUserPwdInput input)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, GetUserId));
            //监测原密码是否正确
            string OldPassWord = PasswordUtil.GenEncodingPassword(input.OldPassWord, entity.Salt);
            if (entity.Password != OldPassWord)
            {
                throw new Exception("原密码输入不一致");
            }
            //监测新密码
            string NewPassWord = PasswordUtil.GenEncodingPassword(input.NewPassWord, entity.Salt);
            if (entity.Password == NewPassWord)
            {
                throw new Exception("新密码不能与旧密码一致");
            }
            entity.Password = NewPassWord;
            int i = await _TEntityRep.AsUpdateable(entity).UpdateColumns(x => x.Password).ExecuteCommandAsync();
            return i > 0;
        }
    }
}
