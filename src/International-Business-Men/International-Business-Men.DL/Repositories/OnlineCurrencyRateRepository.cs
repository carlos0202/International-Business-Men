using International_Business_Men.DAL.Context;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Repositories
{
    public class OnlineCurrencyRateRepository : IRepository<CurrencyRate>
    {
        private readonly OnlineDbContext _context;

        public OnlineCurrencyRateRepository(OnlineDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CurrencyRate>> GetAll()
        {
            return await _context.GetRatesAsync();
        }

        public IEnumerable<CurrencyRate> Where(Expression<Func<CurrencyRate, bool>> exp)
        {
            return _context.GetRates().AsQueryable().Where(exp);
        }
    }
}
