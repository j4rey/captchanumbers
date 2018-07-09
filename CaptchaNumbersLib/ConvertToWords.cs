using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Captcha
{
    public class ConvertToWords
    {
        private static string[,] letters = new string[10, 3] {
                                                {"zero" , "ten"         , "" },
                                                {"one"  , "eleven"      , "" },
                                                {"two"  , "twelve"      , "twenty" },
                                                {"three", "thirteen"    , "thirty" },
                                                {"four" , "fourteen"    , "fourty" },
                                                {"five" , "fifteen"     , "fifty" },
                                                {"six"  , "sixteen"     , "sixty" },
                                                {"seven", "seventeen"   , "seventy" },
                                                {"eight", "eighteen"    , "eighty" },
                                                {"nine" , "nineteen"    , "ninty" }};
        private static string ToWords(int place, int val)
        {
            return letters[val, place];
        }

        public static string ToWords(int number)
        {
            string statement = "";
            List<int> stack = new List<int>();
            {
                int position = 0;

                while (number != 0 || position == 0)
                {
                    int mod = (number % 10);
                    stack.Add(mod);
                    number = number / 10;
                    position++;
                }
            }

            string tempstatement = "";
            for (int index = 0; index < stack.Count; index++)
            {
                int num = stack[index];
                if (index == 0 && num == 0 && stack.Count > 1) //skip if num is 0 and stack count is > 0
                {
                    continue;
                }
                if (index == 1 && num == 1)//check for teen
                {
                    tempstatement = ConvertToWords.ToWords(index, stack[index - 1]);
                    break;
                }
                if (index > 0)
                {
                    index++;
                }
                int power = Convert.ToInt32(Math.Pow(10, index));
                int multiples = (power != 0 ? power : 1);
                int t = num * multiples;
                tempstatement = ConvertToWords.ToWords(index, num) + " " + tempstatement;
            }
            statement += tempstatement;
            return statement.Trim();
        }
    }
}
