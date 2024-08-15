
namespace AhCha.Fortunate.EntityFrameworkCore;

public partial class SysNotice
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 类型(10公告 20通知)
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? Status { get; set; }

    public DateTime? CreateTime { get; set; }

    public long? CreateUserId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public long? UpdateUserId { get; set; }
}
