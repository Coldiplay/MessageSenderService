using MessageSenderService.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MessageSenderService.Model.MiddleWare
{
    /// <summary>
    /// Класс для отловли ошибок происходящих в запросах
    /// </summary>
    /// <param name="request">Делегат запроса</param>
    /// <param name="environment">Окружение запроса</param>
    /// <param name="logger">Логгер для логгирования в консоль</param>
    public class GlobalExceptionMiddleWare(RequestDelegate request, IWebHostEnvironment environment, ILogger<GlobalExceptionMiddleWare> logger) : IExceptionMiddleWare
    {
        private readonly RequestDelegate _requestDelegate = request;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ILogger<GlobalExceptionMiddleWare> _logger = logger;
        public static int GetErrorCode(Exception exception) => exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        public static string GetErrorMessage(Exception exception) => exception switch
        {
            UnauthorizedAccessException => "Доступ запрещён: требуется аутентификация.",
            KeyNotFoundException => "Запрашиваемый ресурс не найден.",
            ArgumentException => "Некорректные входные данные.",
            ValidationException => "Ошибка валидации данных.",
            _ => "Произошла внутренняя ошибка сервера."
        };

        public static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, bool isDevelopment)
        {
            int code = GetErrorCode(exception);
            string errorMessage = GetErrorMessage(exception);
            //Задаём ответу статус код
            httpContext.Response.StatusCode = code;

            //Создаём анонимный класс для превращения его в json для тела ответа
            var response = new 
            {
                code,
                errorMessage,
                details = exception.Message
            };
            
            //Отправляем тело ответа
            await httpContext.Response.WriteAsJsonAsync(response);
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Пробуем запустить запрос в контексте
                await _requestDelegate.Invoke(httpContext);
            }
            //Отлавливаем возможную ошибку
            catch (Exception ex)
            {
                //Логгируем ошибку в консоль
                _logger.LogError(ex, "Произошла непредвиденная ошибка. {httpContext.Request.Method} {httpContext.Request.Path}",
                    httpContext.Request.Method, httpContext.Request.Path);
                //Запускаем метод для отправки ошибки клиенту
                await HandleExceptionAsync(httpContext, ex, _environment.IsDevelopment());
            }
        }
    }
}
