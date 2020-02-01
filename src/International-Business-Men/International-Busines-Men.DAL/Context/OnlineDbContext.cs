using International_Business_Men.DAL.Extensions;
using International_Business_Men.DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Context
{
    public class OnlineDbContext
    {
        private string RatesApiURL { get; set; }
        private string TransactionsApiURL { get; set; }
        public OnlineDbContext(IConfiguration configuration)
        {
            RatesApiURL = configuration.GetValue<string>("RatesEndPoint");
            TransactionsApiURL = configuration.GetValue<string>("TransactionsEndPoint");
        }

        public async Task<IEnumerable<CurrencyRate>> GetRatesAsync()
        {
            using (var client = new WebClient())
            {
                return await client.GetAsync<CurrencyRate>(RatesApiURL);
            }
        }

        public IEnumerable<CurrencyRate> GetRates()
        {
            using (var client = new WebClient())
            {
                return client.GetSync<CurrencyRate>(RatesApiURL);
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            using (var client = new WebClient())
            {
                return await client.GetAsync<Transaction>(TransactionsApiURL);
            }
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            using (var client = new WebClient())
            {
                return client.GetSync<Transaction>(TransactionsApiURL);
            }
        }
    }
}
