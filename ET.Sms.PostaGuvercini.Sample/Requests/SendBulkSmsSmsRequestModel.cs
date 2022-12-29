namespace ET.Sms.PostaGuvercini.Sample.Requests
{
    public class SendBulkSmsSmsRequestModel
    {
        public string[] Gsm { get; set; }
        public string Text { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
