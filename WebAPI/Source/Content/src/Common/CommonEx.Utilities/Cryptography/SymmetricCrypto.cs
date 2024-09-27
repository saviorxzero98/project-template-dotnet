using System.Security.Cryptography;
using System.Text;

namespace CommonEx.Utilities.Cryptography
{
    public class SymmetricCrypto
    {
        /// <summary>
        /// 對稱式加密演算法
        /// </summary>
        public SymmetricAlgorithmType Algorithm { get; set; }

        /// <summary>
        /// 編碼
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Mode
        /// </summary>
        public CipherMode Mode { get; set; } = CipherMode.CBC;

        /// <summary>
        /// Padding
        /// </summary>
        public PaddingMode Padding { get; set; } = PaddingMode.PKCS7;

        /// <summary>
        /// 密文是否為 Base64 字串
        /// </summary>
        public bool IsBase64Cipher { get; set; } = false;

        public SymmetricCrypto()
        {
            Algorithm = SymmetricAlgorithmType.AES;
            Padding = PaddingMode.PKCS7;
            Mode = CipherMode.CBC;
            Encoding = Encoding.UTF8;
        }
        public SymmetricCrypto(SymmetricAlgorithmType algorithm,
                               CipherMode mode = CipherMode.CBC,
                               PaddingMode padding = PaddingMode.PKCS7,
                               bool isBase64Cipher = false)
        {
            Algorithm = algorithm;
            Padding = padding;
            Mode = mode;
            IsBase64Cipher = isBase64Cipher;
            Encoding = Encoding.UTF8;
        }


        /// <summary>
        /// 設定 Encoding
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public SymmetricCrypto SetEncoding(Encoding encoding)
        {
            Encoding = encoding;
            return this;
        }

        /// <summary>
        /// Generate Key
        /// </summary>
        /// <returns></returns>
        public (byte[] key, byte[] iv) GenerateKey()
        {
            SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Mode, Padding);
            provider.GenerateKey();
            provider.GenerateIV();
            return (key: provider.Key, iv: provider.IV);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            try
            {
                using var algorithm = CreateEncryptAlgorithm(Algorithm, Mode, Padding);
                byte[] plain = Encoding.GetBytes(plainText);

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                                   algorithm.CreateEncryptor(key, iv),
                                                                   CryptoStreamMode.Write);
                cryptoStream.Write(plain, 0, plain.Length);
                cryptoStream.FlushFinalBlock();
                var cipherBytes = memoryStream.ToArray();

                if (IsBase64Cipher)
                {
                    return Base64Convert.Create().ToBase64String(cipherBytes);
                }
                else
                {
                    return HexConvert.Create().ToHexString(cipherBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            try
            {
                using var algorithm = CreateEncryptAlgorithm(Algorithm, Mode, Padding);
                byte[] cipherBytes;
                if (IsBase64Cipher)
                {
                    cipherBytes = Base64Convert.Create().FromBase64String(cipherText);
                }
                else
                {
                    cipherBytes = HexConvert.Create().FromHexString(cipherText);
                }

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                                   algorithm.CreateDecryptor(key, iv),
                                                                   CryptoStreamMode.Write);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();

                return Encoding.GetString(memoryStream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Create Crypto Algorithm
        /// </summary>
        /// <param name="algorithmType"></param>
        /// <param name="mode"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        protected SymmetricAlgorithm CreateEncryptAlgorithm(SymmetricAlgorithmType algorithmType,
                                                            CipherMode mode, PaddingMode padding)
        {
            SymmetricAlgorithm algorithm;

            switch (algorithmType)
            {
                case SymmetricAlgorithmType.Rijndael:
                    algorithm = Rijndael.Create();
                    break;

                case SymmetricAlgorithmType.RC2:
                    algorithm = RC2.Create();
                    break;

                case SymmetricAlgorithmType.DES:
                    algorithm = DES.Create();
                    break;

                case SymmetricAlgorithmType.TripleDES:
                    algorithm = TripleDES.Create();
                    break;

                case SymmetricAlgorithmType.AES:
                default:
                    algorithm = Aes.Create();
                    break;
            }

            algorithm.Mode = mode;
            algorithm.Padding = padding;
            return algorithm;
        }
    }
}
