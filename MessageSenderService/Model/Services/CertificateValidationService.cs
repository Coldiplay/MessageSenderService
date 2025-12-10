using MessageSenderService.Model.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace MessageSenderService.Model.Services
{
    public class CertificateValidationService : ICertificateValidationService
    {
        public bool ValidateCertificate(X509Certificate2 clientCertificate)
        {
            //Доставать откуда-нибудь
            string[] allowedThumbprints = [];
            return allowedThumbprints.Contains(clientCertificate.Thumbprint);
        }
    }
}
