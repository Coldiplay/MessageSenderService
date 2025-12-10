namespace MessageSenderService.Model.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса отправки сообщений
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// Метод отправляет сообщение на указанный телефон
        /// </summary>
        /// <param name="telephone">Телефон, на который придёт сообщение</param>
        /// <param name="message">Сообщение</param>
        /// <typeparam name="TResponse">Класс ответа, наследуемый от IResponseResult</typeparam>
        /// <returns>Возвращает ответ от отправки сообщения</returns>
        Task<TResponse> SendAsync<TResponse>(string telephone, string message) where TResponse : IResponseResult, new();
        /// <summary>
        /// Метод проверяет оставшийся баланс
        /// </summary>
        /// <typeparam name="TResponse">Класс ответа, наследуемый от IResponseResult</typeparam>
        /// <returns>Класс ответа с значениями в ответе запроса</returns>
        Task<TResponse> CheckBalanceAsync<TResponse>() where TResponse : IResponseResult, new();
    }
}
