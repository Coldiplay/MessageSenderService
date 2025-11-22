using MediatR;
using MessageSenderService.Model.Interfaces;

namespace MessageSenderService.CQRS
{
    /// <summary>
    /// Комманда-запрос отправки сообщения
    /// </summary>
    public class SendMessageCommand : IRequest<object>
    {
        public string Telephone { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Обработчик комманды-запроса
        /// </summary>
        /// <param name="messageSender">Сервис отправки сообщений</param>
        public class SendMessageCommandHandler(IMessageSender messageSender) : IRequestHandler<SendMessageCommand, object>
        {
            public async Task<object> Handle(SendMessageCommand request, CancellationToken cancellationToken)
            {
                //Получаем ответ от sms.ru
                var response = await messageSender.SendAsync(request.Telephone, request.Message);
                //Потом нужно сдлеать обёртку json-а
                return response;
            }
        }
    }
}
