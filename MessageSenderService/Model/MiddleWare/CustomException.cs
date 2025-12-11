namespace MessageSenderService.Model.MiddleWare
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; } = "Неизвестная ошибка";
    }
}
