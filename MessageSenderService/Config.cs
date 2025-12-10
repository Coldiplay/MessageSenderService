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
            List<bool> conditions = [];
            //Запущен ли в разработке
            conditions.Add(!string.IsNullOrEmpty(runningInDotNet) &&
                               (runningInDotNet.Equals("Development", StringComparison.OrdinalIgnoreCase) || runningInDotNet == "1"));

            // Запущен ли в докере
            conditions.Add(!string.IsNullOrEmpty(runningInContainer) &&
                           (runningInContainer.Equals("true", StringComparison.OrdinalIgnoreCase) || runningInContainer == "1"));

            //Если все условия не соблюдены, то не обращаемся к окружению
            if (!conditions.Any(t => t)) return;

            SmsApi = Environment.GetEnvironmentVariable("SMS_API_KEY") ?? string.Empty;
            SmsBaseAddress = Environment.GetEnvironmentVariable("SMS_BASE_ADDRESS") ?? "https://sms.ru/";
            AuthentificationKey = Environment.GetEnvironmentVariable("AUTHENTIFICATION_KEY") ?? string.Empty;
            CertificateFriendlyName = Environment.GetEnvironmentVariable("CERTIFICATE_NAME") ?? string.Empty;
        }
        //При тесте/запуске указать api-id от sms.ru
        public static string SmsApi { get; private set; } = null!;

        public static string SmsBaseAddress { get; private set; } = null!;

        public static readonly string AuthentificationKey = null!;

        public static readonly string CertificateFriendlyName = null!;


        /* Test area
         * //Сертификат
            var rsa = RSA.Create(2048);
            string subject = "CN-localhost";
            var certificateRequest = new CertificateRequest(subject, rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            certificateRequest.CertificateExtensions.Add(
                new X509BasicConstraintsExtension(
                    certificateAuthority: false, 
                    hasPathLengthConstraint: false, 
                    pathLengthConstraint: 0, 
                    critical: true)
                );

            certificateRequest.CertificateExtensions.Add(
                new X509KeyUsageExtension(
                    keyUsages: X509KeyUsageFlags.DigitalSignature 
                    | X509KeyUsageFlags.KeyEncipherment, 
                    critical: false)
                );

            certificateRequest.CertificateExtensions.Add(
                new X509SubjectKeyIdentifierExtension(
                    key: certificateRequest.PublicKey, 
                    critical: false)
                );
            certificateRequest.CertificateExtensions.Add(
                new X509Extension(
                    new AsnEncodedData(
                        "Subject Alternative Name",
                        new byte[] { 48, 11, 130, 9, 108, 111, 99, 97, 108, 104, 111, 115, 116 }),
                    false)
                );
            var certificate = certificateRequest.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddMinutes(5));

            //var exportableCertificate = new X509Certificate2(
            //    certificate.Export(X509ContentType.Cert),
            //    (string)null,
            //    X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet
            //    ).CopyWithPrivateKey(rsa);
            X509Certificate2 exportCert = X509CertificateLoader.LoadPkcs12(
                data: certificate.Export(X509ContentType.Cert), 
                password: (string)null, 
                keyStorageFlags: X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet
                ).CopyWithPrivateKey(rsa);

            exportCert.FriendlyName = "Test Certificate";

            var passwordForCertificateProtection = new SecureString();
            foreach (var @char in "p@ssw0rd")
            {
                passwordForCertificateProtection.AppendChar(@char);
            }

            // Export certificate to a file.
            File.WriteAllBytes(
                "certificateForServerAuthorization.pfx",
                exportCert.Export(
                    X509ContentType.Pfx,
                    passwordForCertificateProtection
                )
            );
         */


    }
}
