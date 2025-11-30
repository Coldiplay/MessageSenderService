namespace MessageSenderService.Model.Interfaces
{
    /// <summary>
    /// Интерфейс для валидаторов запросов
    /// </summary>
    /// <typeparam name="TRequest">Запрос команды, который проверяет этот валидатор</typeparam>
    public interface IValidator<in TRequest>
    {
        /// <summary>
        /// Валидирование запроса
        /// </summary>
        /// <param name="request">Запрос команды</param>
        /// <returns>Возвращает коллекцию строк с текстом, поясняющих из-за чего ошибка</returns>
        Task<IEnumerable<string>> ValidateAsync(TRequest request, CancellationToken ct);
    }
}
