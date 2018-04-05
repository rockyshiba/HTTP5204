using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace GeoPedia.Models
{
    public static class Encryptor
    {
        //Source: https://stackoverflow.com/questions/12416249/hashing-a-string-with-sha256
        public static string Sha256String(string plaintext)
        {
            HashAlgorithm crypt = new SHA256Managed();
            StringBuilder hash = new StringBuilder();
            byte[] cryptoArray = crypt.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

            foreach(byte byteItem in cryptoArray)
            {
                hash.Append(byteItem.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}