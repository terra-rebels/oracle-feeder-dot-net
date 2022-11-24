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

            return array.Aggregate(0.0, (a, b) => (a + b) / Num.num(array.Length));
        }

        public double vwap(Dictionary<double, double> PriceVolumeArray) 
        {
           
                if(PriceVolumeArray is null || PriceVolumeArray.Values is null || PriceVolumeArray.Values.ToArray().Length == 0)
                {
                    throw new ArgumentNullException("empty array");
                }
                    
                if (PriceVolumeArray.Values.ToArray().Length == 1)
                {
                    return PriceVolumeArray.Keys.ElementAt(0);
                }

                // rewrite
                // const sum = array.reduce((s, x) => s.plus(x.volume.multipliedBy(x.price)), num(0))
                // const totalVolume = array.reduce((s, x) => s.plus(x.volume), num(0))
                // return sum.dividedBy(totalVolume) || num(0)
             return 0;
            }
        }
    }
