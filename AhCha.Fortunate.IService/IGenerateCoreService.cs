using AhCha.Fortunate.ModelsDto;

namespace AhCha.Fortunate.IService
{
    public interface IGenerateCoreService
    {

        /// <summary>
        /// 获取系统数据库
        /// </summary>
        /// <returns></returns>
        Task<List<SelectHelper>> GetSysDatabase();

        /// <summary>
        /// 根据数据库获取所有表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<SelectHelper>> GetSysTable(GenerateCoreInput input);

        /// <summary>
        /// 生成后端代码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GeneratecCore(GenerateCoreInput input);
    }
}
