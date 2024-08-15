
namespace AhCha.Fortunate.IService
{

    /// <summary>
    /// 所有的interface都应该基础该BaseInterface，且定义接口后，【禁止瞎几把乱改接口名】导致对接接口404
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseServices<TEntity> where TEntity : class
    {

    }
}
