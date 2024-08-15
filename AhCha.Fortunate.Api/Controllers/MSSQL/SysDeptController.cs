using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Enum;
using AhCha.Fortunate.Api.AppCode;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDeptDto;

namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 系统部门
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysDeptController : BaseApiController
    {
        private readonly ISysDepService _ISysDepService;
        public SysDeptController(ISysDepService ISysDepService)
        {

            _ISysDepService = ISysDepService;
        }


        /// <summary>
        /// 获取部门数据（树状）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TreeDeptOutput>> GetDepts([FromQuery] QuerySysDepInput input)
        {
            return await _ISysDepService.GetDepts(input);
        }

        /// <summary>
        /// 增加部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddSysDepts(AddSysDepInput input)
        {
            return await _ISysDepService.AddSysDepts(input);
        }

        /// <summary>
        /// 获取部门详情信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SysDeptOutput> GetSysDeptsDetail([FromQuery] SysDeptInput input)
        {
            return await _ISysDepService.GetSysDeptsDetail(input);
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> PutSysDepts(PutSysDepInput input)
        {
            return await _ISysDepService.PutSysDepts(input);
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteSysDepts(DeleteSysDepInput input)
        {
            return await _ISysDepService.DeleteSysDepts(input);
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectHelper>> GetDeptList()
        {
            return await _ISysDepService.GetDeptList();
        }
    }
}
