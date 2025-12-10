using MediatR;
using MessageSenderService.CQRS;
using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.Middleware;
using MessageSenderService.Model.ResponseClass;
using MessageSenderService.Model.Services;
using MessageSenderService.Model.Validators;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Scalar.AspNetCore;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MessageSenderService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);
            var builder = WebApplication.CreateBuilder(args);
            /*
            //Загрузка сертификата
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            var certificate = store.Certificates.OfType<X509Certificate2>()
                .First(c => c.FriendlyName == "Test Certificate");
            store.Close();


            //builder.WebHost.UseKestrel(options =>
            //{
            //    options.Listen(System.Net.IPAddress.Loopback, 44321, listenOptions =>
            //    {
            //        var connectionOptions = new HttpsConnectionAdapterOptions() 
            //        { 
            //            ServerCertificate = certificate
            //        };
            //        listenOptions.UseHttps(connectionOptions);
            //    });
            //});


            // Add services to the container
            builder.Services.AddScoped<ICertificateValidationService, CertificateValidationService>();

            builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate(options => 
            {
                options.AllowedCertificateTypes = CertificateTypes.SelfSigned;
                options.Events = new CertificateAuthenticationEvents()
                {
                    OnCertificateValidated = context =>
                    {
                        var validationService = context.HttpContext.RequestServices.GetService<ICertificateValidationService>();

                        if (validationService.ValidateCertificate(context.ClientCertificate))
                            context.Success();
                        else
                            context.Fail("invalid Certificate");

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Fail("invalid Certificate");
                        return Task.CompletedTask;
                    }
                };

            });

            builder.Services.Configure<KestrelServerOptions>(options => 
            {
                options.ConfigureHttpsDefaults(options =>
                {
                    options.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                });
            });
            */

            //Добавление обработчика валидаторов
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            //Добавление медиатора
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterGenericHandlers = true;
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            //Добавление самого сервиса отправки сообщений
            builder.Services.AddScoped<IMessageSender, MessageSender>();

            

            //Добавление синглтона клиента для отправки сообщения через sms.ru
            builder.Services.AddSingleton<HttpClient>(_ =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(Config.SmsBaseAddress)
                };
                return httpClient;
            });

            //Добавление валидатора для комманды
            builder.Services.AddTransient<IValidator<SendMessageCommand<SendMessageResult>>, ValidatorSendMessageCommand<SendMessageResult>>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            //Использование глобального отловщика ошибок
            app.UseMiddleware<GlobalExceptionMiddleWare>();
            //app.UseMiddleware<MiddlewareTest>();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
