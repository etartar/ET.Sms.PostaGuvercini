using ET.Sms.PostaGuvercini.Exceptions;
using System.Text;
using System.Web;

namespace ET.Sms.PostaGuvercini.Requests
{
    public class SendSmsRequest
    {
        private SendSmsRequest()
        {
        }

        public SendSmsRequest(string gsm, string text, DateTime? sendDate = null, DateTime? expireDate = null)
        {
            if (text.Length > 480)
                throw new PostaGuverciniValidationException("Gönderilecek SMS içeriği. 480 karakter (3 SMS) den fazla olmamalıdır.");

            Gsm = new string[] { gsm };
            Text = text;
            SendDate = sendDate;
            ExpireDate = expireDate;
        }

        public SendSmsRequest(string[] gsm, string text, DateTime? sendDate = null, DateTime? expireDate = null)
        {
            if (text.Length > 480)
                throw new PostaGuverciniValidationException("Gönderilecek SMS içeriği. 480 karakter (3 SMS) den fazla olmamalıdır.");

            Gsm = gsm;
            Text = text;
            SendDate = sendDate;
            ExpireDate = expireDate;
        }

        /// <summary>
        /// Alıcının GSM numarası. 5321112233 formatında olmalıdır. Başına 0 konmamalı ve arada boşluk bırakılmamalıdır.
        /// </summary>
        public string[] Gsm { get; set; }

        /// <summary>
        /// Gönderilecek SMS içeriği. 480 karakter (3 SMS) den fazla olmamalıdır.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Mesajın gönderilmesi istenen (Send dt) tarih saat bilgisi. Formatı şöyle olmalıdır YYYYAAGG SS:HH. 
        /// Örn. 20080616 15:30
        /// Opsiyonel bilgidir.
        /// </summary>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Mesajın son gönderilmesi istenen (Expire dt) tarih saat bilgisi. Formatı şöyle olmalıdır YYYYAAGG SS:HH.
        /// Örn. 20080616 15:30
        /// Opsiyonel bilgidir.
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        #region Methods

        public string QueryParams
        {
            get
            {
                StringBuilder sb = new();

                sb.Append(ConcatGsmNumbers);

                string message = HttpUtility.UrlEncode(Text, Encoding.GetEncoding("ISO-8859-9"));

                sb.Append($"&text={message}");

                if (SendDate.HasValue)
                {
                    string dt = SendDate.Value.ToString("yyyyMMdd HH:mm");
                    sb.Append($"&dt={dt}");
                }

                if (ExpireDate.HasValue)
                {
                    string dt2 = ExpireDate.Value.ToString("yyyyMMdd HH:mm");
                    sb.Append($"&dt2={dt2}");
                }

                return sb.ToString();
            }
        }

        private string ConcatGsmNumbers
        {
            get
            {
                string gsmNumbers = string.Empty;

                foreach (string gsmNumber in Gsm)
                    gsmNumbers += $"&gsm={gsmNumber}";

                return gsmNumbers;
            }
        }

        #endregion
    }
}