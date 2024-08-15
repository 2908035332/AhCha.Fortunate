using SqlSugar;

namespace AhCha.Fortunate.Entity.MSSQL
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SysMenu")]
    [TenantAttribute(ConstConfigId.MSSQLAhChaFortunate)]
    public partial class SysMenu
    {

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Desc:父级ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long? ParentId { get; set; }

        /// <summary>
        /// Desc:菜单标题
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:菜单图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Icon { get; set; }

        /// <summary>
        /// Desc:路由名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:路由地址
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Path { get; set; }

        /// <summary>
        /// Desc:组件路径
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Component { get; set; }

        /// <summary>
        /// Desc:是否隐藏
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsHide { get; set; }

        /// <summary>
        /// Desc:是否缓存
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsKeepAlive { get; set; }

        /// <summary>
        /// Desc:是否内嵌网址
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsIframe { get; set; }

        /// <summary>
        /// Desc:是否固定(不允许关闭)
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsAffix { get; set; }

        /// <summary>
        /// Desc:是否外部连接(新标签打开)
        /// Default:
        /// Nullable:False
        /// </summary>           
        public bool IsLink { get; set; }

        /// <summary>
        /// Desc:重定向地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Redirect { get; set; }

        /// <summary>
        /// Desc:外部或者Iframe网址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Url { get; set; }

        /// <summary>
        /// Desc:菜单类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Type { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Sort { get; set; }

        /// <summary>
        /// Desc:访问的api控制器名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ApiTag { get; set; }

        /// <summary>
        /// Desc:操作权限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Permission { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? CreateUserId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public long? UpdateUserId { get; set; }

    }
}
