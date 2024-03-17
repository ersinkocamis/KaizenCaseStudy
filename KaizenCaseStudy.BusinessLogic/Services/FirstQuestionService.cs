using KaizenCaseStudy.BusinessLogic.Interfaces;
using System.Security.Cryptography;

namespace KaizenCaseStudy.BusinessLogic.Services
{
    public class FirstQuestionService : IFirstQuestionService
    {
        private readonly char[] ShuffledChars = "FA7C2XNYP95EHL4G3KTDMZR".ToCharArray();
        private readonly int[] RandomIndexes = [3, 21, 7, 13];
        private readonly bool[] RandomBooleans = [true, true, false, false];

        public string[] GenerateCodes()
        {
            string[] codes = new string[1000];
            for (int j = 0; j < 1000; j++)
            {
                int nextCharIndex;
                char[] chars = new char[8];
                int randomIndexNumber = 0;

                for (int i = 0; i < 4; i++)
                {
                    randomIndexNumber = RandomNumberGenerator.GetInt32(0, ShuffledChars.Length);

                    nextCharIndex = (randomIndexNumber + RandomIndexes[i]) % ShuffledChars.Length;
                    if (RandomBooleans[i])
                    {
                        chars[i * 2] = ShuffledChars[randomIndexNumber];
                        chars[(i * 2) + 1] = ShuffledChars[nextCharIndex];
                    }
                    else
                    {
                        chars[i * 2] = ShuffledChars[nextCharIndex];
                        chars[(i * 2) + 1] = ShuffledChars[randomIndexNumber];
                    }
                }

                string codeString = string.Join("", chars);
                codes[j] = codeString;
                Console.WriteLine(codeString);
            }
            int duplicateCount = codes.GroupBy(x => x)
            .Where(g => g.Count() > 1)
            .Select(b => b.Key)
            .Count();
            Console.WriteLine($"Duplicate Count: {duplicateCount}");
            return codes;
        }

        public bool CheckCode(string code)
        {
            char randomChar;
            for (int i = 0; i < 4; i++)
            {
                if (RandomBooleans[i])
                {
                    randomChar = code[i * 2];
                    int index = Array.IndexOf(ShuffledChars, randomChar);
                    int modifier = RandomIndexes[i];
                    var nextIndex = (index + modifier) % ShuffledChars.Length;
                    if(code[(i * 2) + 1] != ShuffledChars[nextIndex])
                    {
                        return false;
                    }
                }
                else
                {
                    randomChar = code[(i * 2) + 1];
                    int index = Array.IndexOf(ShuffledChars, randomChar);
                    int modifier = RandomIndexes[i];
                    var nextIndex = (index + modifier) % ShuffledChars.Length;
                    if (code[(i * 2)] != ShuffledChars[nextIndex])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}