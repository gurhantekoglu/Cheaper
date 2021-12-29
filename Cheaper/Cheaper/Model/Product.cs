using System;
using System.Collections.Generic;
using System.Text;

namespace Cheaper.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShopName { get; set; }
        public string ProductPhotoUrl { get; set; }
        public double Price { get; set; }
        public DateTime PriceDate { get; set; }
    }
}
