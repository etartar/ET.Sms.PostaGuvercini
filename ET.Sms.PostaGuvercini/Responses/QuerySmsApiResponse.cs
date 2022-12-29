using ET.Sms.PostaGuvercini.Constants;
using ET.Sms.PostaGuvercini.Helpers;

namespace ET.Sms.PostaGuvercini.Responses
{
    public class QuerySmsApiResponse : ApiResponse
    {
        public string Status { get; set; }

        public static QuerySmsApiResponse Success(string errorNo, string status)
        {
            return new QuerySmsApiResponse
            {
                IsSuccess = errorNo.Equals(PostaGuverciniConstants.SuccessStatusCode),
                StatusCode = errorNo,
                Message = status.GetStatus(),
                Status = status
            };
        }

        public static QuerySmsApiResponse Fail(string errorNo, string message, string status)
        {
            return new QuerySmsApiResponse
            {
                IsSuccess = false,
                StatusCode = errorNo,
                Message = $"{status.GetStatus()} | {message}",
                Status = status
            };
        }
    }
}
