using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatasanoCryptoChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        private static void SingleByteXORDeCipher(string ResultText)
        {
            for (char SingleChar = '0'; SingleChar <= 'z'; SingleChar++)
            {
                string phrase = SingleCharXOR(ResultText, SingleChar);
                Console.WriteLine(SingleChar + ". : " + phrase + " | " + CalcLetterFreqs(phrase));
            }
        }

        private static string SingleCharXOR(string ResultText, char SingleChar)
        {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ResultText.Length; i++)
                {
                    sb.Append((char)(ResultText[i] ^ SingleChar));
                }
                return string.Format("{0}", sb);
        }

        private static string GetXOR(string str01, string str02)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str01.Length; i++)
            {
                sb.Append((char)(str01[i] ^ str02[i]));
            }
            return string.Format("{0}", sb);
        }

        private static string ConvertText2Hex(string PlainText)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            char[] values = PlainText.ToCharArray();
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                string hexOutput = String.Format("{0:X}", value);
                sb.Append(hexOutput);
            }
            return string.Format("{0}",sb);
        }

        private static string ConvertHex2String(string HexValues)
        {
            byte[] raw = new byte[HexValues.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                try
                {
                    raw[i] = Convert.ToByte(HexValues.Substring(i * 2, 2), 16);

                }
                catch (Exception)
                {
                    return "Error during conversion.";
                }
            }
            return Encoding.ASCII.GetString(raw);
        }


        private static string ConvertString2Base64(string PlainText)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(PlainText));
        }

        private static string CalcLetterFreqs(string input)
        {
            var freqs = input
                .Where(c => Char.IsLetter(c))
                .GroupBy(c => Char.ToUpper(c))                    
                .ToDictionary(g => g.Key, g => g.Count());

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (var item in freqs.OrderByDescending(x => x.Value))
            {
                sb.Append(string.Format("{0}",item.Key,item.Value));
            }
            var result = string.Format("{0}", sb);
            return result.Substring(0, Math.Min(result.Length, 7));
        }
    }
}
