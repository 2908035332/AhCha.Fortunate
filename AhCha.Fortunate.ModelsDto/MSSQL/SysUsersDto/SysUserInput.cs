
namespace AhCha.Fortunate.ModelsDto.MSSQL.SysUsersDto
{
    public class SysUserInput
    {
        public long Id { get; set; }
    }

    public class QuerySysUsersInput : PageInputBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartQueryTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndQueryTime { get; set; }
    }

    public class AddSysUserInput
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public int? UserState { get; set; }
    }

    public class PutSysUserInput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public long RoleId { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public int? UserState { get; set; }
    }


    public class PutSysUserPwdInput
    {
        public string OldPassWord { get; set; }
        public string NewPassWord { get; set; }
    }

    public class DeleteSysUserInput : SysUserInput
    {


    }


}
