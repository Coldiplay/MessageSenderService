using MessageSenderService.Model.Enums;
using System.Text.Json.Serialization;

namespace MessageSenderService.Model.ResponseClass
{
    public class RequestResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "ERROR";
        [JsonPropertyName("status_code")]
        public ResponseOnSendRequest Status_Code { get; set; }
        [JsonPropertyName("sms")]
        public Dictionary<string, MessageResult> PhonesMessageResults { get; set; } = [];
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

    public class MessageResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = "ERROR";
        [JsonPropertyName("status_code")]
        public ResponseOnSendRequest Status_Code { get; set; }
        [JsonPropertyName("sms_id")]
        public string? Sms_ID { get; set; } = null;
        [JsonPropertyName("status_text")]
        public string? Status_Text { get; set; } = null;

    }

}
