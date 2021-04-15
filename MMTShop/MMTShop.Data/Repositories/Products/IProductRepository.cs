using MMTShop.Data.Entities.Products;
using MMTShop.Data.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MMTShop.Data.Repositories.Products
{
    public   interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetFeaturedAsy();
        Task<IEnumerable<Product>> GetAllSP(int categoryId,int page, int size);

    }
}
