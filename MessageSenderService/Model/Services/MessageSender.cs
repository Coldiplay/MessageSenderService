using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.MiddleWare;

namespace MessageSenderService.Model.Services
{
    /// <summary>
    /// Сервис отправки сообщений
    /// </summary>
    /// <param name="client">Клиент подключения к sms.ru</param>
    public class MessageSender(HttpClient client) : IMessageSender
    {
        private readonly HttpClient _httpClient = client;

        /// <summary>
        /// Отправка текстового сообщения на указанный телефон
        /// </summary>
        /// <param name="telephone">Телефон, на который приходит сообщение</param>
        /// <param name="message">Отправляемое сообщение</param>
        /// <returns>Класс ответа, указанный при вызове метода</returns>
        /// <exception cref="ArgumentNullException">При фееричном сценарии, что мы не смогли отправить запрос на sms.ru из-за непредвиденных обстоятельств</exception>
        public async Task<TResponse> SendAsync<TResponse>(string telephone, string message) where TResponse : IResponseResult, new()
        {
            //Отправляем сообщение через sms.ru, получаем json ответ и конвертируем его в класс ответа
            var response = await _httpClient.GetAsync($"sms/send?api_id={Config.SmsApi}&to={telephone}&msg={message}&json=1");
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
