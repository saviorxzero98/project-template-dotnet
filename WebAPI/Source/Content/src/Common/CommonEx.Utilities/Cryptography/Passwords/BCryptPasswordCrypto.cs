namespace CommonEx.Utilities.Cryptography.Passwords
{
    public class BCryptPasswordCrypto : IPasswordCrypto
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <returns></returns>
        public string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool Verify(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
