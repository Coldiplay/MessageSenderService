using MediatR;
using MessageSenderService.Model.Interfaces;

namespace MessageSenderService.CQRS
{
    public class GetBalanceCommand<T> : IRequest<T> where T : IResponseResult, new()
    {
        public class GetBalanceCommandHandler(IMessageSender messageSender) : IRequestHandler<GetBalanceCommand<T>, T>
        {
            public async Task<T> Handle(GetBalanceCommand<T> request, CancellationToken cancellationToken)
            {
                //Отправляем запрос через наш сервис и получаем ответ от sms.ru
                var response = await messageSender.SendAsync<T>($"my/balance?api_id={Config.SmsApi}&json=1");
                return response;
            }
        }
    }
}
