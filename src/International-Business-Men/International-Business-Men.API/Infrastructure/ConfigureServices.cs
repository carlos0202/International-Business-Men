using International_Business_Men.DAL.Context;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using International_Business_Men.DL.Repositories;
using International_Business_Men.DL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace International_Business_Men.API.Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IOnlineContext, OnlineDbContext>();
            services.AddTransient<ILocalSourceRepository<CurrencyRate>, LocalCurrencyRateRepository>();
            services.AddTransient<IRepository<CurrencyRate>, OnlineCurrencyRateRepository>();
            services.AddTransient<ILocalSourceRepository<ProductTransaction>, LocalTransactionRepository>();
            services.AddTransient<IRepository<ProductTransaction>, OnlineTransactionRepository>();
            services.AddTransient<IService<ProductTransaction>, TransactionService>();
            services.AddTransient<IService<CurrencyRate>, CurrencyRateService>();
        }
    }
}
