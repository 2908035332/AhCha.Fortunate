using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.IService;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Const;
using Microsoft.AspNetCore.Authorization;


namespace AhCha.Fortunate.Api.Controllers
{
    /// <summary>
    /// 生成后端代码
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.BusinessModules)]
    public class GenerateCoreController : BaseApiController
    {
        private readonly IGenerateCoreService iGenerateCore;
        public GenerateCoreController(IGenerateCoreService _iGenerateCore)
        {
            iGenerateCore = _iGenerateCore;
        }
       
        /// <summary>
        /// 获取系统数据库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<List<SelectHelper>> GetSysDatabase()
        {
            return iGenerateCore.GetSysDatabase();
        }

        /// <summary>
        /// 根据数据库获取所有表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<List<SelectHelper>> GetSysTable([FromQuery] GenerateCoreInput input)
        {
            return iGenerateCore.GetSysTable(input);
        }

        /// <summary>
        /// 生成后端代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<string> PostGeneratecCore(GenerateCoreInput input)
        {
            return iGenerateCore.GeneratecCore(input);
        }
    }
}
