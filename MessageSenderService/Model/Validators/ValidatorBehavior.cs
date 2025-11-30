using MediatR;
using MessageSenderService.Model.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MessageSenderService.Model.Validators
{
    /// <summary>
    /// Обработчик валидаторов
    /// </summary>
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators) 
        {
            _validators = validators;
        }

        /// <summary>
        /// Метод обрабатывающий запрос
        /// </summary>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Если нет валидаторов, то просто запускаем следующий запрос
            if (!_validators.Any())
                return await next();

            //Список ошибок для вывода
            List<string> failures = [];

            foreach (var validator in _validators)
            {
                //На каждом валидаторе проверяем запросы
                var failuresInValidator = await validator.ValidateAsync(request, cancellationToken);
                //Если ответ не пуст, то в ошибки записываем все ошибки, которые были в валидаторе
                failures.AddRange(failuresInValidator);
            }
            //При ошибке/ошибках кидаем исключение валидации с сообщением в виде всех ошибок
            if (failures.Count > 0)
                throw new ValidationException(string.Join("; ", failures));
            else
                //Если всё ок, то просто запускаем следующий запрос
                return await next();
        }
    }
}
