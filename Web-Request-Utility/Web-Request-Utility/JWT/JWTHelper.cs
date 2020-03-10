using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Security.Cryptography.X509Certificates;
using RSACng = Security.Cryptography.RSACng;
using Web_Request_Utility.JWT.Object;

namespace Web_Request_Utility.JWT
{
    public class JwtHelper
    {
        private static readonly string Sha1 = "SHA1";
        private static readonly string Sha256 = "SHA256";
        private static readonly string Sha512 = "SHA512";
        private static readonly string Sha384 = "SHA384";

        static byte[] ComputeHash(string alg, byte[] bytes)
        {
            if (alg == Sha1)
            {
                return new SHA1CryptoServiceProvider().ComputeHash(bytes);
            }
            if (alg == Sha256)
            {
                return new SHA256CryptoServiceProvider().ComputeHash(bytes);
            }
            if (alg == Sha384)
            {
                return new SHA384CryptoServiceProvider().ComputeHash(bytes);
            }
            if (alg == Sha512)
            {
                return new SHA512CryptoServiceProvider().ComputeHash(bytes);
            }
            return null;
        }
        static HashAlgorithm GetHashAlgorithm(string algorithm)
        {
            HashAlgorithm hashAlgorithm;

            if (algorithm == ("SHA1"))
            {
                hashAlgorithm = new SHA1CryptoServiceProvider();
            }
            else if (algorithm == ("SHA256"))
            {
                hashAlgorithm = new SHA256CryptoServiceProvider();
            }
            else if (algorithm == ("SHA384"))
            {
                hashAlgorithm = new SHA384CryptoServiceProvider();
            }
            else if (algorithm == ("SHA512"))
            {
                hashAlgorithm = new SHA512CryptoServiceProvider();
            }
            else
            {
                hashAlgorithm = new SHA1CryptoServiceProvider();
            }
            return hashAlgorithm;
        }

        static CngAlgorithm ConvertAlg(string alg)
        {
            if (alg == Sha1)
            {
                return CngAlgorithm.Sha1;
            }
            if (alg == Sha256)
            {
                return CngAlgorithm.Sha256;
            }
            if (alg == Sha384)
            {
                return CngAlgorithm.Sha384;
            }
            if (alg == Sha512)
            {
                return CngAlgorithm.Sha512;
            }
            return CngAlgorithm.Sha1;
        }

        public static string SignStr(string iss, X509Certificate2 cert, string alg = "SHA1")
        {
            try
            {
                string verifyString = GetVerifyString(alg, iss);
                byte[] bytes = Encoding.UTF8.GetBytes(verifyString);
                var rgbHash = ComputeHash(alg, bytes);
                byte[] sign;

                if (cert.HasCngKey())
                {
                    //使用 System.Security.Cryptography.Cng.dll 给Cng 签名
                    //var priKey = cert.GetRSAPrivateKey();
                    //sign = priKey.SignHash(rgbHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    //使用 Security.Cryptography.dll 给Cng 签名
                    var cngPrivateKey = cert.GetCngPrivateKey();
                    var rsaCng = new RSACng(cngPrivateKey);
                    sign = rsaCng.SignHash(rgbHash, ConvertAlg(alg));
                }
                else
                {
                    sign = ((RSACryptoServiceProvider)cert.PrivateKey).SignHash(rgbHash, CryptoConfig.MapNameToOID(alg));
                }
                return verifyString + "." + Convert.ToBase64String(sign);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static bool Verify(string signStr, X509Certificate2 cert, string alg = "SHA1")
        {
            string[] signStrArray = signStr.Split('.');
            var plainText = Encoding.UTF8.GetBytes($"{signStrArray[0]}.{signStrArray[1]}");
            var signData = Convert.FromBase64String(signStrArray[2]);
            var rsaProviderPublic = (RSACryptoServiceProvider) cert.PublicKey.Key;
            var hashAlg = GetHashAlgorithm(alg);
            var plainTextHash = hashAlg.ComputeHash(plainText);
            return rsaProviderPublic.VerifyHash(plainTextHash, CryptoConfig.MapNameToOID(alg), signData);
        }

        private static string GetVerifyString(string alg, string iss)
        {
            Header header = new Header()
            {
                Alg = alg,
                Typ = "jwt"
            };
            Payload payload1 = new Payload {Iss = iss };
            long ticks = DateTime.UtcNow.AddSeconds(59).Ticks;
            payload1.Exp = ticks;
            payload1.Jti = Guid.NewGuid().ToString();
            Payload payload2 = payload1;
            return header + "." + payload2;
        }
    }



}
