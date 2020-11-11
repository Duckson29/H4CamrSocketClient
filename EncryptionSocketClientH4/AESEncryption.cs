using EncryptionSocketClientH4.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionSocketClientH4
{
    class AESEncryption
    {
        private Aes aesObject;

        public Aes AesObject
        {
            get { return aesObject; }
            set { aesObject = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_aesKey">the key for the decryption and encryption</param>
        /// <param name="_aesIV">the iv for the decryption and encryption</param>
        public AESEncryption(byte[] _aesKey = null, byte[] _aesIV = null)
        {
            if (_aesKey == null)
                AesObject = GenrentAes();
            else
            {
                AesObject = GenrentAes();
                AesObject.Key = (_aesKey.Length < 16) ? GenrentAes().Key : _aesKey;
                AesObject.IV = (_aesIV.Length < 32) ? GenrentAes().IV : _aesIV
                    ;
            }

        }
        public byte[] Decryption(byte[] chiperText)
        {
            ICryptoTransform decrypt = aesObject.CreateDecryptor();
            byte[] output;
            using (MemoryStream memoryStreamDecrypt = new MemoryStream(chiperText))
            {
                using (CryptoStream cryptoStreamDecrypt = new CryptoStream(memoryStreamDecrypt, decrypt, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[2048];
                    int lenght = cryptoStreamDecrypt.Read(buffer, 0, 2048);
                    output = new byte[lenght];
                    Array.Copy(buffer, 0, output, 0, lenght);

                }
            }
            return output;
        }
        /// <summary>
        /// Encryps the data.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public byte[] Encryption(byte[] plainText)
        {
            byte[] encryptedText;
            ICryptoTransform encryptor = AesObject.CreateEncryptor();

            using (MemoryStream memoryStreamEncrypt = new MemoryStream())
            {
                using (CryptoStream crytoStreamStreamEncrypt = new CryptoStream(memoryStreamEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    crytoStreamStreamEncrypt.Write(plainText, 0, plainText.Length);
                }
                encryptedText = memoryStreamEncrypt.ToArray();
            }
            return encryptedText;

        }
        /// <summary>
        /// This returns the iv and key..
        /// </summary>
        /// <returns></returns>
        public byte[][] GiveAesIVAndKey()
        {
            return new byte[][] { AesObject.Key, AesObject.IV };
        }
        /// <summary>
        /// Genreate new set of iv and key.
        /// </summary>
        /// <returns></returns>
        public Aes GenrentAes()
        {
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }
    }
}
