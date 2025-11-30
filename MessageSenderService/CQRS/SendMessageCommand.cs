using MediatR;
using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.ResponseClass;

namespace MessageSenderService.CQRS
{
    /// <summary>
    /// Комманда-запрос отправки сообщения
    /// </summary>
    public class SendMessageCommand : IRequest<RequestResult>
    {
        public string Telephone { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Обработчик комманды-запроса
        /// </summary>
        /// <param name="messageSender">Сервис отправки сообщений</param>
        public class SendMessageCommandHandler(IMessageSender messageSender) : IRequestHandler<SendMessageCommand, RequestResult>
        {
            public async Task<RequestResult> Handle(SendMessageCommand request, CancellationToken cancellationToken)
            {
                //Получаем ответ от sms.ru
                RequestResult? response = await messageSender.SendAsync(request.Telephone, request.Message);
                //Потом нужно сдлеать обёртку json-а
                return response;
            }
        }
    }
}
