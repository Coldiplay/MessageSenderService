using MediatR;
using MessageSenderService.Model.Attributes;
using MessageSenderService.Model.Interfaces;

namespace MessageSenderService.CQRS
{
    public class CheckBalanceCommand<T> : IRequest<T> where T : IResponseResult, new()
    {
        public class CheckBalanceCommandHandler(IMessageSender messageSender) : IRequestHandler<CheckBalanceCommand<T>, T>
        {
            [ServiceMethodName("CheckBalanceAsync")]
            public async Task<T> Handle(CheckBalanceCommand<T> request, CancellationToken cancellationToken)
            {
                //Отправляем запрос через наш сервис и получаем ответ от sms.ru
                var response = await messageSender.CheckBalanceAsync<T>();
                return response;
            }
        }
    }
}
