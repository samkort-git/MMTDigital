using System;
using System.Threading.Tasks;

using MMTShop.Data.Repositories.Products;


namespace MMTShop.Data.Repositories
{
    /// <summary>
    /// Data Repository (Unit Of Work)
    /// </summary>
    public class DataRepository : IDataRepository
    {
        private readonly DataContext context;
       
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }

       
        public DataRepository(DataContext context,      IProductRepository productRepository, ICategoryRepository categoryRepository   )
        {
            this.context = context;

            Products = productRepository;
            Categories = categoryRepository;

        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
        }
    }
}
