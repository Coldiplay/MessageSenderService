using MediatR;
using MessageSenderService.Model.Interfaces;

namespace MessageSenderService.CQRS
{
    /// <summary>
    /// Комманда-запрос отправки сообщения
    /// </summary>
    public class SendMessageCommand<TResponse> : IRequest<TResponse> where TResponse : class, new()
    {
        public string Telephone { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Обработчик комманды-запроса
        /// </summary>
        /// <param name="messageSender">Сервис отправки сообщений</param>
        public class SendMessageCommandHandler(IMessageSender messageSender) : IRequestHandler<SendMessageCommand<TResponse>, TResponse>
        {
            public async Task<TResponse> Handle(SendMessageCommand<TResponse> request, CancellationToken cancellationToken)
            {
                //Получаем ответ от sms.ru
                var response = await messageSender.SendAsync<TResponse>(request.Telephone, request.Message);
                //Потом нужно сдлеать обёртку json-а
                return response;
            }
        }
    }
}
