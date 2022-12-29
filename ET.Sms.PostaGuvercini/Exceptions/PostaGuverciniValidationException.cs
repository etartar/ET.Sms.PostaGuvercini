namespace ET.Sms.PostaGuvercini.Exceptions
{
    public class PostaGuverciniValidationException : Exception
    {
        public PostaGuverciniValidationException(string message) : base(message)
        {
        }

        public PostaGuverciniValidationException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
