using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.Common.Enum;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Common.Utility;
using AhCha.Fortunate.ModelsDto.MSSQL.SysMenuDto;
using AhCha.Fortunate.ModelsDto.MSSQL.SysRoleDto;

namespace AhCha.Fortunate.Api.Controllers.MSSQL
{

    /// <summary>
    /// 系统菜单模块
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysMenuController : BaseApiController
    {

        private readonly ISysMenuService _ISysMenuService;

        public SysMenuController(ISysMenuService ISysMenuService)
        {
            _ISysMenuService = ISysMenuService;
        }

        /// <summary>
        /// 获取菜单数据(树形结构)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TreeMenu>> GetMenus([FromQuery] QuerySysMenuInptu input)
        {
            return await _ISysMenuService.GetMenus(input);
        }

        /// <summary>
        /// 删除菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteMenus(DeleteSysMenuInput input)
        {
            return await _ISysMenuService.DeleteMenus(input);
        }

        /// <summary>
        /// 获取所有Controller名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<string>> GetApiTag()
        {
            return await Task.FromResult(ControllerUtil.GetAllControllerNames(Assembly.GetExecutingAssembly()));
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddSysMenu(AddSysMenuInput input)
        {
            return await _ISysMenuService.AddSysMenu(input);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> PutSysMenu(PutSysMenuInput input)
        {
            return await _ISysMenuService.PutSysMenu(input);
        }

        /// <summary>
        /// 获取菜单详情数据
        /// </summary>
        /// <param name="inptu"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SysMenuOutput> GetSysMenuDetail([FromQuery] SysMenuInptu inptu)
        {
            return await _ISysMenuService.GetSysMenuDetail(inptu);
        }

        /// <summary>
        /// 操作权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<string>> GetPermissionType()
        {
            return await Task.FromResult(Enum.GetNames(typeof(PermissionType)).ToList());
        }

        /// <summary>
        /// 给页面添加按钮(控制权限)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddPageButtons(ButtonsSysMenuInput input)
        {
            return await _ISysMenuService.AddPageButtons(input);
        }
    }
}
