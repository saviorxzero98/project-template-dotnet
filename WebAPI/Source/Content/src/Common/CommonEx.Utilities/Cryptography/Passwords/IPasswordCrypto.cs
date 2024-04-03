namespace CommonEx.Utilities.Cryptography.Passwords
{
    public interface IPasswordCrypto
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <returns></returns>
        string Encrypt(string password);

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        bool Verify(string password, string passwordHash);
    }
}
