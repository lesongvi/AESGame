﻿using System.Security.Cryptography;

namespace AESGame.Models
{
    public class AESCryptOptions
    {
        #region Private members

        private static int MAX_ALLOWED_SALT_LEN = 255;

        private static int MIN_ALLOWED_SALT_LEN = 4;

        private static int DEFAULT_MIN_SALT_LEN = MIN_ALLOWED_SALT_LEN;
        private static int DEFAULT_MAX_SALT_LEN = 8;

        private ICryptoTransform encryptor = null;
        private ICryptoTransform decryptor = null;

        #endregion

        #region Constructor
        public AESCryptOptions()
        {
            PasswordHash = AESPasswordHash.SHA1;
            PasswordHashIterations = 1;
            MinSaltLength = 0;
            MaxSaltLength = 0;
            FixedKeySize = null;
            PaddingMode = PaddingMode.PKCS7;
        }
        #endregion

        #region Properties
        public int? FixedKeySize { get; set; }

        public AESPasswordHash PasswordHash { get; set; }

        public int PasswordHashIterations { get; set; }

        public int MinSaltLength { get; set; }

        public int MaxSaltLength { get; set; }

        public string PasswordHashSalt { get; set; }

        public PaddingMode PaddingMode { get; set; }
        #endregion

    }
}
