using System.Collections.Generic;
using MMTShop.Data.Entities.Base;

namespace MMTShop.Data.Entities.Products
{
    public   class Category : NamedEntity
    {
        /* Navigation Properties */
        public virtual List<Product> Products { get; set; }
        public bool IsFeatured { get; set; }
    }
}
