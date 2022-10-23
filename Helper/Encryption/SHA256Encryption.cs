using System.Security.Cryptography;
using System.Text;

namespace Helper
{
    public class SHA256Encryption
    {
        public static string Encrypt(string RawData)
        {
            SHA256 hash = SHA256.Create();
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(RawData));
            string res = "";
            for (int i = 0; i < data.Length; i++)
            {
                res += data[i].ToString("x2").ToUpperInvariant();
            }
            return res;
        }

        public static bool Compare(string Encrypted, string PlainText)
        {
            return Encrypt(PlainText).Equals(Encrypted);
        }
    }
}
