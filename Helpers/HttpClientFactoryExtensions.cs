// HttpClientFactoryExtensions.cs
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public static class HttpClientFactoryExtensions
{
    public static void IgnoreSSLErrors(this HttpClient client)
    {
        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
            delegate { return true; });
    }
}
