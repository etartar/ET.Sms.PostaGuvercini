using ET.Sms.PostaGuvercini.Abstract;
using Microsoft.Extensions.Options;

namespace ET.Sms.PostaGuvercini.Sample.Configurations
{
    public class SmsConfiguration : ISmsConfiguration
    {
        private readonly PostaGuverciniConfig _appConfig;

        public SmsConfiguration(IOptionsSnapshot<PostaGuverciniConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }

        public string UserName => _appConfig.UserName;

        public string Password => _appConfig.Password;

        public string BaseUrl => _appConfig.BaseUrl;

        public string SendSmsUrl => _appConfig.SendSmsUrl;

        public string SendBulkSmsUrl => _appConfig.SendBulkSmsUrl;

        public string QuerySmsUrl => _appConfig.QuerySmsUrl;

        public string QueryCreditUrl => _appConfig.QueryCreditUrl;
    }
}
