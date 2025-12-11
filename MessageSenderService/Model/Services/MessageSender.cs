using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.MiddleWare;

namespace MessageSenderService.Model.Services
{
    /// <summary>
    /// Сервис взаимодействия с sms.ru
    /// </summary>
    /// <param name="client">Клиент подключения к sms.ru</param>
    public class MessageSender(HttpClient client) : IMessageSender
    {
        private readonly HttpClient _httpClient = client;

        /// <summary>
        /// Отправка запроса на sms.ru
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns>Класс ответа, указанный при вызове метода</returns>
        /// <exception cref="ArgumentNullException">При фееричном сценарии, что мы не смогли отправить запрос на sms.ru из-за непредвиденных обстоятельств</exception>
        public async Task<TResponse> SendAsync<TResponse>(string requestUri) where TResponse : IResponseResult, new()
        {
            //Отправляем запрос на sms.ru, получаем json ответ и конвертируем его в класс ответа
            var response = await _httpClient.GetAsync(requestUri);
            TResponse? resultFromJson = await response.Content.ReadFromJsonAsync<TResponse>();
            if (!response.IsSuccessStatusCode || resultFromJson is null)
            {
                // TODO: создать обработку кастомных ошибок
                throw new CustomException();
            }

            return resultFromJson;
        }
    }
}
