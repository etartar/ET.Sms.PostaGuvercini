using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace ET.Sms.PostaGuvercini.Concrete
{
    public class ServiceAdapterBase
    {
        private readonly IHttpClientFactory _client;
        protected readonly string ContentTypeJson = "application/json";
        protected readonly string ContentTypeTextPlain = "text/plain";

        public ServiceAdapterBase(IHttpClientFactory client)
        {
            _client = client;
        }

        protected string ApiBaseUrl { get; set; }
        protected string ContentType { get; set; }

        protected HttpClient CreateClient()
        {
            HttpClient client = _client.CreateClient();

            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));

            return client;
        }

        protected JsonSerializerSettings GetJsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    Culture = CultureInfo.InvariantCulture,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    Converters = new List<JsonConverter>
                    {
                        new StringEnumConverter()
                    }
                };
            }
        }

        protected string GetRequestData(object value)
        {
            return JsonConvert.SerializeObject(value, GetJsonSerializerSettings);
        }

        protected async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, string requestData = "")
        {
            using HttpClient client = CreateClient();
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
            if (!string.IsNullOrEmpty(requestData))
            {
                request.Content = new StringContent(requestData, Encoding.UTF8, ContentType);
            }

            return await client.SendAsync(request);
        }
    }
}
