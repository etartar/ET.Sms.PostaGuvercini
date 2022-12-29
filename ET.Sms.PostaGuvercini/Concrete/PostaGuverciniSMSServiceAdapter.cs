using ET.Sms.PostaGuvercini.Abstract;
using ET.Sms.PostaGuvercini.Helpers;
using ET.Sms.PostaGuvercini.Models;
using ET.Sms.PostaGuvercini.Requests;
using ET.Sms.PostaGuvercini.Responses;

namespace ET.Sms.PostaGuvercini.Concrete
{
    public class PostaGuverciniSMSServiceAdapter : ServiceAdapterBase, ISmsService
    {
        private readonly ISmsConfiguration _smsConfiguration;

        public PostaGuverciniSMSServiceAdapter(IHttpClientFactory client, ISmsConfiguration smsConfiguration) : base(client)
        {
            _smsConfiguration = smsConfiguration;
        }

        public async Task<SendSmsApiResponse> SendSms(SendSmsRequest request)
        {
            DefaultParameters();

            string url = $"{_smsConfiguration.SendSmsUrl}{BaseData}{request.QueryParams}";

            HttpResponseMessage result = await SendAsync(HttpMethod.Get, url);
            string content = await result.Content.ReadAsStringAsync();
            ParseResponse response = ApiResponseHelper.SplitParameters(content);

            if (result.IsSuccessStatusCode)
            {
                return SendSmsApiResponse.Success(response.ErrorNo, response.MessageId, response.Charge);
            }
            else
            {
                return SendSmsApiResponse.Fail(response.ErrorNo, response.ErrorText, response.Charge);
            }
        }

        public async Task<List<SendSmsApiResponse>> SendBulkSms(SendSmsRequest request)
        {
            List<SendSmsApiResponse> smsResult = new();

            DefaultParameters();

            string url = $"{_smsConfiguration.SendBulkSmsUrl}{BaseData}{request.QueryParams}";

            HttpResponseMessage result = await SendAsync(HttpMethod.Get, url);
            string content = await result.Content.ReadAsStringAsync();
            ParseResponse response = ApiResponseHelper.SplitParameters(content);
            List<ParseResponse> multipleResponse = ApiResponseHelper.MultipleSplitParameters(content);

            if (result.IsSuccessStatusCode)
            {
                multipleResponse.ForEach(r =>
                {
                    smsResult.Add(SendSmsApiResponse.Success(r.ErrorNo, r.MessageId, r.Charge));
                });
            }
            else
            {
                smsResult.Add(SendSmsApiResponse.Fail(response.ErrorNo, response.ErrorText, response.Charge));
            }

            return smsResult;
        }

        public async Task<QuerySmsApiResponse> QuerySms(string messageId)
        {
            DefaultParameters();

            string url = $"{_smsConfiguration.QuerySmsUrl}{BaseData}&message_id={messageId}";

            HttpResponseMessage result = await SendAsync(HttpMethod.Get, url);
            string content = await result.Content.ReadAsStringAsync();
            ParseResponse response = ApiResponseHelper.SplitParameters(content);

            if (result.IsSuccessStatusCode)
            {
                return QuerySmsApiResponse.Success(response.ErrorNo, response.Status);
            }
            else
            {
                return QuerySmsApiResponse.Fail(response.ErrorNo, response.ErrorText, response.Status);
            }
        }

        public async Task<QueryCreditApiResponse> QueryCredit()
        {
            DefaultParameters();

            string url = $"{_smsConfiguration.QueryCreditUrl}{BaseData}";

            HttpResponseMessage result = await SendAsync(HttpMethod.Get, url);
            string content = await result.Content.ReadAsStringAsync();
            ParseResponse response = ApiResponseHelper.SplitParameters(content);

            if (result.IsSuccessStatusCode)
            {
                return QueryCreditApiResponse.Success(response.ErrorNo, response.Credit);
            }
            else
            {
                return QueryCreditApiResponse.Fail(response.ErrorNo, response.ErrorText, response.Credit);
            }
        }

        #region Private Methods

        private void DefaultParameters()
        {
            ApiBaseUrl = _smsConfiguration.BaseUrl;
            ContentType = ContentTypeTextPlain;
        }

        private string BaseData
        {
            get
            {
                return $"?user={_smsConfiguration.UserName}&password={_smsConfiguration.Password}";
            }
        }

        #endregion
    }
}
