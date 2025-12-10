namespace MessageSenderService.Model.Middleware
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = "Неизвестная ошибка";
        //public int HttpErrorCode { get; set; }
    }
}
