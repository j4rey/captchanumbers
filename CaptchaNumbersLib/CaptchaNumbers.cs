using System;
using System.Collections.Generic;

namespace Captcha
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class CaptchaNumbers
    {
        private static string[] strOp = new string[] { "+", "-", "/", "x" };
        private enum enum_operators { ADD, SUBTRACT, DIVIDE, MULTIPLY };
        public string num1 { get; set; }
        public string opr { get; set; }
        public string num2 { get; set; }
        public string sum { get; set; }
        public string val { get; set; }

        public static CaptchaNumbers GetNewCaptcha()
        {
            CaptchaNumbers res = null;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            //for (int i = 0; i < 10000; i++)
            {
                int random_op = random.Next(0, 3);
                enum_operators op = (enum_operators)random_op;
                //MathOp.operators op = (MathOp.operators.DIVIDE);
                int Num1 = 0;
                int Num2 = 0;
                int SUM = 0;
                switch (op)
                {
                    case enum_operators.ADD:
                        {
                            Num1 = random.Next(1, 10);
                            Num2 = random.Next(1, 10);
                            SUM = Num1 + Num2;
                        }
                        break;
                    case enum_operators.SUBTRACT:
                        {
                            Num1 = random.Next(1, 10);
                            Num2 = random.Next(1, 10);
                            if (Num1 < Num2) //swap numbers if num1 < num2
                            {
                                Num2 = Num2 + Num1;
                                Num1 = Num2 - Num1;
                                Num2 = Num2 - Num1;
                            }
                            SUM = Num1 - Num2;
                        }
                        break;
                    case enum_operators.DIVIDE:
                        {
                            //get 2 random numbers
                            Num2 = random.Next(10, 99);
                            Num1 = random.Next(2, 10);

                            Num2 = Num2 / Num1;
                            Num1 = Num2 * Num1;
                            SUM = Num1 / Num2;

                            //swap SUM with Num2
                            SUM = SUM * Num2;
                            Num2 = SUM / Num2;
                            SUM = SUM / Num2;
                        }
                        break;
                    case enum_operators.MULTIPLY:
                        {
                            Num1 = random.Next(1, 10);
                            Num2 = random.Next(1, 10);
                            SUM = Num1 * Num2;
                        }
                        break;
                }

                res = new CaptchaNumbers()
                {
                    num1 = Convert.ToString(Num1),
                    opr = strOp[(int)op],
                    num2 = Convert.ToString(Num2),
                    sum = Convert.ToString(SUM)
                };
            }
            return HideData(res);
        }

        public static CaptchaNumbers HideData(CaptchaNumbers res)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            List<int> temp = new List<int> { 0, 1, 2 };
            int r_convert = temp[random.Next(0, temp.Count)];

            switch (r_convert)
            {
                case 0:
                    res.num1 = "(" + ConvertToWords.ToWords(Convert.ToInt32(res.num1)) + ")";
                    break;
                case 1:
                    res.num2 = "(" + ConvertToWords.ToWords(Convert.ToInt32(res.num2)) + ")";
                    break;
                case 2:
                    res.sum = "(" + ConvertToWords.ToWords(Convert.ToInt32(res.sum)) + ")";
                    break;
            }

            temp.Remove(r_convert);
            int r_encode = temp[random.Next(temp.Count)];

            switch (r_encode)
            {
                case 0:
                    res.val = res.num1;
                    res.num1 = "blank";
                    break;
                case 1:
                    res.val = res.num2;
                    res.num2 = "blank";
                    break;
                case 2:
                    res.val = res.sum;
                    res.sum = "blank";
                    break;
            }
            return res;
        }

        public static bool ValidateCaptch(string txt, string encodedval)
        {
            return true;
        }
    }
}
