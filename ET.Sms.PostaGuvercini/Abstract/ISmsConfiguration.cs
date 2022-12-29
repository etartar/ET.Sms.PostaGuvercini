namespace ET.Sms.PostaGuvercini.Abstract
{
    public interface ISmsConfiguration
    {
        string UserName { get; }
        string Password { get; }
        string BaseUrl { get; }
        string SendSmsUrl { get; }
        string SendBulkSmsUrl { get; }
        string QuerySmsUrl { get; }
        string QueryCreditUrl { get; }
    }
}
