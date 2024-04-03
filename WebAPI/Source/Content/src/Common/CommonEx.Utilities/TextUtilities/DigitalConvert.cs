using System.Text;
using System.Text.RegularExpressions;

namespace CommonEx.Utilities.TextUtilities
{
    public class DigitalConvert
    {
        /// <summary>
        /// Int to Letters (Start with 0)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string IntToLetters(int number)
        {
            const int letterCount = 26;
            const char startLetter = 'A';
            string result = string.Empty;

            if (number < 0) return result;

            do
            {
                result = (char)(startLetter + number % letterCount) + result;
                number /= letterCount;
            }
            while (--number >= 0);
            return result;
        }

        /// <summary>
        /// Letters to Int (Start with 0)
        /// </summary>
        /// <param name="text">text</param>
        /// <returns></returns>
        public static int LettersToInt(string text)
        {
            const int letterCount = 26;
            const char startLetter = 'A';
            var regex = new Regex("^[a-zA-z]+$");

            if (!regex.IsMatch(text)) return -1;

            int result = 0;
            var letters = text.ToUpper();

            foreach (char letter in letters)
            {
                result = result * letterCount + (letter - startLetter + 1);
            }
            return result - 1;
        }

        /// <summary>
        /// Int To Roman Number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string IntToRomanNumber(int number)
        {
            if (number <= 0) return string.Empty;

            Dictionary<int, string> romanNumberMap = new Dictionary<int, string>
            {
                { 1000000, "m" },
                { 500000, "d" },
                { 100000, "c" },
                { 50000, "l" },
                { 10000, "x" },
                { 5000, "v" },
                { 4000, "Mv" },
                { 1000, "M" },
                { 900, "CM" },
                { 500, "D" },
                { 400, "CD" },
                { 100, "C" },
                { 90, "XC" },
                { 50, "L" },
                { 40, "XL" },
                { 10, "X" },
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" },
            };

            var roman = new StringBuilder();

            foreach (var item in romanNumberMap)
            {
                while (number >= item.Key)
                {
                    roman.Append(item.Value);
                    number -= item.Key;
                }
            }

            return roman.ToString();
        }

        /// <summary>
        /// Roman Number To Int
        /// </summary>
        /// <param name="roman"></param>
        /// <returns></returns>
        public static int RomanNumberToInt(string roman)
        {
            var regex = new Regex("^[I|V|X|L|C|D|M|v|x|l|c|d|m]+$");
            if (!regex.IsMatch(roman)) return -1;

            Dictionary<char, int> romanNumberMap = new Dictionary<char, int>
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
                { 'v', 5000 },
                { 'x', 10000 },
                { 'l', 50000 },
                { 'c', 100000 },
                { 'd', 500000 },
                { 'm', 1000000 },
            };
            int total = 0;

            int current, previous = 0;
            char currentRoman, previousRoman = '\0';

            for (int i = 0; i < roman.Length; i++)
            {
                currentRoman = roman[i];

                previous = previousRoman != '\0' ? romanNumberMap[previousRoman] : '\0';
                current = romanNumberMap[currentRoman];

                if (previous != 0 && current > previous)
                {
                    total = total - (2 * previous) + current;
                }
                else
                {
                    total += current;
                }

                previousRoman = currentRoman;
            }

            return total;
        }
    }
}
