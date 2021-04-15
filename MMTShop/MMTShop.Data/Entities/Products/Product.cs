using System;
using MMTShop.Data.Entities.Base;

namespace MMTShop.Data.Entities.Products
{
  public   class Product: NamedEntity
    {
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        /* Navigation Properties */
        public virtual Category Category { get; set; }
    }
}
