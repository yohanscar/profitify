using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profitify
{
    public class Markets
    {
        public int idMarket { get; set; }
        public string coinCodeFrom { get; set; }
        public string coinCodeto { get; set; }
        public string coinNameFrom { get; set; }
        public string coinNameTo { get; set; }
        public int decimalPlacesCoinFrom { get; set; }
        public int decimalPlacesCoinTo { get; set; }
        public string descriptionCoinRelation { get; set; }
    }
}