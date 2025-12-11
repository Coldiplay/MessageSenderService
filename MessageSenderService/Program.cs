using MediatR;
using MessageSenderService.CQRS;
using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.MiddleWare;
using MessageSenderService.Model.ResponseClass;
using MessageSenderService.Model.Services;
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
            _ = builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            //Добавление медиатора
            _ = builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterGenericHandlers = true;
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            //Добавление самого сервиса отправки сообщений
            _ = builder.Services.AddScoped<IMessageSender, MessageSender>();

            //Добавление синглтона клиента для отправки сообщения через sms.ru
            _ = builder.Services.AddSingleton<HttpClient>(_ =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(Config.SmsBaseAddress)
                };
                return httpClient;
            });

            //Добавление валидатора для комманды
            _ = builder.Services.AddTransient<IValidator<SendMessageCommand<SendMessageResponse>>, ValidatorSendMessageCommand<SendMessageResponse>>();

            _ = builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            _ = builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                _ = app.MapScalarApiReference();
                _ = app.MapOpenApi();
            }

            _ = app.UseHttpsRedirection();

            _ = app.UseAuthorization();

            //Использование глобального отловщика ошибок
            _ = app.UseMiddleware<GlobalExceptionMiddleWare>();

            _ = app.MapControllers();

            await app.RunAsync();
        }
    }
}
