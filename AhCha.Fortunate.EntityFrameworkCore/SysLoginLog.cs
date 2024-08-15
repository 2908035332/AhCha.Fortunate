
namespace AhCha.Fortunate.EntityFrameworkCore;

public partial class SysLoginLog
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long DeptId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// UA
    /// </summary>
    public string? Uastr { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 设备
    /// </summary>
    public string? Device { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime? LoginTime { get; set; }
}
