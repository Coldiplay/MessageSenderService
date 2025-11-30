using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessageSenderService
{
    public static class Config
    {
        static Config()
        {
            var runningInContainer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
            var runningInDotNet = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            bool inAspNetCore = !string.IsNullOrEmpty(runningInDotNet) &&
                               (runningInDotNet.Equals("Development", StringComparison.OrdinalIgnoreCase) || runningInDotNet == "1");
            bool loadInDocker = !string.IsNullOrEmpty(runningInContainer) &&
                           (runningInContainer.Equals("true", StringComparison.OrdinalIgnoreCase) || runningInContainer == "1");
            if (!loadInDocker && !inAspNetCore) return;
            SmsApi = Environment.GetEnvironmentVariable("SMS_API_KEY") ?? string.Empty;
            SmsBaseAddress = Environment.GetEnvironmentVariable("SMS_BASE_ADDRESS") ?? string.Empty;
            AuthentificationKey = Environment.GetEnvironmentVariable("AUTHENTIFICATE_KEY") ?? string.Empty;
        }
        //При тесте/запуске указать api-id от sms.ru
        public static string SmsApi { get; private set; } = null!;

        public static string SmsBaseAddress { get; private set; } = null!;

        //static string KEY { get; } = "hjkdsfbgvlsijhcwhaenrtivgvhszshjkngasdfgdfgqwlkne";
        private static readonly string AuthentificationKey = null!;

        public static SymmetricSecurityKey GetSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthentificationKey));
    }
}
