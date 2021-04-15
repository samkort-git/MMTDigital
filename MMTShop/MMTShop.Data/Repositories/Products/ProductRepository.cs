using MMTShop.Data.Entities.Products;
using MMTShop.Data.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace MMTShop.Data.Repositories.Products
{
    public   class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Product>> GetFeaturedAsy()
        {
            IEnumerable<Product> list;
            string sql = "EXEC GetFeaturedProducts";
            list = await context.Set<Product>().FromSqlRaw(sql).ToListAsync();
            return list;
        }

        public async Task<IEnumerable<Product>> GetProductsAsy(int categoryId)
        {
            return await Entities
                            .Where(a => a.CategoryId== categoryId)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllSP(int categoryId,  int page, int size)
        {
            IEnumerable<Product> list;
            string sql = $"EXEC GetAllProduct {categoryId}";
            list = await context.Set<Product>().FromSqlRaw(sql).ToListAsync();
            return list;
        }
    }
}
