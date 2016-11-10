using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad3Affine
{
    class Program
    {
        private static string explicitTextPath = "Plik.txt";
        private static string keysFile = "Key.txt";

        private static Dictionary<char, int> alphabet = new Dictionary<char, int>
        {
            {'a', 0 },
            {'b', 1 },
            {'c', 2 },
            {'d', 3 },
            {'e', 4 },
            {'f', 5 },
            {'g', 6 },
            {'h', 7 },
            {'i', 8 },
            {'j', 9 },
            {'k', 10 },
            {'l', 11 },
            {'m', 12 },
            {'n', 13 },
            {'o', 14 },
            {'p', 15 },
            {'q', 16 },
            {'r', 17 },
            {'s', 18 },
            {'t', 19 },
            {'u', 20 },
            {'v', 21 },
            {'w', 22 },
            {'x', 23 },
            {'y', 24 },
            {'z', 25 }
        };

        private static int a;
        private static int b;

        static void Main(string[] args)
        {
            string explicitText = GetExplicitTextFromFile();
            int[] keys = GetKeysFromFile();
            a = keys[0];
            b = keys[1];

            if (nwd(a, alphabet.Count) == 1)
            {
                string encodedText = Encode(explicitText);

                Console.WriteLine(encodedText);
                Console.WriteLine(Decode(encodedText));
            }
            else
            {
                Console.WriteLine("Blad");
            }

            Console.ReadLine();
        }

        private static string Encode(string text)
        {
            StringBuilder encodedText = new StringBuilder();

            foreach (var item in text)
            {
                int secretCharIndex = ((a * alphabet[item]) + b) % alphabet.Count;

                encodedText.Append(alphabet.FirstOrDefault(x => x.Value == secretCharIndex).Key);
            }

            return encodedText.ToString();
        }

        private static string Decode(string text)
        {
            StringBuilder decodedText = new StringBuilder();

            foreach (var item in text)
            {
                int temp = 0;

                for (int x = 0; x < alphabet.Count; x++)
                {
                    if ((a * x) % alphabet.Count == 1)
                        temp = x;
                }

                var index = alphabet[item];
                var newIndex = (temp * (index - b)) % alphabet.Count;

                if (newIndex < 0)
                {
                    newIndex += alphabet.Count;
                }
                
                decodedText.Append(alphabet.FirstOrDefault(x => x.Value == newIndex).Key);
            }

            return decodedText.ToString();
        }

        private static string GetExplicitTextFromFile()
        {
            if (File.Exists(explicitTextPath))
            {
                return File.ReadAllText(explicitTextPath);
            }

            return String.Empty;
        }

        private static int[] GetKeysFromFile()
        {
            var keys = Enumerable.Empty<int>();

            if (File.Exists(keysFile))
            {
                keys = File.ReadAllText(keysFile).Split(' ').Select(x => Convert.ToInt32(x));
            }

            return keys.ToArray();
        }

        private static int nwd(int a, int b)
        {
            int c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            return a;
        }
    }
}
