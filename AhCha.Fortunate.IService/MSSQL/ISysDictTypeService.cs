using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDictDataDto;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDictTypeDto;

namespace AhCha.Fortunate.IService.MSSQL
{
    public interface ISysDictTypeService : IBaseServices<SysDictType>
    {

        /// <summary>
        /// 获取字典数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SqlSugarPagedList<SysDictTypeOutput>> GetSysDictTypePage(QuerySysDictTypeInput input);
       
        /// <summary>
        /// 获取字典下级数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<SysDictDataOutput>> GetDictDataList(SysDictDataInput input);

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddSysDictType(AddSysDictTypeInput input);

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysDictType(PutSysDictTypeInput input);

        /// <summary>
        /// 删除数据字典（父级字典删除，需要删除其子级字典数据）
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteSysDictType(List<DeleteSysDictTypeInput> input);

        /// <summary>
        /// 获取数据字典详情（根据id）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SysDictTypeOutput>  GetSysDictTypeDetail(SysDictTypeInput input);

        /// <summary>
        /// 根据字典代码Code获取下级字典数据
        /// </summary>
        /// <returns></returns>
        Task<List<SelectHelper>> GetDataDic(SearchInput input);
    }
}
