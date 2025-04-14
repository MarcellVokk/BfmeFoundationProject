using System;

namespace BfmeFoundationProject.AllInOneLauncher.Core.Utils
{
    public static class NumberUtils
    {
        public static string IntAsPrettyNumber(int num)
        {
            int i = (int)Math.Pow(10, (int)Math.Max(0, Math.Log10(num) - 2));
            num = num / i * i;

            if (num >= 1000000000)
                return (num / 1000000000D).ToString("0.#") + "B";
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.#") + "M";
            if (num >= 1000)
                return (num / 1000D).ToString("0.#") + "K";

            return num.ToString("#,0");
        }
    }
}
