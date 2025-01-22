namespace CommonEx.Utilities.Cryptography
{
    public enum HashAlgorithmType { MD5, SHA1, SHA256, SHA384, SHA512 }
    public enum SymmetricAlgorithmType { DES, TripleDES, AES, RC2 }
    public enum AsymmetricAlgorithmType { RSA }
    public enum EncryptedStringEncode { Raw, Base64, Hex }
    public enum HashStringFormatType { Base64, Hex }
}
