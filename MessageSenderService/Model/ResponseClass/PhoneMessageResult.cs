using MessageSenderService.Model.Enums;
using MessageSenderService.Model.Interfaces;
using System.Text.Json.Serialization;

namespace MessageSenderService.Model.ResponseClass;

public class PhoneMessageResult : IResponseResult
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = "ERROR";
    [JsonPropertyName("status_code")]
    //public ResponseOnSendRequest StatusCode { get; set; }
    public int StatusCode { get; set; }
    [JsonPropertyName("sms_id")]
    public string? SmsId { get; set; } = null;
    [JsonPropertyName("status_text")]
    public string? StatusText { get; set; } = null;

    public int GetHttpStatusCode => SmsRuStatusCodeConverter.GetHttpStatusCode(StatusCode);

    public string ConvertToMessage()
    {
        throw new NotImplementedException();
    }
}