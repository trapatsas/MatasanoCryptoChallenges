using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MatasanoCryptoChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set 1 - Challenge 1
            Console.WriteLine("");
            Console.WriteLine("Set 1 - Challenge 1");
            Console.WriteLine("===================");
            // Solution
            var s01c01 = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            Console.WriteLine(string.Format("{0}", ConvertString2Base64(ConvertHex2String(s01c01))));

            // Set 1 - Challenge 2
            Console.WriteLine("");
            Console.WriteLine("Set 1 - Challenge 2");
            Console.WriteLine("===================");
            // Solution
            var s01c02 = "1c0111001f010100061a024b53535009181c";
            var s01c02_b = "686974207468652062756c6c277320657965";
            Console.WriteLine(string.Format("{0}", GetXOR(ConvertHex2String(s01c02), ConvertHex2String(s01c02_b))));

            // Set 1 - Challenge 3
            Console.WriteLine("");
            Console.WriteLine("Set 1 - Challenge 3");
            Console.WriteLine("===================");
            // Solution
            var s01c03 = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
            foreach (var item in SingleByteXORDeCipher(ConvertHex2String(s01c03)))
            {
                Console.WriteLine(item);
            }

            // Set 1 - Challenge 4
            Console.WriteLine("");
            Console.WriteLine("Set 1 - Challenge 4");
            Console.WriteLine("===================");
            // Solution
            string fileLocation = "http://cryptopals.com/static/challenge-data/4.txt";
            WebClient client = new WebClient();
            using (var stream = client.OpenRead(fileLocation))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        foreach (var item in SingleByteXORDeCipher(ConvertHex2String(line)))
                        {
                                Console.WriteLine(item);
                        }
                    }
                }
            }

            // Set 1 - Challenge 5
            Console.WriteLine("");
            Console.WriteLine("Set 1 - Challenge 5");
            Console.WriteLine("===================");
            // Solution

            var ch05 = "Burning 'em, if you ain't quick and nimble I go crazy when I hear a cymbal";

            var c_key = "ICE";

            var n = ch05.Length / c_key.Length;
            var d = ch05.Length % c_key.Length;


            var x = c_key.Substring(0, d) ;
            var y = string.Concat(Enumerable.Repeat(c_key, n)) + x;

            Console.WriteLine(ConvertText2Hex(GetXOR(ch05, y)));


            // END
            Console.ReadKey();
        }

        private static IEnumerable<string> SingleByteXORDeCipher(string ResultText)
        {
            for (char SingleChar = '0'; SingleChar <= 'z'; SingleChar++)
            {
                string phrase = SingleCharXOR(ResultText, SingleChar);
                if (!NonsenseFilter(phrase))
                {
                    yield return string.Format("Char: \"{0}\", Phrase: \"{1}\"", SingleChar, phrase); //SingleChar + ". : " + phrase + " | " + CalcLetterFreqs(phrase);                
                }
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
            return string.Format("{0}", sb);
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
                sb.Append(string.Format("{0}", item.Key, item.Value));
            }
            var result = string.Format("{0}", sb);
            return result.Substring(0, Math.Min(result.Length, 7));
        }

        private static bool NonsenseFilter(string text)
        {
            const int MaxAnsiCode = 122;
            const int MinAnsiCode = 65;
            string s = text.Replace(" ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            return s.Any(c => c > MaxAnsiCode || c < MinAnsiCode);
        }
    }
}
