namespace MessageSenderService.Model.Middleware
{
    /// <summary>
    /// Базовый класс для middleware
    /// </summary>
    /// <param name="request">Делегат запроса</param>
    /// <param name="environment">Окружение программы</param>
    /// <param name="logger">Логер для введения записей о действиях</param>
    public abstract class MiddlewareBase(RequestDelegate request,
        IWebHostEnvironment environment,
        ILogger logger)
    {
        public abstract Task InvokeAsync(HttpContext context);
    }
}
