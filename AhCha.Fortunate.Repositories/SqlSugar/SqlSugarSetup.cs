using SqlSugar;
using AhCha.Fortunate.ModelsDto;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AhCha.Fortunate.Repositories.SqlSugar
{
    public static class SqlSugarSetup
    {
        /// <summary>
        /// 多库处理
        /// </summary>
        /// <param name="services"></param>
        public static void SqlSugarScopeConfigure(this IServiceCollection services)
        {
            if (AhChaFortunateGlobalContext.DatabaseConfigs == null || !AhChaFortunateGlobalContext.DatabaseConfigs.Any())
            {
                throw new Exception("请先配置数据库连接字符串");
            }

            List<ConnectionConfig> ConnectionConfigs = new List<ConnectionConfig>();
            AhChaFortunateGlobalContext.DatabaseConfigs.ForEach(DbConfigEntity =>
            {
                var entity = new ConnectionConfig()
                {
                    ConfigId = DbConfigEntity.ConfigId,
                    DbType = DbConfigEntity.DbType.ToEnum<DbType>(),
                    ConnectionString = DbConfigEntity.ConnectionString,
                    IsAutoCloseConnection = DbConfigEntity.IsAutoCloseConnection,
                };
                //达梦数据库特殊配置
                if (DbConfigEntity.DbType.ToEnum<DbType>() == DbType.Dm)
                {
                    entity.MoreSettings = new ConnMoreSettings()
                    {
                        IsAutoToUpper = false //设置禁用自动转成大写表
                    };
                }

                ConnectionConfigs.Add(entity);
            });
            SqlSugarScope sqlSugar = new SqlSugarScope(ConnectionConfigs, db =>
            {
                //输出sql,参数语句
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);
                };
                foreach (var item in ConnectionConfigs)
                {
                    var dbProvider = db.GetConnectionScope((string)item.ConfigId);
                    dbProvider.Aop.DataExecuting = (oldValue, entityInfo) =>
                    {
                        // 新增操作
                        if (entityInfo.OperationType == DataFilterType.InsertByObject)
                        {
                            // id 主键赋值
                            if (entityInfo.EntityColumnInfo.IsPrimarykey && entityInfo.EntityColumnInfo.PropertyInfo.PropertyType == typeof(long))
                            {
                                //建表统一主键id为Id，否则识别不到（多库操作id也为大写i(I),小写d）Id
                                var id = ((dynamic)entityInfo.EntityValue).Id;
                                if (id == null || id == 0)
                                {
                                    //统一赋值雪花id
                                    entityInfo.SetValue(Yitter.IdGenerator.YitIdHelper.NextId());
                                }
                            }
                            #region 通用字段赋值
                            if (entityInfo.PropertyName == "CreateTime")
                            {
                                entityInfo.SetValue(DateTime.Now);
                            }
                            if (entityInfo.PropertyName == "CreateUserId")
                            {
                                entityInfo.SetValue(CurrentUser.GetUserId);
                            }
                            #endregion
                        }

                        //修改操作
                        if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                        {
                            #region 通用字段赋值
                            if (entityInfo.PropertyName == "UpdateTime")
                            {
                                entityInfo.SetValue(DateTime.Now);
                            }
                            if (entityInfo.PropertyName == "UpdateUserId")
                            {
                                entityInfo.SetValue(CurrentUser.GetUserId);
                            }
                            #endregion
                        }

                        //删除操作（逻辑删除数据记录操作，例如谁操作了删除，操作时间）
                        if (entityInfo.OperationType == DataFilterType.DeleteByObject)
                        {

                        }
                    };
                }
            });

            services.AddSingleton<ISqlSugarClient>(sqlSugar);
            // 注册 SqlSugar 仓储
            services.AddScoped(typeof(SqlSugarRepository<>));
        }

        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<SqlSugarPagedList<TEntity>> ToPagedListAsync<TEntity>(this ISugarQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            RefAsync<int> totalCount = 0;
            var items = await query.ToPageListAsync(pageIndex, pageSize, totalCount);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return new SqlSugarPagedList<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = (int)totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }
    }

}
