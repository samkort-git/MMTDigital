using MMTShop.Data.Models.Base;
using System.Collections.Generic;

namespace MMTShop.Data.Models
{
    public   class CategoryModel : NamedModel
    {
        /* Navigation Properties */
        public virtual List<ProductModel> Products { get; set; }
        public bool IsFeatured { get; set; }
    }
}
