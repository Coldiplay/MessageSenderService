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
        //public ResponseOnSendRequest StatusCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int StatusCode { get; set; }
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

        public int GetHttpStatusCode => SmsRuStatusCodeConverter.GetHttpStatusCode(StatusCode);

        public string ConvertToMessage()
        {
            throw new NotImplementedException();
        }
    }
}
