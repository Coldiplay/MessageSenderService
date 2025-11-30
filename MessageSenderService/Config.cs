using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessageSenderService
{
    public class Config
    {
        //При тесте/запуске указать api-id от sms.ru
        public const string SMS_API = "5753D32B-35C2-E394-7157-A20BD70B99A8";
        public const string SMS_BASE_ADRESS = "https://sms.ru/";
        const string KEY = "hjkdsfbgvlsijhcwhaenrtivgvhszshjkngasdfgdfgqwlkne";

        public static SymmetricSecurityKey GetSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
