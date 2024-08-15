namespace AhCha.Fortunate.ModelsDto.MSSQL.SysNoticeDto
{
    public class SysNoticeInput 
    {
        public long Id { get; set; }
                   
    }

    public class QuerySysNoticeInput : PageInputBase
    {
       
    }

    public class AddSysNoticeInput: SysNoticeInput
    {
        public string Title { get; set; }
        public object Content { get; set; }
        public string Type { get; set; }
        public bool? Status { get; set; }

    }

    public class PutSysNoticeInput : SysNoticeInput
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public bool? Status { get; set; }

    }


    public class DeleteSysNoticeInput 
    {
        public long Id { get; set; }
    }

}
