using System.Linq.Expressions;
using AhCha.Fortunate.ModelsDto;
using Microsoft.EntityFrameworkCore;
using AhCha.Fortunate.EntityFrameworkCore;

namespace AhCha.Fortunate.Repositories.EntityFrameworkCore
{
    public class EntityFrameworkCoreRepository<T> : IEntityFrameworkCoreRepository<T> where T : class
    {
        private readonly AhChaFortunateContext context;
        private readonly DbSet<T> dbSet;

        public EntityFrameworkCoreRepository(AhChaFortunateContext _context)
        {
            context = _context;
            dbSet = _context.Set<T>();
        }


        #region 添加
        public T Add(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public bool AddOk(T entity)
        {
            dbSet.Add(entity);
            return context.SaveChanges() > 0;
        }
        #endregion

        #region 删除
        public bool Delete(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Remove(entity);
            return context.SaveChanges() > 0;
        }
        #endregion

        #region 查询
        public T SelectById(int id)
        {
            return dbSet.Find(id);
        }
        public T SelectWhere(Expression<Func<T, bool>> whereLambda)
        {
            //.AsNoTracking()非追踪查询
            //针对查询，在一些情况下，我们只需要返回一个只读的数据就可以，并不会对数据记录进行任何的修改。这种时候不希望Entity Framework进行不必要的状态变动跟踪，
            //可以使用Entity Framework的AsNoTracking方法来查询返回不带变动跟踪的查询结果。
            //由于是无变动跟踪，所以对返回的数据的没有进行任何修改，在SaveChanges()时，都不会提交到数据库中。
            return dbSet.AsNoTracking().FirstOrDefault(whereLambda);
        }
        public List<T> SelectList(Expression<Func<T, bool>> whereLambda)
        {
            return dbSet.AsNoTracking().Where(whereLambda).ToList();
        }

        public List<T> SelectAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public List<T> SelectList<TOrder>(Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, TOrder>> orderbyLambda)
        {
            var list = dbSet.AsNoTracking().Where(whereLambda);
            if (isAsc)
                list = list.OrderBy<T, TOrder>(orderbyLambda);
            else
                list = list.OrderByDescending<T, TOrder>(orderbyLambda);
            return list.ToList();
        }
        public SqlSugarPagedList<T> SelectPageList<TOrder>(PageInputBase page, Expression<Func<T, bool>> whereLambda = null, Expression<Func<T, TOrder>> orderbyLambda = null, bool isAsc = false)
        {
            var list = dbSet.AsNoTracking();
            if (whereLambda != null)
            {
                list = list.Where(whereLambda);
            }
            if (orderbyLambda != null)
            {
                if (isAsc)
                    list = list.OrderBy<T, TOrder>(orderbyLambda);
                else
                    list = list.OrderByDescending<T, TOrder>(orderbyLambda);
            }
            SqlSugarPagedList<T> pageData = new SqlSugarPagedList<T>();
            pageData.PageIndex = page.PageIndex;
            pageData.PageSize = page.PageSize;
            pageData.TotalCount = list.Count();
            pageData.Items = list.Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize).ToList();
            return pageData;
        }

        #endregion

        #region 修改
        public T Update(T entity)
        {
            dbSet.Attach(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public bool UpdateOk(T entity)
        {
            dbSet.Attach(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;

        }
 
        #endregion
    }
}
