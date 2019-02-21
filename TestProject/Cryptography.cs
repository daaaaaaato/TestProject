using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TestProject
{
    public class Cryptography
    {
        public static string SHA256(string text)
        {
            var crypt = new SHA256Managed();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }

        public static string SHA512(string text)
        {
            var crypt = new SHA512Managed();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }
    }
}