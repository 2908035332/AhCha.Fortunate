using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysRoleDto;


namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 系统角色模块
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysRoleController : BaseApiController
    {
        private readonly ISysRoleService _iRoleService;
        public SysRoleController(ISysRoleService roleService)
        {
            _iRoleService = roleService;
        }

        /// <summary>
        /// 获取角色集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SqlSugarPagedList<SysRoleOutput>> GetRolePage([FromQuery] QuerySysRoleInput input)
        {
            return await _iRoleService.GetRolePage(input);
        }

        /// <summary>
        /// 获取该角色拥有的菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleTreeMenuOutput>> GetRoleTreeMenu([FromQuery] SysRoleInput input)
        {
            return await _iRoleService.GetRoleTreeMenu(input);
        }

        /// <summary>
        /// 获取数据范围
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string[] GetDataRang()
        {
            return typeof(DataRang).GetFields().Select(s => (string)s.GetValue(null)).ToArray();
        }

        /// <summary>
        /// 根据角色id获取角色详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SysRoleOutput> GetSysRoleDetail([FromQuery] SysRoleInput input)
        {
            return await _iRoleService.GetSysRoleDetail(input);
        }

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddSysRole(AddSysRoleInput input)
        {
            return await _iRoleService.AddSysRole(input);
        }

        /// <summary>
        /// 编辑角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> PutSysRole(PutSysRoleInput input)
        {
            if (input == null || input.Id <= 0)
            {
                throw new Exception("非法数据。");
            }

            return await _iRoleService.PutSysRole(input);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteSysRole(List<DeleteSysRoleInput> inputs)
        {
            return await _iRoleService.DeleteSysRole(inputs);
        }

        /// <summary>
        /// 设置角色可使用菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SettingRoleMenu(List<SettingRoleMenuInput> inputs)
        {
            return await _iRoleService.SettingRoleMenu(inputs);
        }

        /// <summary>
        /// 获取角色数据权限(部门数据，树形结构)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RoleTreeDeptOutput>> GetRoleTreeDept([FromQuery] SysRoleInput input)
        {
            return await _iRoleService.GetRoleTreeDept(input);
        }

        /// <summary>
        /// 获取角色数据集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectHelper>> GetRoleList()
        {
            return await _iRoleService.GetRoleList();
        }
    }
}
