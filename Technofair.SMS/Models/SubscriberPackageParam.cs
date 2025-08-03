namespace TFSMS.Admin.Models
{
    public class SubscriberPackageParam : ISubscriberPackage
    {
        public string? CardNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? ArticleNumber { get; set; }
        public string? channelid { get; set; }
        public string? monthend { get; set; }
    }
}
