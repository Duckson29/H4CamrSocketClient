using EncryptionSocketClientH4.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionSocketClientH4
{
    public class RSAEncrpytion : IEncryption
    {

        RSACryptoServiceProvider RSA;
        RSAParameters privateKey;
        readonly RSAParameters publicKey;
        public RSAParameters PublicKey { get => publicKey; }
        public RSAParameters PrivateKey { get => privateKey; set => privateKey = value; }

        bool padding = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CreateKeyPair">if true it creates a new key pair for use in a rsa encrytpion</param>
        public RSAEncrpytion(bool CreateKeyPair = true)
        {
            if (CreateKeyPair)
            {
                RSA = new RSACryptoServiceProvider(2048);
                PrivateKey = RSA.ExportParameters(true);
                publicKey = RSA.ExportParameters(false);
            }
        }

        /// <summary>
        /// for use when you have a rsaParameters as in when you are creating a rsa key pair..
        /// </summary>
        /// <param name="PrimeKeyA">Public key value</param>
        /// <param name="PrimeKeyB">private Key value</param>
        /// <param name="EncrytpionStrength"></param>
        /// <returns></returns>
        byte[] Encryption(byte[] Data, RSAParameters RSAKey)
        {
            try
            {
                RSAParameters key = (RSAKey.D == null) ? RSAKey : PrivateKey;
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(key);
                    encryptedData = RSA.Encrypt(Data, padding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// For use when you only kown the public key and modulus.
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="PublicKey"> The public key in a byte[]</param>
        /// <param name="Modulus">The Modulus to use in a byte[]</param>
        /// <returns></returns>
        public byte[] Encryption(byte[] Data, byte[] PublicKey, byte[] Modulus)
        {
            return Encryption(Data, new RSAParameters
            {
                Exponent = (PublicKey == null) ? null : PublicKey,
                Modulus = (Modulus == null) ? null : Modulus
            }); ;
        }
        /// <summary>
        /// For use when you need to decrypt useing the private key.
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="RSAKey"></param>
        /// <returns>a byte[] containing the decrypted data</returns>
        public byte[] Decryption(byte[] Data, RSAParameters RSAKey)
        {
            try
            {
                RSAParameters dPrivateKey = (RSAKey.D == null) ? RSAKey : PrivateKey;
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(dPrivateKey);
                    decryptedData = RSA.Decrypt(Data, padding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
