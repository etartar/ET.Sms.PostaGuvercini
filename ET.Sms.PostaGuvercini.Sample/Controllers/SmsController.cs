using ET.Sms.PostaGuvercini.Abstract;
using ET.Sms.PostaGuvercini.Requests;
using ET.Sms.PostaGuvercini.Responses;
using ET.Sms.PostaGuvercini.Sample.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ET.Sms.PostaGuvercini.Sample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet]
        public async Task<IActionResult> QueryCredit()
        {
            QueryCreditApiResponse result = await _smsService.QueryCredit();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> QuerySms([FromQuery]string messageId)
        {
            QuerySmsApiResponse result = await _smsService.QuerySms(messageId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SendSms([FromBody] SendSmsRequestModel request)
        {
            SendSmsRequest sendSmsRequest = new SendSmsRequest(request.Gsm, request.Text, request.SendDate, request.ExpireDate);
            SendSmsApiResponse result = await _smsService.SendSms(sendSmsRequest);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SendBulkSms([FromBody] SendBulkSmsSmsRequestModel request)
        {
            SendSmsRequest sendSmsRequest = new SendSmsRequest(request.Gsm, request.Text, request.SendDate, request.ExpireDate);
            List<SendSmsApiResponse> result = await _smsService.SendBulkSms(sendSmsRequest);
            return Ok(result);
        }
    }
}