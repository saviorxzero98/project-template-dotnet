using System.Security.Cryptography;
using System.Text;

namespace CommonEx.Utilities.Cryptography
{
    public class HmacGenerator
    {
        public HashAlgorithmType Algorithm { get; set; } = HashAlgorithmType.SHA256;
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UrlEncodeFlag { get; set; } = false;

        public HmacGenerator(HashAlgorithmType algorithm = HashAlgorithmType.SHA256,
                             bool urlEncodeFlag = false)
        {
            Algorithm = algorithm;
            UrlEncodeFlag = urlEncodeFlag;
        }

        /// <summary>
        /// Create Hmac
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <returns></returns>
        public byte[] Create(string plainText, string secretText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            // Hash
            using var algorithm = GetHashAlgorithm(Algorithm, Encoding.GetBytes(secretText));
            byte[] textBytes = Encoding.GetBytes(plainText);
            byte[] hashBytes = algorithm.ComputeHash(textBytes);

            return hashBytes;
        }
        /// <summary>
        /// Create Hmac + Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public byte[] Create(string plainText, string secretText, string saltText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            if (string.IsNullOrEmpty(saltText))
            {
                return Create(plainText, secretText);
            }

            // Hash
            using var algorithm = GetHashAlgorithm(Algorithm, Encoding.GetBytes(secretText));
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
        /// Create Hmac
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <returns></returns>
        public string CreateToBase64String(string plainText, string secretText)
        {
            var hashBytes = Create(plainText, secretText);
            return Base64Convert.Create().ToBase64String(hashBytes);
        }
        /// <summary>
        /// Create Hmac + Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public string CreateToBase64String(string plainText, string secretText, string saltText)
        {
            var hashBytes = Create(plainText, secretText, saltText);
            return Base64Convert.Create().ToBase64String(hashBytes);
        }


        /// <summary>
        /// Create Hmac
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <returns></returns>
        public string CreateToHexString(string plainText, string secretText)
        {
            var hashBytes = Create(plainText, secretText);
            return HexConvert.Create().ToHexString(hashBytes);
        }
        /// <summary>
        /// Create Hmac + Salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <param name="saltText"></param>
        /// <returns></returns>
        public string CreateToHexString(string plainText, string secretText, string saltText)
        {
            var hashBytes = Create(plainText, secretText, saltText);
            return HexConvert.Create().ToHexString(hashBytes);
        }


        /// <summary>
        /// Vaildate Hmac
        /// </summary>
        /// <param name="vaildateText"></param>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Vaildate(string vaildateText, string plainText, string secretText,
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
                    hashText = CreateToHexString(plainText, secretText);
                    break;

                case HashStringFormatType.Base64:
                default:
                    hashText = CreateToBase64String(plainText, secretText);
                    break;
            }
            return hashText.Equals(vaildateText);
        }
        /// <summary>
        /// Vaildate Hmac + Salt
        /// </summary>
        /// <param name="vaildateText"></param>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <param name="saltText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Vaildate(string vaildateText, string plainText, string secretText, string saltText,
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
                    hashText = CreateToHexString(plainText, secretText, saltText);
                    break;

                case HashStringFormatType.Base64:
                default:
                    hashText = CreateToBase64String(plainText, secretText, saltText);
                    break;
            }
            return hashText.Equals(vaildateText);
        }


        /// <summary>
        /// Get Hash Algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="secretBytes"></param>
        /// <returns></returns>
        protected HashAlgorithm GetHashAlgorithm(HashAlgorithmType algorithm, byte[] secretBytes)
        {
            switch (algorithm)
            {
                case HashAlgorithmType.MD5:
                    return new HMACMD5(secretBytes);

                case HashAlgorithmType.SHA1:
                    return new HMACSHA1(secretBytes);

                case HashAlgorithmType.SHA384:
                    return new HMACSHA384(secretBytes);

                case HashAlgorithmType.SHA512:
                    return new HMACSHA512(secretBytes);

                case HashAlgorithmType.SHA256:
                default:
                    return new HMACSHA256(secretBytes);
            }
        }
    }
}
