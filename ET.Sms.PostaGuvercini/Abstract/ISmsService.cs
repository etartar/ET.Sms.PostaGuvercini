using ET.Sms.PostaGuvercini.Requests;
using ET.Sms.PostaGuvercini.Responses;

namespace ET.Sms.PostaGuvercini.Abstract
{
    public interface ISmsService
    {
        /// <summary>
        /// Sms gönderme
        /// </summary>
        /// <param name="gsm"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<SendSmsApiResponse> SendSms(SendSmsRequest request);

        /// <summary>
        /// Toplu Sms gönderme
        /// </summary>
        /// <param name="gsm"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        Task<List<SendSmsApiResponse>> SendBulkSms(SendSmsRequest request);

        /// <summary>
        /// Sms durum sorgulama
        /// Bu istek ile gönderdiğiniz SMS in teslim edilme durumu sorgulanabilir
        /// </summary>
        /// <returns></returns>
        Task<QuerySmsApiResponse> QuerySms(string messageId);

        /// <summary>
        /// Sms durum sorgulama
        /// Bu istek ile gönderdiğiniz SMS in teslim edilme durumu sorgulanabilir
        /// </summary>
        /// <returns></returns>
        Task<QueryCreditApiResponse> QueryCredit();
    }
}
