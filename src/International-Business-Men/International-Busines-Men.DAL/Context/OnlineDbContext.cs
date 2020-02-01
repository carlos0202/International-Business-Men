using International_Business_Men.DAL.Extensions;
using International_Business_Men.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Context
{
    public class OnlineDbContext: IOnlineContext
    {
        private string RatesApiURL { get; set; }
        private string TransactionsApiURL { get; set; }
        private readonly IConfiguration _configuration;

        public OnlineDbContext(IConfiguration configuration)
        {
            _configuration = configuration;          
        }

        public async Task<IEnumerable<CurrencyRate>> GetRatesAsync()
        {
            RatesApiURL = _configuration.GetValue<string>("RatesEndPoint");

            using (var client = new WebClient())
            {
                return await client.GetAsync<CurrencyRate>(RatesApiURL);
            }
        }

        public IEnumerable<CurrencyRate> GetRates()
        {
            RatesApiURL = _configuration.GetValue<string>("RatesEndPoint");

            using (var client = new WebClient())
            {
                return client.GetSync<CurrencyRate>(RatesApiURL);
            }
        }

        public async Task<IEnumerable<ProductTransaction>> GetTransactionsAsync()
        {
            TransactionsApiURL = _configuration.GetValue<string>("TransactionsEndPoint");
            using (var client = new WebClient())
            {
                var transactions = await client.GetAsync<TransactionDTO>(TransactionsApiURL);
                return transactions.Select(o =>
                    new ProductTransaction
                    {
                        SKU = o.SKU,
                        Amount = o.Amount,
                        Currency = o.Currency
                    });
            }
        }

        public IEnumerable<ProductTransaction> GetTransactions()
        {
            TransactionsApiURL = _configuration.GetValue<string>("TransactionsEndPoint");
            using (var client = new WebClient())
            {
                return client.GetSync<ProductTransaction>(TransactionsApiURL);
            }
        }
    }
}
