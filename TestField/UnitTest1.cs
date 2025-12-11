using MessageSenderService.Model.ResponseClass;
using System.Net.Http.Json;

namespace TestField
{
    [TestFixture]
    public class Tests
    {
        private static HttpClient _httpClient = new();
        [SetUp]
        public void Setup()
        {
            var hostUri = "localhost:7280";

            _httpClient = new HttpClient() 
            { 
                BaseAddress = new Uri($"https://{hostUri}/api/SmsRu/")
            };
        }

        [Test]
        public async Task GetBalanceTest()
        {
            var response = await _httpClient.GetFromJsonAsync<BalanceResponse>("GetBalance");
            Console.WriteLine(response);
            Assert.That(response, Is.Not.Null);
        }

        
        [TestCase("79510004964", "Message")]
        [TestCase("+79510004964", "Message")]
        //[TestCase("9510004964", "Message")] // Пока не работает, ибо regex требует цифру региона
        public async Task SendMessageTest(string phone, string message)
        {
            phone = Uri.EscapeDataString(phone);
            message = Uri.EscapeDataString(message);
            var response = await _httpClient.GetAsync($"SendMessage?telephone={phone}&message={message}");
            var responseMessage = await response.Content.ReadFromJsonAsync<SendMessageResponse>();
            Console.WriteLine(responseMessage);
            Assert.Multiple(() => 
            {
                Assert.That(responseMessage, Is.Not.Null);
                Assert.That(responseMessage!.Status, Is.Not.EqualTo("ERROR"));
                Assert.That(responseMessage.GetHttpStatusCode, Is.EqualTo(200));
            });
        }

        [TestCase("+79510004964", "")]
        [TestCase("myphone", "Message")]
        [TestCase("+795100049640", "Message")]
        ///<summary>Проверка валидации данных (чтобы плохие данные не проходили в запросе)</summary>
        public async Task SendMessageValidationTest(string phone, string message)
        {
            phone = Uri.EscapeDataString(phone);
            message = Uri.EscapeDataString(message);
            var response = await _httpClient.GetAsync($"SendMessage?telephone={phone}&message={message}");
            var responseMessage = await response.Content.ReadFromJsonAsync<SendMessageResponse>();
            Console.WriteLine(responseMessage);

            Assert.That(responseMessage is null || 
                responseMessage.Status.Equals("ERROR") ||
                responseMessage.GetHttpStatusCode >= 400);
        }



        [TearDown]
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
