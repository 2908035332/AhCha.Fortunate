
namespace AhCha.Fortunate.EntityFrameworkCore;

/// <summary>
/// 数据字典数据
/// </summary>
public partial class SysDictDatum
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 类型ID
    /// </summary>
    public long TypeId { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 字典值1
    /// </summary>
    public string? Value1 { get; set; }

    /// <summary>
    /// 字典值2
    /// </summary>
    public string? Value2 { get; set; }

    /// <summary>
    /// 字典值3
    /// </summary>
    public string? Value3 { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? Status { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人id
    /// </summary>
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 修改人id
    /// </summary>
    public long? UpdateUserId { get; set; }
}
