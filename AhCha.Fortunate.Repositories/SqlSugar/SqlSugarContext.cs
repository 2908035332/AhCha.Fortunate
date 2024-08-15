using SqlSugar;
using AhCha.Fortunate.Common.Global;
using AhCha.Fortunate.Common.Extensions;

namespace AhCha.Fortunate.Repositories.SqlSugar
{
    /// <summary>
    /// 生成使用（单例模式）
    /// </summary>
    public sealed class SqlSugarContext
    {
        private SqlSugarContext()
        {
        }

        public static SqlSugarScope GetInstance(DatabaseConfig config)
        {
            var entityConfig = new ConnectionConfig()
            {
                ConfigId = config.ConfigId,
                DbType = config.DbType.ToEnum<DbType>(),
                ConnectionString = config.ConnectionString,
                IsAutoCloseConnection = config.IsAutoCloseConnection,
            };
            //达梦数据库特殊配置
            if (config.DbType.ToEnum<DbType>() == DbType.Dm)
            {
                entityConfig.MoreSettings = new ConnMoreSettings()
                {
                    IsAutoToUpper = false //设置禁用自动转成大写表
                };
            }

            var instance = new SqlSugarScope(entityConfig);
            instance.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(String.Concat("SQL：", sql));

            };
            instance.Aop.DataExecuting = (oldValue, entityInfo) =>
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
            };
            return instance;
        }
    }
}
