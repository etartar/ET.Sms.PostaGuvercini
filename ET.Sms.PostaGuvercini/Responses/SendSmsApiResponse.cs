using ET.Sms.PostaGuvercini.Constants;
using ET.Sms.PostaGuvercini.Helpers;

namespace ET.Sms.PostaGuvercini.Responses
{
    public class SendSmsApiResponse : ApiResponse
    {
        public string MessageId { get; set; }
        public int Charge { get; set; }

        public static SendSmsApiResponse Success(string errorNo, string messageId, string charge)
        {
            return new SendSmsApiResponse
            {
                IsSuccess = errorNo.Equals(PostaGuverciniConstants.SuccessStatusCode),
                StatusCode = errorNo,
                MessageId = messageId,
                Message = errorNo.GetError(),
                Charge = Convert.ToInt32(charge)
            };
        }

        public static SendSmsApiResponse Fail(string errorNo, string message, string charge)
        {
            return new SendSmsApiResponse
            {
                IsSuccess = false,
                StatusCode = errorNo,
                Message = $"{errorNo.GetError()} | {message}",
                Charge = Convert.ToInt32(charge)
            };
        }
    }
}
