using System.Security.Cryptography;
using System.Text;

namespace CommonEx.Utilities.Cryptography
{
    public class HashGenerator
    {
        public HashAlgorithmType Algorithm { get; set; } = HashAlgorithmType.SHA256;
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UrlEncodeFlag { get; set; } = false;

        public HashGenerator(HashAlgorithmType algorithm = HashAlgorithmType.SHA256, 
                             bool urlEncodeFlag = false)
        {
            Algorithm = algorithm;
            UrlEncodeFlag = urlEncodeFlag;
        }

        /// <summary>
        /// Create Hash
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public byte[] Create(string plainText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            // Hash
            using var algorithm = GetHashAlgorithm(Algorithm);
            byte[] textBytes = Encoding.GetBytes(plainText);
            byte[] hashBytes = algorithm.ComputeHash(textBytes);

            return hashBytes;
        }
        /// <summary>
        /// Create Hash + Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public byte[] Create(string plainText, string saltText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            if (string.IsNullOrEmpty(saltText))
            {
                return Create(plainText);
            }

            // Hash
            using var algorithm = GetHashAlgorithm(Algorithm);
            byte[] textBytes = Encoding.GetBytes(plainText);
            byte[] saltBytes = Encoding.GetBytes(saltText);
            byte[] saltedTextBytes = new byte[textBytes.Length + saltText.Length];

            // Concatenate password and salt
            Buffer.BlockCopy(textBytes, 0, saltedTextBytes, 0, textBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, saltedTextBytes, textBytes.Length, saltBytes.Length);

            // Hash the concatenated password and salt
            byte[] hashedBytes = algorithm.ComputeHash(saltedTextBytes);

            // Concatenate the salt and hashed password for storage
            byte[] hashedBytesWithSalt = new byte[hashedBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, hashedBytesWithSalt, 0, saltBytes.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedBytesWithSalt, saltBytes.Length, hashedBytes.Length);

            return hashedBytesWithSalt;
        }


        /// <summary>
        /// Create Hash
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string CreateToBase64String(string plainText)
        {
            var hashBytes = Create(plainText);
            return Base64Convert.Create().ToBase64String(hashBytes);
        }
        /// <summary>
        /// Create Hash + Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public string CreateToBase64String(string plainText, string saltText)
        {
            var hashBytes = Create(plainText, saltText);
            return Base64Convert.Create().ToBase64String(hashBytes);
        }


        /// <summary>
        /// Create Hash
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string CreateToHexString(string plainText)
        {
            var hashBytes = Create(plainText);
            return HexConvert.Create().ToHexString(hashBytes);
        }
        /// <summary>
        /// Create Hash + Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public string CreateToHexString(string plainText, string saltText)
        {
            var hashBytes = Create(plainText, saltText);
            return HexConvert.Create().ToHexString(hashBytes);
        }


        /// <summary>
        /// Vaildate Hash
        /// </summary>
        /// <param name="vaildateText"></param>
        /// <param name="plainText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Vaildate(string vaildateText, string plainText,
                             HashStringFormatType type = HashStringFormatType.Base64)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }
            if (string.IsNullOrEmpty(vaildateText))
            {
                throw new ArgumentNullException(nameof(vaildateText));
            }

            string hashText;
            switch (type)
            {
                case HashStringFormatType.Hex:
                    hashText = CreateToHexString(plainText);
                    break;

                case HashStringFormatType.Base64:
                default:
                    hashText = CreateToBase64String(plainText);
                    break;
            }
            return hashText.Equals(vaildateText);
        }
        /// <summary>
        /// Vaildate Hash
        /// </summary>
        /// <param name="vaildateText"></param>
        /// <param name="plainText"></param>
        /// <param name="saltText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Vaildate(string vaildateText, string plainText, string saltText,
                             HashStringFormatType type = HashStringFormatType.Base64)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }
            if (string.IsNullOrEmpty(vaildateText))
            {
                throw new ArgumentNullException(nameof(vaildateText));
            }

            string hashText;
            switch (type)
            {
                case HashStringFormatType.Hex:
                    hashText = CreateToHexString(plainText, saltText);
                    break;

                case HashStringFormatType.Base64:
                default:
                    hashText = CreateToBase64String(plainText, saltText);
                    break;
            }
            return hashText.Equals(vaildateText);
        }


        /// <summary>
        /// Get Hash Algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static HashAlgorithm GetHashAlgorithm(HashAlgorithmType algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithmType.MD5:
                    return MD5.Create();

                case HashAlgorithmType.SHA1:
                    return SHA1.Create();

                case HashAlgorithmType.SHA384:
                    return SHA384.Create();

                case HashAlgorithmType.SHA512:
                    return SHA512.Create();

                case HashAlgorithmType.SHA256:
                default:
                    return SHA256.Create();
            }
        }
    }
}
