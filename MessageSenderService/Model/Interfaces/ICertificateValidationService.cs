using System.Security.Cryptography.X509Certificates;

namespace MessageSenderService.Model.Interfaces
{
    /// <summary>
    /// Интерфейс для обозначения сервиса валидирования сертификата
    /// </summary>
    public interface ICertificateValidationService
    {
        bool ValidateCertificate(X509Certificate2 clientCertificate);
    }
}
