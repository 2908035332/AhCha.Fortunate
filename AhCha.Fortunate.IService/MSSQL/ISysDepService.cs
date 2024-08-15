using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Entity.MSSQL;
using AhCha.Fortunate.ModelsDto.MSSQL.SysDeptDto;

namespace AhCha.Fortunate.IService.MSSQL
{
    public interface ISysDepService : IBaseServices<SysDept>
    {
        /// <summary>
        /// 获取部门数据（树状）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<TreeDeptOutput>> GetDepts(QuerySysDepInput input);

        /// <summary>
        /// 增加部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddSysDepts(AddSysDepInput input);

        /// <summary>
        /// 获取部门详情信息
        /// </summary>
        /// <returns></returns>
        Task<SysDeptOutput> GetSysDeptsDetail(SysDeptInput input);

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> PutSysDepts(PutSysDepInput input);

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> DeleteSysDepts(DeleteSysDepInput input);

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        Task<List<SelectHelper>> GetDeptList();

    }





}
