using MessageSenderService.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MessageSenderService.Model.Middleware
{
    /// <summary>
    /// Класс для отловли ошибок происходящих в запросах
    /// </summary>
    /// <param name="request">Делегат запроса</param>
    /// <param name="environment">Окружение программы</param>
    /// <param name="logger">Логгер для логирования в консоль</param>
    public class GlobalExceptionMiddleWare(RequestDelegate request, 
        IWebHostEnvironment environment, 
        ILogger<GlobalExceptionMiddleWare> logger) 
        : MiddlewareBase(request, environment, logger), IExceptionMiddleWare
    {
        public static int GetErrorCode(Exception exception) => exception switch
        {
            CustomException customException => customException.ErrorCode,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException or ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        public static string GetErrorMessage(Exception exception) => exception switch
        {
            UnauthorizedAccessException => "Доступ запрещён: требуется аутентификация.",
            KeyNotFoundException => "Запрашиваемый ресурс не найден.",
            ArgumentException => "Некорректные входные данные.",
            ValidationException => "Ошибка валидации данных.",
            CustomException customException => customException.ErrorMessage,
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

        public override async Task InvokeAsync(HttpContext httpContext)//, RequestDelegate next)
        {
            try
            {
                // Пробуем запустить запрос в контексте
                await request.Invoke(httpContext);
            }
            //Отлавливаем возможную ошибку
            catch (Exception ex)
            {
                //Логируем ошибку в консоль
                logger.LogError(ex, "Произошла непредвиденная ошибка. {httpContext.Request.Method} {httpContext.Request.Path}",
                    httpContext.Request.Method, httpContext.Request.Path);
                //Запускаем метод для отправки ошибки клиенту
                await HandleExceptionAsync(httpContext, ex, environment.IsDevelopment());
            }
        }
    }
}
