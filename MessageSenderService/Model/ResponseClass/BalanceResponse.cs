using MessageSenderService.Model.Enums;
using MessageSenderService.Model.Interfaces;
using System.Text.Json.Serialization;

namespace MessageSenderService.Model.ResponseClass
{
    public class BalanceResponse : IResponseResult
    {
        public string Status { get; set; }
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }
        public double Balance { get; set; }
        [JsonIgnore]
        public int GetHttpStatusCode => SmsRuStatusCodeConverter.GetHttpStatusCode(StatusCode);

        public string ConvertToMessage()
        {
            return string.Empty;
        }
    }
}
