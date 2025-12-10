using MessageSenderService.Model.Attributes;
using MessageSenderService.Model.Interfaces;
using MessageSenderService.Model.Middleware;
using MessageSenderService.Model.ResponseClass;

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
        [ReturnType(typeof(Task<SendMessageResult>))]
        public async Task<TResponse> SendAsync<TResponse>(string telephone, string message) where TResponse : IResponseResult, new()
        {
            //Отправляем сообщение через sms.ru, получаем json ответ и конвертируем его в класс ответа
            return await GetFromJson<TResponse>($"sms/send?api_id={Config.SmsApi}&to={telephone}&msg={message}&json=1");
        }

        [ReturnType(typeof(BalanceResponse))]
        public async Task<TResponse> CheckBalanceAsync<TResponse>() where TResponse : IResponseResult, new()
        {
            //Отправляем сообщение через sms.ru, получаем json ответ и конвертируем его в класс ответа
            return await GetFromJson<TResponse>($"my/balance?api_id={Config.SmsApi}&json=1");
        }

        private async Task<TResponse> GetFromJson<TResponse>(string uri) where TResponse : IResponseResult, new()
        {
            var response = await _httpClient.GetAsync(uri);
            TResponse? resultFromJson = await response.Content.ReadFromJsonAsync<TResponse>();
            if (!response.IsSuccessStatusCode || resultFromJson is null)
            {
                throw new CustomException();
            }
            return resultFromJson;
        }
    }
}
