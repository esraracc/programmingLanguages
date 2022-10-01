using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Core.Security.EmailAuthenticator
{

    public class EmailAuthenticatorHelper : IEmailAuthenticatorHelper
    {
        private byte[] GetRandomBytes(int size)
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[size];
                randomNumberGenerator.GetBytes(randomBytes);

                return randomBytes;
            }
        }
        public Task<string> CreateEmailActivationKey()
        {
            var key = Convert.ToBase64String(GetRandomBytes(64));
            return Task.FromResult(key);
        }

        public Task<string> CreateEmailActivationCode()
        {
            string code = RandomNumberGenerator.GetInt32(Convert.ToInt32(Math.Pow(10, 6))).ToString().PadLeft(6, '0');
            return Task.FromResult(code);
        }
    }
}