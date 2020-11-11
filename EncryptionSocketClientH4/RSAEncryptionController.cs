using EncryptionSocketClientH4.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;

namespace EncryptionSocketClientH4
{
    public class RSAEncryptionController
    {
        AESEncryption aes = new AESEncryption();
        IEncryption rsa;
        public RSAEncryptionController(IEncryption _rsa)
        {
            rsa = _rsa;
        }
        public byte[] EncryptdataWithRSA(byte[] data,byte[] modulus,byte[] exp)
        {
            return rsa.Encryption(data,exp,modulus);
        }
        public byte[] DecryptDataWithRSA(byte[] data) 
        {
            return rsa.Decryption(data,new RSAParameters());
        }
        public byte[] EncryptdataWithAES(byte[] data)
        {
            return aes.Encryption(data);
        }
        public byte[] DecryptDataWithAES(byte[] data) 
        {
            return aes.Decryption(data);
        }
    }
}
