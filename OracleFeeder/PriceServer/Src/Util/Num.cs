namespace PriceServer.Util
{
    public static class Num
    {
        public static double num(double number)
        {
            return number;
        }

        public static double num(string number)
        {
            var success = double.TryParse(number, out double result);
            if (success) { return result; }

            throw new InvalidOperationException("not a number");
        }
    }
}
