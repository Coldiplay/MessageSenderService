using MediatR;
using MessageSenderService.Model.Attributes;
using MessageSenderService.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MessageSenderService.CQRS
{
    /// <summary>
    /// Комманда-запрос отправки сообщения
    /// </summary>
    public class SendMessageCommand<T> : IRequest<T> where T : IResponseResult, new()
    {
        [Required]
        public required string Telephone { get; set; }
        [Required]
        public required string Message { get; set; }

        /// <summary>
        /// Обработчик комманды-запроса
        /// </summary>
        /// <param name="messageSender">Сервис отправки сообщений</param>
        public class SendMessageCommandHandler(IMessageSender messageSender) : IRequestHandler<SendMessageCommand<T>, T>
        {
            [ServiceMethodName("SendAsync")]
            public async Task<T> Handle(SendMessageCommand<T> request, CancellationToken cancellationToken)
            {
                //Отправляем запрос через наш сервис и получаем ответ от sms.ru
                var response = await messageSender.SendAsync<T>(request.Telephone, request.Message);
                return response;
            }
        }
    }
}
