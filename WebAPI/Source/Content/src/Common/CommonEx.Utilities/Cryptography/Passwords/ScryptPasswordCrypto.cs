using Scrypt;

namespace CommonEx.Utilities.Cryptography.Passwords
{
    public class ScryptPasswordCrypto : IPasswordCrypto
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <returns></returns>
        public string Encrypt(string password)
        {
            var encoder = new ScryptEncoder();
            return encoder.Encode(password);
        }

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool Verify(string password, string passwordHash)
        {
            var encoder = new ScryptEncoder();
            return encoder.Compare(password, passwordHash);
        }
    }
}
