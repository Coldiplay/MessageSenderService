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
        /// <returns>Возвращает ответ от отправки сообщения</returns>
        Task<TResponse> SendAsync<TResponse>(string telephone, string message) where TResponse : class, new();
    }
}
