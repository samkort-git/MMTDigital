using MMTShop.Data.Models.Base;

namespace MMTShop.Data.Models
{
    public class ProductModel : NamedModel
    {
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        /* Navigation Properties */
        public virtual CategoryModel Category { get; set; }
    }
}