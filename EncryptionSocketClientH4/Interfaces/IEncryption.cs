using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionSocketClientH4.Interfaces
{
    public interface IEncryption
    {
        byte[] Encryption(byte[] Data, byte[] PublicKey, byte[] Modulus);
        byte[] Decryption(byte[] Data, RSAParameters RSAKey);
    }
}
