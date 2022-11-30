using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceServer.Models
{
    public class PriceVolume
    {
        public double Price { get; set; }
        public double Volume { get; set; }
        public long  Timestamp { get; set; }
    }
}
