using System;
using System.Security.Cryptography;

namespace MobileProviderAPI.Helpers
{
    public static class SecurityHelper
    {
        public static string GenerateSecureSecretKey()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64]; // 512 bit key
                randomNumberGenerator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes); // Convert key to Base64 format
            }
        }
    }
}
