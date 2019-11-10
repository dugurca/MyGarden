using System;

namespace _Game.Source.CS
{
    public static class S
    {
        //For representation purposes
        //Incremental games often use this to large numbers 
        public static string CoinStr(double num)
        {
            if (num <= 0) return "0";
            if (num < 1000) return num.ToString("#.##");
            int numZeros = (int)Math.Log10(num);
            
            for (int i = 2; i <= 21; i++)
            {
                if (numZeros < i * 3)
                {
                    double res = num / Math.Pow(10, 3 * (i - 1));
                    return res.ToString("#.##" + SuffixStr[i]);
                }
            }

            return "!!!";
        }

        private const string SuffixStr = "..KMBTqQsSONdUD!@#$%^&";
    }
}
