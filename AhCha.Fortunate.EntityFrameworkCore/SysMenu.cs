
namespace AhCha.Fortunate.EntityFrameworkCore;

public partial class SysMenu
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// 菜单标题
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 菜单图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 路由名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 路由地址
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// 组件路径
    /// </summary>
    public string Component { get; set; } = null!;

    /// <summary>
    /// 是否隐藏
    /// </summary>
    public bool IsHide { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }

    /// <summary>
    /// 是否内嵌网址
    /// </summary>
    public bool IsIframe { get; set; }

    /// <summary>
    /// 是否固定(不允许关闭)
    /// </summary>
    public bool IsAffix { get; set; }

    /// <summary>
    /// 是否外部连接(新标签打开)
    /// </summary>
    public bool IsLink { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    public string? Redirect { get; set; }

    /// <summary>
    /// 外部或者Iframe网址
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 访问的api控制器名称
    /// </summary>
    public string? ApiTag { get; set; }

    /// <summary>
    /// 操作权限
    /// </summary>
    public string? Permission { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public long? UpdateUserId { get; set; }
}
