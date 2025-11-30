namespace MessageSenderService.Model.MiddleWare
{
    public class CustomException : Exception
    {
        public int Error_Code { get; set; }
        public string Error_Message { get; set; }
    }
}
