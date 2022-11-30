using Microsoft.Win32.SafeHandles;
using PriceServer.Models;
using PriceServer.Src.Util;

namespace PriceServer.Util
{
    internal class Statistics
    {
        public double average(double[] array)
        {
            if (array is null || array.Length < 1)
            {
                throw new ArgumentNullException("empty array");
            }

            if (array.Length == 1)
            {
                return array[0];
            }

            return array.Aggregate((a, b) => (a + b) / Num.num(array.Length));
        }

        public double vwap(PriceVolume[] array)
        {
            if (array is null || array.Length < 1)
            {
                throw new ArgumentNullException("empty array");
            }

            if (array.Length == 1)
            {
                return array[0].Price;
            }

            var sum = array.Aggregate(Num.num(0.0), (s, x) => s + (x.Volume * x.Price));
            var totalVolume = array.Aggregate(Num.num(0.0), (s, x) => s + x.Volume);
            return sum / totalVolume == 0 ? Num.num(0) : sum / totalVolume;
        }
        public double tvwap(PriceVolume[] array, double minimumTimeWeight = 0.2)
        {
            if (array is null || array.Length < 1)
            {
                throw new ArgumentNullException("empty array");
            }

            if (array.Length == 1)
            {
                return array[0].Price;
            }
            var sortedArray = array.OrderByDescending(a => a.Timestamp);
            var now = Num.num(DateTime.Now.Ticks);
            var period = now - (Num.num(array[0].Timestamp));
            var weightUnit = (Num.num(1) - minimumTimeWeight) / period;

            var tvwapTrades = sortedArray.Select(trade => {
                return new PriceVolume { Price = trade.Price, Volume = trade.Volume * weightUnit * (period - (now - Num.num(trade.Timestamp)) + minimumTimeWeight) };
            }).ToArray();
            
            
            return vwap(tvwapTrades);
        }
    }
}
