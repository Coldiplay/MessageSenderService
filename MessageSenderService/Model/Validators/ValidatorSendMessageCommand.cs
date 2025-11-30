using MessageSenderService.CQRS;
using MessageSenderService.Model.Interfaces;
using System.Text.RegularExpressions;

namespace MessageSenderService.Model.Validators
{
    /// <summary>
    /// Валидатор команды отправки сообщения
    /// </summary>
    public partial class ValidatorSendMessageCommand<TResponse> : IValidator<SendMessageCommand<TResponse>> where TResponse : class, new()
    {
        /// <summary>
        /// Валидируем комманду отправки сообщения
        /// </summary>
        /// <param name="request">Наша комманда-запрос</param>
        /// <param name="ct"></param>
        /// <returns>Список строк-ошибок</returns>
        public async Task<IEnumerable<string>> ValidateAsync(SendMessageCommand<TResponse> request, CancellationToken ct)
        {
            List<string> fails = [];
            //Если сообщение пустое или состоит только из пробелов
            if (string.IsNullOrEmpty(request.Message.Trim()))
                fails.Add("Нельзя отправить пустое сообщение");
            //Если сообщение больше 160 символов
            else if (request.Message.Length > 160)
                fails.Add("Нельзя отправить сообщение больше 160 символов");

            /*
             * Если телефон не совпадает по шаблону
             * Принимает телефон в виде:
             * +70000000000
             * (Остальные примеры тоже могут иметь +)
             * 71234567890
             * 7 123 456 7890
             * 7 123 456 7890
             * 7 123.456.7890
             * 7(123)456-7890
            */
            if (!Regex.IsMatch(request.Telephone, @"^(?:\+?)(?:[1-9])(?:\s?)(?:\(?)(?<AreaCode>\d{3})(?:[\).\s]?)(?<Prefix>\d{3})(?:[-\.\s]?)(?<Suffix>\d{4})(?!\d)"))
                    fails.Add("Неверно введён телефон");
            else
                //Если телефон был принят, то просто убираем символ +, принимая только цифры
                request.Telephone = Regex.Replace(request.Telephone, @"\D", "");

            return fails;
        }
    }
}
