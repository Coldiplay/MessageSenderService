namespace MessageSenderService.Model.Interfaces
{
    public interface IResponseResult 
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public int GetHttpStatusCode { get; }
        public string ConvertToMessage();
    }
}
