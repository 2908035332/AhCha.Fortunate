using Microsoft.AspNetCore.Mvc;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Const;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDictDataDto;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDictTypeDto;



namespace AhCha.Fortunate.Api.Controllers.MSSQL
{
    /// <summary>
    /// 系统字典模块
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = SwaggerGroupName.SystemModules)]
    public class SysDictController : BaseApiController
    {
        /// <summary>
        /// 数据字典 - 类型
        /// </summary>
        private readonly ISysDictTypeService _ISysDictTypeService;

        public SysDictController(ISysDictTypeService iSysDictTypeService)
        {
            _ISysDictTypeService = iSysDictTypeService;
        }

        #region 数据字典 - 类型

        /// <summary>
        /// 获取字典数据集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SqlSugarPagedList<SysDictTypeOutput>> GetSysDictTypePage([FromQuery] QuerySysDictTypeInput input)
        {
            return await _ISysDictTypeService.GetSysDictTypePage(input);
        }

        /// <summary>
        /// 获取数据字典详情（根据id）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SysDictTypeOutput> GetSysDictTypeDetail([FromQuery] SysDictTypeInput input)
        {
            return await _ISysDictTypeService.GetSysDictTypeDetail(input);
        }

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddSysDictType(AddSysDictTypeInput input)
        {
            return await _ISysDictTypeService.AddSysDictType(input);
        }

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> PutSysDictType(PutSysDictTypeInput input)
        {
            return await _ISysDictTypeService.PutSysDictType(input);
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteSysDictType(List<DeleteSysDictTypeInput> input)
        {
            return await _ISysDictTypeService.DeleteSysDictType(input);
        }

        #endregion

        #region 数据字典 - 类型数据

        /// <summary>
        /// 获取字典下级数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SysDictDataOutput>> GetDictDataList([FromQuery] SysDictDataInput input)
        {
            return await _ISysDictTypeService.GetDictDataList(input);
        }


        #endregion


        /// <summary>
        /// 根据字典代码Code获取下级字典数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectHelper>> GetDataDic([FromQuery] SearchInput input)
        {
            return await _ISysDictTypeService.GetDataDic(input);
        }
    }
}
