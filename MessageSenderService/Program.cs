using MediatR;
using MessageSenderService.CQRS;
using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.MiddleWare;
using MessageSenderService.Model.ResponseClass;
using MessageSenderService.Model.Validators;
using Scalar.AspNetCore;
using System.Reflection;

namespace MessageSenderService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Добавление обработчика валидаторов
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            
            //Добавление медиатора
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //Добавление самого сервиса отправки сообщений
            builder.Services.AddScoped<IMessageSender, Model.Services.MessageSender>();

            //Добавление синглтона клиента для отправки сообщения через sms.ru
            builder.Services.AddSingleton<HttpClient>(opt =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(Config.SMS_BASE_ADRESS)
                };
                return httpClient;
            });

            //Добавление валидатора для комманды
            builder.Services.AddTransient<IValidator<SendMessageCommand<RequestResult>>, ValidatorSendMessageCommand<RequestResult>>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Использование глобального отловщика ошибок
            app.UseMiddleware<GlobalExceptionMiddleWare>();

            app.MapControllers();

            app.Run();
        }
    }
}
