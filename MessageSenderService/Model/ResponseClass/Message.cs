using MessageSenderService.Model.Enums;
using MessageSenderService.Model.Interfaces;
using System.Text.Json.Serialization;

namespace MessageSenderService.Model.ResponseClass
{
    public class SendMessageResult : IResponseResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "ERROR";
        [JsonPropertyName("status_code")]
        public ResponseOnSendRequest StatusCode { get; set; }
        [JsonPropertyName("sms")]
        public Dictionary<string, PhoneMessageResult> PhonesMessageResults { get; set; } = [];
        [JsonPropertyName("balance")]
        public double Balance { get; set; }


        public bool IsSuccess
        { 
            get 
            {
                return false;
            } 
        }
    }

    public class PhoneMessageResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "ERROR";
        [JsonPropertyName("status_code")]
        public ResponseOnSendRequest StatusCode { get; set; }
        [JsonPropertyName("sms_id")]
        public string? SmsId { get; set; } = null;
        [JsonPropertyName("status_text")]
        public string? StatusText { get; set; } = null;

    }

}
