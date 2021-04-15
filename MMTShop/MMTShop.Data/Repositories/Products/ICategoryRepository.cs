using MMTShop.Data.Entities.Products;
using MMTShop.Data.Entities.Products;
using MMTShop.Data.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMTShop.Data.Repositories.Products
{
    public   interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllSP(int page, int size);

    }
}
