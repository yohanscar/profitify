using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profitify
{
    public class Ticker
    {
        public decimal buy { get; set; }
        public decimal sell { get; set; }
        public int timestamp { get; set; }
        public string coinFrom { get; set; }
        public string coinTo { get; set; }
        public decimal min { get; set; }
        public decimal max { get; set; }
        public decimal last { get; set; }
        public decimal volume { get; set; }
    }
}