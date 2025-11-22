using MessageSenderService.Model.Interfaces;

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
        /// <returns>Json-объект ответа sms.ru</returns>
        /// <exception cref="ArgumentNullException">При фееричном сценарии, что мы не смогли отправить запрос на sms.ru из-за непредвиденных обстоятельств</exception>
        public async Task<object> SendAsync(string telephone, string message)
        {
            //Отправляем сообщение через sms.ru и получаем json ответ
            var response = await _httpClient.GetFromJsonAsync<object>($"sms/send?api_id={Config.SMS_API}&to={telephone}&msg={message}&json=1");

            if (response is null)
                throw new ArgumentNullException("Не удалось подключится к сервису отправки сообщений");

            return response;
        }
    }
}
