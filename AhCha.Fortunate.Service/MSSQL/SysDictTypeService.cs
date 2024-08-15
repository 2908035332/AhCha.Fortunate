using Mapster;
using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDictDataDto;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDictTypeDto;





namespace AhCha.Fortunate.Service.MSSQL
{
    public class SysDictTypeService : BaseServices<SysDictType>, ISysDictTypeService
    {
        private readonly SqlSugarRepository<SysDictType> _TEntityRep;
        private readonly SqlSugarRepository<SysDictData> _SysDictDataReq;
        public SysDictTypeService(SqlSugarRepository<SysDictType> TEntityRep, SqlSugarRepository<SysDictData> SysDictDataReq)
        {
            _TEntityRep = TEntityRep;
            _SysDictDataReq = SysDictDataReq;
        }


        /// <summary>
        /// 获取字典数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SqlSugarPagedList<SysDictTypeOutput>> GetSysDictTypePage(QuerySysDictTypeInput input)
        {
            var query = await _TEntityRep.AsQueryable()
              .WhereIF(!string.IsNullOrWhiteSpace(input.Name), x => SqlFunc.Contains(x.Name, input.Name))
              .WhereIF(input.StartQueryTime != null && input.EndQueryTime != null, x => SqlFunc.Between(x.CreateTime.Value, input.StartQueryTime.Value, input.EndQueryTime.Value))
              .Select<SysDictTypeOutput>()
              .ToPagedListAsync(input.PageIndex, input.PageSize);
            return query;
        }

        /// <summary>
        /// 获取字典下级数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<SysDictDataOutput>> GetDictDataList(SysDictDataInput input)
        {
            return await _SysDictDataReq.AsQueryable(x => SqlFunc.Equals(x.TypeId, input.Id))
                  .Select<SysDictDataOutput>()
                  .ToListAsync();
        }

        /// <summary>
        /// 新增数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddSysDictType(AddSysDictTypeInput input)
        {
            if (input == null) { throw new Exception("非法数据。"); }
            try
            {
                _TEntityRep.Ado.BeginTran();
                bool any = await _TEntityRep.AnyAsync(x => SqlFunc.Equals(x.Name, input.Name));
                if (any) { throw new Exception("该字典已存在。"); }
                SysDictType entity = input.Adapt<SysDictType>();
                entity.Id = Yitter.IdGenerator.YitIdHelper.NextId();

                #region 字典 - 类型数据
                if (input.DictDatas != null && input.DictDatas.Count > 0)
                {
                    var addDicData = input.DictDatas.Adapt<List<SysDictData>>();
                    addDicData.ForEach(item => item.TypeId = entity.Id);
                    await _SysDictDataReq.InsertAsync(addDicData);
                }
                #endregion

                await _TEntityRep.InsertAsync(entity);
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception("新增字典异常");
            }
        }

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysDictType(PutSysDictTypeInput input)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                SysDictType entity = input.Adapt<SysDictType>();
                #region 数据字典子集数据修改

                if (input.DictDatas != null && input.DictDatas.Count > 0)
                {
                    var dataOld = input.DictDatas.Adapt<List<SysDictData>>();
                    await _SysDictDataReq.DeleteAsync(x => SqlFunc.Equals(x.TypeId, entity.Id));
                    dataOld.ForEach(item => item.Id = 0);
                    await _SysDictDataReq.InsertAsync(dataOld);
                }

                #endregion
                await _TEntityRep.UpdateAsync(entity);
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception("修改字典异常");
            }
        }

        /// <summary>
        /// 删除数据字典（父级字典删除，需要删除其子级字典数据）
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteSysDictType(List<DeleteSysDictTypeInput> input)
        {
            try
            {
                _TEntityRep.Ado.BeginTran();
                List<long> ids = input.Select(x => x.Id).ToList();
                await _SysDictDataReq.DeleteAsync(x => ids.Contains(x.TypeId));
                await _TEntityRep.DeleteAsync(x => ids.Contains(x.Id));
                _TEntityRep.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                _TEntityRep.Ado.RollbackTran();
                throw new Exception($"删除字典异常{ex.Message.ToString()}");
            }
        }

        /// <summary>
        /// 获取数据字典详情（根据id）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SysDictTypeOutput> GetSysDictTypeDetail(SysDictTypeInput input)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));
            return entity.Adapt<SysDictTypeOutput>();
        }

        /// <summary>
        /// 根据字典代码Code获取下级字典数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectHelper>> GetDataDic(SearchInput input)
        {

            return await _SysDictDataReq.AsQueryable()
                 .LeftJoin<SysDictType>((a, b) => SqlFunc.Equals(a.TypeId, b.Id))
                 .Where((a, b) => SqlFunc.Equals(b.Code, input.Code))
                 .Select((a, b) => new SelectHelper()
                 {
                     value = a.Value1,
                     label = a.Name
                 })
                 .ToListAsync();
        }

    }
}
