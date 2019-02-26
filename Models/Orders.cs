using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profitify
{
    public class Orders
    {
        public int timestamp { get; set; }
        public string status { get; set; }
        public decimal amountOriginal { get; set; }
        public string type { get; set; }
        public string coinFrom { get; set; }
        public string coinTo { get; set; }
        public List<object> negotiations { get; set; }
        public string orderId { get; set; }
        public string userId { get; set; }
        public string nickName { get; set; }
        public decimal price { get; set; }
        public decimal amount { get; set; }

    }
    public class OrdersParam
    {
        public string CoinFrom { get; set; }
        public string CoinTo { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}