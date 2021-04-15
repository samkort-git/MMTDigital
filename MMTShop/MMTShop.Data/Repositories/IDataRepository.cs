using System;
using System.Threading.Tasks;
using MMTShop.Data.Repositories.Products;


namespace MMTShop.Data.Repositories
{
    /// <summary>
    /// Data Repository (Unit Of Work)
    /// </summary>
    public interface IDataRepository : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }

        int Save();
        Task<int> SaveAsync();
    }
}
