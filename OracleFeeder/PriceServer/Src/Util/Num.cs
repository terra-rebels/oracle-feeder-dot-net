namespace PriceServer.Util
{
    public static class Num
    {
        public static double num(double number)
        {
            return number;
        }

        public static long num(long number)
        {
            return number;
        }

        public static int num(int number)
        {
            return number;
        }

        public static float num(float number)
        {
            return number;
        }

        public static long num(string number)
        {
            var success = long.TryParse(number, out long result);
            if (success) { return result; }

            throw new InvalidOperationException("not a number");
        }
    }
}
