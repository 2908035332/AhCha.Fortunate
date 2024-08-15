using System.Linq.Expressions;
using AhCha.Fortunate.ModelsDto;

namespace AhCha.Fortunate.Repositories.EntityFrameworkCore
{
    public interface IEntityFrameworkCoreRepository<T> where T : class
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add(T entity);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>成功/失败</returns>
        bool AddOk(T entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>更新后的数据实体</returns>
        T Update(T entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>成功/失败</returns>
        bool UpdateOk(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T SelectById(int id);
        /// <summary>
        /// 条件查询返回单个实体
        /// </summary>
        /// <param name="whereLambda">查询条件</param>
        /// <returns></returns>
        T SelectWhere(Expression<Func<T, bool>> whereLambda);
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns>集合</returns>
        List<T> SelectAll();
        /// <summary>
        /// 条件查询返回实体集合
        /// </summary>
        /// <param name="anyLambda">查询条件</param>
        /// <returns>集合</returns>
        List<T> SelectList(Expression<Func<T, bool>> whereLambda);
        /// /// <summary>
        /// 条件查询且排序
        /// </summary>
        /// <typeparam name="TOrder">排序字段的类型</typeparam>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <returns></returns>
        List<T> SelectList<TOrder>(Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, TOrder>> orderbyLambda);
        /// <summary>
        /// 分页条件查询且排序
        /// </summary>
        /// <typeparam name="TOrder">排序字段的类型</typeparam>
        /// <param name="page">分页参数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>分页后的结果</returns>
        SqlSugarPagedList<T> SelectPageList<TOrder>(PageInputBase page, Expression<Func<T, bool>> whereLambda = null, Expression<Func<T, TOrder>> orderbyLambda = null, bool isAsc = false);

    }
}
