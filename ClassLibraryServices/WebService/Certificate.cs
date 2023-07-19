using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServices.WebService
{
    internal static class Certificate
    {
        internal static bool ValidateServerCertificate(HttpRequestMessage message, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors policy)
        {
            return true;
            //return certificate.GetCertHashString() == "88D664F25FEE8A99CDA8B1041B708AE6808B061D" ? true : false;
        }
    }
}
