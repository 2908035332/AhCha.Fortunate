using Mapster;
using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.IService.MSSQL;
using AhCha.Fortunate.Repositories.SqlSugar;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDeptDto;


namespace AhCha.Fortunate.Service.MSSQL
{
    public class SysDepService : BaseServices<SysDept>, ISysDepService
    {
        private readonly SqlSugarRepository<SysDept> _TEntityRep;
        public SysDepService(SqlSugarRepository<SysDept> TEntityRep)
        {
            _TEntityRep = TEntityRep;
        }

        /// <summary>
        /// 获取部门数据（树状）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<TreeDeptOutput>> GetDepts(QuerySysDepInput input)
        {
            var queryable = _TEntityRep.AsQueryable()
                .WhereIF(!string.IsNullOrWhiteSpace(input?.Name), x => x.Name.Contains(input.Name))
                .Select(t => new TreeDeptOutput
                {
                    ParentId = t.ParentId,
                    Id = t.Id,
                    Name = t.Name,
                    Desc = t.Desc,
                    Status = t.Status,
                    CreateTime = t.CreateTime
                });
            if (!string.IsNullOrWhiteSpace(input?.Name))
            {
                return queryable.ToList();
            }
            else
            {
                return queryable.ToTree(s => s.children, s => s.ParentId, 0, s => s.Id);
            }
        }

        /// <summary>
        /// 增加部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> AddSysDepts(AddSysDepInput input)
        {
            var entity = input.Adapt<SysDept>();
            return await _TEntityRep.InsertAsync(entity) > 0;
        }

        /// <summary>
        /// 获取部门详情信息
        /// </summary>
        /// <returns></returns>
        public async Task<SysDeptOutput> GetSysDeptsDetail(SysDeptInput input)
        {
            var entity = await _TEntityRep.FirstOrDefaultAsync(x => SqlFunc.Equals(x.Id, input.Id));
            return entity.Adapt<SysDeptOutput>();
        }

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> PutSysDepts(PutSysDepInput input)
        {
            var entity = input.Adapt<SysDept>();
            return await _TEntityRep.UpdateIgnoreNullAsync(entity) > 0;
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSysDepts(DeleteSysDepInput input)
        {
            return await _TEntityRep.DeleteAsync(x => SqlFunc.Equals(x.Id, input.Id)) > 0;
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<SelectHelper>> GetDeptList()
        {
            return await _TEntityRep.AsQueryable()
                .OrderBy(x => x.CreateTime.Value).Select(x => new SelectHelper()
                {
                    value = x.Id.ToString(),
                    label = x.Name,
                    parentId = x.ParentId,
                }).ToTreeAsync(it => it.children, it => it.parentId, 0, it => it.value);
        }
    }
}
