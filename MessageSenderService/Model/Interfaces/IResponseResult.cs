namespace MessageSenderService.Model.Interfaces
{
    public interface IResponseResult
    {
        string Status { get; set; }
        int StatusCode { get; set; }
        int GetHttpStatusCode { get; }
        string ConvertToMessage();
    }
}
