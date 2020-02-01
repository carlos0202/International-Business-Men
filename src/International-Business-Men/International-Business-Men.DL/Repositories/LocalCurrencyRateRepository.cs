using International_Business_Men.DAL.Context;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Repositories
{

    public class LocalCurrencyRateRepository : ILocalSourceRepository<CurrencyRate>
    {
        private readonly LocalDbContext _context;

        private readonly DbSet<CurrencyRate> _entities;

        public LocalCurrencyRateRepository(LocalDbContext context)
        {
            _context = context;
            _entities = context.CurrencyRates;
        }
        public async Task<IEnumerable<CurrencyRate>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public IEnumerable<CurrencyRate> Refresh(IEnumerable<CurrencyRate> newSource)
        {
            _entities.RemoveRange(_entities.ToList());
            _entities.AddRange(newSource);

            return _entities.ToList();
        }

        public IEnumerable<CurrencyRate> Where(Expression<Func<CurrencyRate, bool>> exp)
        {
            return _entities.Where(exp);
        }
    }
}
