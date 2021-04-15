using MMTShop.Data.Entities.Products;
using MMTShop.Data.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace MMTShop.Data.Repositories.Products
{
    public   class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Category>> GetAllSP(int page, int size)
        {
            IEnumerable<Category> list;
            string sql = "EXEC GetCategories";
            list = await context.Set<Category>().FromSqlRaw(sql).ToListAsync();
            return list;
        }
    }
}
