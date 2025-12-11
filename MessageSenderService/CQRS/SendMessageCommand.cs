using MediatR;
using MessageSenderService.Model.Interfaces;

namespace MessageSenderService.CQRS
{
    /// <summary>
    /// Комманда-запрос отправки сообщения
    /// </summary>
    public class SendMessageCommand<T> : IRequest<T> where T : IResponseResult, new()
    {
        public required string Telephone { get; set; }
        public required string Message { get; set; }

        /// <summary>
        /// Обработчик комманды-запроса
        /// </summary>
        /// <param name="messageSender">Сервис отправки сообщений</param>
        public class SendMessageCommandHandler(IMessageSender messageSender) : IRequestHandler<SendMessageCommand<T>, T>
        {
            public async Task<T> Handle(SendMessageCommand<T> request, CancellationToken cancellationToken)
            {
                //Получаем ответ от sms.ru
                var response = await messageSender.SendAsync<T>($"sms/send?api_id={Config.SmsApi}&to={request.Telephone}&msg={request.Message}&json=1");
                //Потом нужно сдлеать обёртку json-а
                return response;
            }
        }
    }
}
