using System;
using System.Text;

namespace OneStopRecruitment.Helpers.TagHelpers
{
    public class Base16
    {
        public static string Encode(string Text)
        {
            char[] chars = Text.ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in chars)
            {
                stringBuilder.Append(((short)c).ToString("x"));
            }
            return stringBuilder.ToString();
        }

        public static string Decode(string Hex)
        {
            string Encoded = Hex.Replace("-", "");
            byte[] raw = new byte[Encoded.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(Encoded.Substring(i * 2, 2), 16);
            }
            return Encoding.UTF8.GetString(raw);
        }
    }
}
