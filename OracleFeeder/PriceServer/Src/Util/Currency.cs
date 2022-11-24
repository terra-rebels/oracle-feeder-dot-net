namespace PriceServer.Util
{
    internal class Currency
    {
        public string GetBaseCurrency(string symbol)
        {
            return symbol.Split('/')[0];
        }

        public string GetQuoteCurrency(string symbol)
        {
            return symbol.Split('/')[1];
        }
    }
}
