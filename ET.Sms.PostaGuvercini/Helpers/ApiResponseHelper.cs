using ET.Sms.PostaGuvercini.Constants;
using ET.Sms.PostaGuvercini.Models;
using System.Text;

namespace ET.Sms.PostaGuvercini.Helpers
{
    public static class ApiResponseHelper
    {
        public static ParseResponse SplitParameters(string response)
        {
            ParseResponse parseResponse = new();
            string[] splitData = response.Split('&');

            foreach (string data in splitData)
            {
                if (data.Contains(PostaGuverciniConstants.ErrorNo))
                    parseResponse.ErrorNo = data.GetParameter(PostaGuverciniConstants.ErrorNo);
                else if (data.Contains(PostaGuverciniConstants.ErrorText))
                    parseResponse.ErrorText = data.GetParameter(PostaGuverciniConstants.ErrorText);
                else if (data.Contains(PostaGuverciniConstants.MessageId))
                    parseResponse.MessageId = data.GetParameter(PostaGuverciniConstants.MessageId);
                else if (data.Contains(PostaGuverciniConstants.Charge))
                    parseResponse.Charge = data.GetParameter(PostaGuverciniConstants.Charge);
                else if (data.Contains(PostaGuverciniConstants.Status))
                    parseResponse.Status = data.GetParameter(PostaGuverciniConstants.Status);
                else if (data.Contains(PostaGuverciniConstants.Credit))
                    parseResponse.Credit = data.GetParameter(PostaGuverciniConstants.Credit);
            }

            return parseResponse;
        }
        
        public static List<ParseResponse> MultipleSplitParameters(string response)
        {
            List<ParseResponse> parseResponse = new();
            string[] splitData = response.Split('&');
            int index = -1;

            foreach (string data in splitData)
            {
                if (data.Contains(PostaGuverciniConstants.ErrorNo))
                {
                    index++;
                    parseResponse[index] = new ParseResponse();
                    parseResponse[index].ErrorNo = data.GetParameter(PostaGuverciniConstants.ErrorNo);
                }
                else if (data.Contains(PostaGuverciniConstants.ErrorText))
                {
                    parseResponse[index].ErrorText = data.GetParameter(PostaGuverciniConstants.ErrorText);
                }
                else if (data.Contains(PostaGuverciniConstants.MessageId))
                {
                    parseResponse[index].MessageId = data.GetParameter(PostaGuverciniConstants.MessageId);
                }
                else if (data.Contains(PostaGuverciniConstants.Charge))
                {
                    parseResponse[index].Charge = data.GetParameter(PostaGuverciniConstants.Charge);
                }
            }

            return parseResponse;
        }

        public static string GetParameter(this string value, string key)
        {
            var splitItem = value.Split('=');
            if (splitItem[0] == key)
            {
                if (splitItem[1].Contains("|"))
                {
                    return splitItem[1].Split('|')[0];
                }

                return splitItem[1];
            }

            return string.Empty;
        }

        public static string GetStatus(this string status)
        {
            ApiResponseConstants.StatusCodes.TryGetValue(status, out string data);
            return data;
        }

        public static string GetError(this string errorNo)
        {
            ApiResponseConstants.ErrorCodes.TryGetValue(errorNo, out string data);
            return data;
        }

        public static string GetMessageData(this string statusCode, string message)
        {
            StringBuilder sb = new();

            sb.Append(statusCode.GetError());

            if (!string.IsNullOrEmpty(message))
            {
                sb.Append($" | {message}");
            }

            return sb.ToString();
        }
    }
}
