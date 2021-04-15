using Microsoft.Extensions.DependencyInjection;
using MMTShop.Data.Repositories;
using MMTShop.Data.Repositories.Base;
using MMTShop.Data.Repositories.Products;


namespace MMTShop.Data
{
    public static class ServiceExtensions
    {
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        }
    }
}
