using CryptoHelper;

namespace CommonEx.Utilities.Cryptography.Passwords
{
    public class PBKDF2PasswordCrypto : IPasswordCrypto
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <returns></returns>
        public string Encrypt(string password)
        {
            return Crypto.HashPassword(password);
        }

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool Verify(string password, string passwordHash)
        {
            return Crypto.VerifyHashedPassword(passwordHash, password);
        }
    }
}
