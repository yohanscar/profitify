using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profitify
{
    public class Buy
    {
        public string orderId { get; set; }
        public string userId { get; set; }
        public string nickName { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
    }

    public class Sell
    {
        public string orderId { get; set; }
        public string userId { get; set; }
        public string nickName { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
    }

    public class OrderBook
    {
        public List<Buy> buy { get; set; }
        public List<Sell> sell { get; set; }
    }
}