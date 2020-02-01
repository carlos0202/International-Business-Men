using International_Business_Men.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Context
{
    public interface IOnlineContext
    {
        Task<IEnumerable<CurrencyRate>> GetRatesAsync();
        IEnumerable<CurrencyRate> GetRates();
        Task<IEnumerable<ProductTransaction>> GetTransactionsAsync();
        IEnumerable<ProductTransaction> GetTransactions();
    }
}