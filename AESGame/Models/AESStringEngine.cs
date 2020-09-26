using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace AESGame.Models
{
    class AESStringEngine
    {
        #region Private members
        private ICryptoTransform encryptor = null;
        private ICryptoTransform decryptor = null;

        private AESCryptOptions Options = null;

        #endregion

        #region Constructors
        public AESStringEngine(string passPhrase) :
            this(passPhrase, null)
        {
        }

        public AESStringEngine(string passPhrase,
                                string initVector) :
            this(passPhrase, initVector, new AESCryptOptions())
        {
        }

        public AESStringEngine(string passPhrase, string initVector, AESCryptOptions options)
        {
            this.Options = options;

            if (Options.FixedKeySize.HasValue
                && Options.FixedKeySize != 128
                && Options.FixedKeySize != 192
                && Options.FixedKeySize != 256)
                throw new NotSupportedException("ERROR: options.FixedKeySize must be NULL (for auto-detect) or have a value of 128, 192 or 256");

            byte[] initVectorBytes = null;

            byte[] saltValueBytes = null;

            if (initVector == null) initVectorBytes = new byte[0];
            else initVectorBytes = Encoding.UTF8.GetBytes(initVector);

            int keySize = (Options.FixedKeySize.HasValue)
                ? Options.FixedKeySize.Value
                : GetAESKeySize(passPhrase);

            if (keySize == 1024)
                return;

            byte[] keyBytes = null;
            if (Options.PasswordHash == AESPasswordHash.None)
            {
                keyBytes = System.Text.Encoding.UTF8.GetBytes(passPhrase);
            }
            else
            {
                if (Options.PasswordHashSalt == null) saltValueBytes = new byte[0];
                else saltValueBytes = Encoding.UTF8.GetBytes(options.PasswordHashSalt);

                PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                           passPhrase,
                                                           saltValueBytes,
                                                           Options.PasswordHash.ToString().ToUpper().Replace("-", ""),
                                                           Options.PasswordHashIterations);

                keyBytes = password.GetBytes(keySize / 8);
            }

            AesManaged symmetricKey = new AesManaged();

            symmetricKey.Padding = Options.PaddingMode;

            symmetricKey.Mode = (initVectorBytes.Length == 0)
                ? CipherMode.ECB
                : CipherMode.CBC;

            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }
        #endregion

        #region Encryption routines
        public string Encrypt(string plainText)
        {
            return Encrypt(Encoding.UTF8.GetBytes(plainText));
        }

        public string Encrypt(byte[] plainTextBytes)
        {
            return Convert.ToBase64String(EncryptToBytes(plainTextBytes));
        }

        public byte[] EncryptToBytes(string plainText)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(plainText));
        }

        public byte[] EncryptToBytes(byte[] plainTextBytes)
        {
            byte[] plainTextBytesWithSalt = (UseSalt()) ? AddSalt(plainTextBytes) : plainTextBytes;

            byte[] cipherTextBytes = null;

            lock (this)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {

                    using (CryptoStream cryptoStream = new CryptoStream(
                                                       memoryStream,
                                                       encryptor,
                                                        CryptoStreamMode.Write))
                    {

                        cryptoStream.Write(plainTextBytesWithSalt, 0, plainTextBytesWithSalt.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
                return cipherTextBytes;
            }
        }
        #endregion

        #region Decryption routines
        public string Decrypt(string cipherText)
        {
            return Decrypt(Convert.FromBase64String(cipherText));
        }

        public string Decrypt(byte[] cipherTextBytes)
        {
            return Encoding.UTF8.GetString(DecryptToBytes(cipherTextBytes));
        }

        public byte[] DecryptToBytes(string cipherText)
        {
            return DecryptToBytes(Convert.FromBase64String(cipherText));
        }

        public byte[] DecryptToBytes(byte[] cipherTextBytes)
        {
            byte[] decryptedBytes = null;
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;
            int saltLen = 0;

            decryptedBytes = new byte[cipherTextBytes.Length];

            lock (this)
            {
                using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(
                                                       memoryStream,
                                                       decryptor,
                                                       CryptoStreamMode.Read))
                    {

                        decryptedByteCount = cryptoStream.Read(decryptedBytes,
                                                                0,
                                                                decryptedBytes.Length);
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
            }

            if (UseSalt())
            {
                saltLen = (decryptedBytes[0] & 0x03) |
                            (decryptedBytes[1] & 0x0c) |
                            (decryptedBytes[2] & 0x30) |
                            (decryptedBytes[3] & 0xc0);
            }

            plainTextBytes = new byte[decryptedByteCount - saltLen];

            Array.Copy(decryptedBytes, saltLen, plainTextBytes,
                        0, decryptedByteCount - saltLen);

            return plainTextBytes;
        }
        #endregion

        #region Helper functions
        public static int GetAESKeySize(string passPhrase)
        {
            switch (passPhrase.Length)
            {
                case 16:
                    return 128;
                case 24:
                    return 192;
                case 32:
                    return 256;
                default:
                    return 1024;
            }
        }

        private bool UseSalt()
        {
            return (Options.MaxSaltLength > 0 && Options.MaxSaltLength >= Options.MinSaltLength);
        }


        private byte[] AddSalt(byte[] plainTextBytes)
        {
            if (!UseSalt()) return plainTextBytes;

            byte[] saltBytes = GenerateSalt(Options.MinSaltLength, Options.MaxSaltLength);

            byte[] plainTextBytesWithSalt = new byte[plainTextBytes.Length + saltBytes.Length];
            Array.Copy(saltBytes, plainTextBytesWithSalt, saltBytes.Length);

            Array.Copy(plainTextBytes, 0,
                        plainTextBytesWithSalt, saltBytes.Length,
                        plainTextBytes.Length);

            return plainTextBytesWithSalt;
        }

        private byte[] GenerateSalt(int minSaltLen, int maxSaltLen)
        {
            int saltLen = 0;

            if (minSaltLen == maxSaltLen) saltLen = minSaltLen;
            else
                saltLen = GenerateRandomNumber(minSaltLen, maxSaltLen);

            byte[] salt = new byte[saltLen];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(salt);

            salt[0] = (byte)((salt[0] & 0xfc) | (saltLen & 0x03));
            salt[1] = (byte)((salt[1] & 0xf3) | (saltLen & 0x0c));
            salt[2] = (byte)((salt[2] & 0xcf) | (saltLen & 0x30));
            salt[3] = (byte)((salt[3] & 0x3f) | (saltLen & 0xc0));

            return salt;
        }

        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            byte[] randomBytes = new byte[4];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            int seed = ((randomBytes[0] & 0x7f) << 24) |
                        (randomBytes[1] << 16) |
                        (randomBytes[2] << 8) |
                        (randomBytes[3]);

            Random random = new Random(seed);

            return random.Next(minValue, maxValue + 1);
        }
        #endregion
    }
}
