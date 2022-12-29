using ET.Sms.PostaGuvercini.Constants;
using ET.Sms.PostaGuvercini.Helpers;

namespace ET.Sms.PostaGuvercini.Responses
{
    public class QueryCreditApiResponse : ApiResponse
    {
        public int Credit { get; set; }

        public static QueryCreditApiResponse Success(string errorNo, string credit)
        {
            return new QueryCreditApiResponse
            {
                IsSuccess = errorNo.Equals(PostaGuverciniConstants.SuccessStatusCode),
                StatusCode = errorNo,
                Message = errorNo.GetError(),
                Credit = Convert.ToInt32(credit)
            };
        }

        public static QueryCreditApiResponse Fail(string errorNo, string message, string credit)
        {
            return new QueryCreditApiResponse
            {
                IsSuccess = false,
                StatusCode = errorNo,
                Message = $"{errorNo.GetError()} | {message}",
                Credit = Convert.ToInt32(credit)
            };
        }
    }
}
