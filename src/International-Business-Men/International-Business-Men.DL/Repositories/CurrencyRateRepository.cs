using International_Business_Men.DAL.Context;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Repositories
{

    public class CurrencyRateRepository : IRepository<CurrencyRate>
    {
        private readonly RatesContext _context;

        private readonly DbSet<CurrencyRate> _entities;

        public CurrencyRateRepository(RatesContext context)
        {
            _context = context;
            _entities = context.CurrencyRates;
        }
        public async Task<IEnumerable<CurrencyRate>> GetAll()
        {
            return await _entities.ToListAsync();
        }
        public async Task<CurrencyRate> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }

        public IEnumerable<CurrencyRate> Where(Expression<Func<CurrencyRate, bool>> exp)
        {
            return _entities.Where(exp);
        }
        public async void Insert(CurrencyRate entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null");
            await _entities.AddAsync(entity);
            _context.SaveChanges();
        }
        public async void Update(CurrencyRate entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null");

            var oldEntity = await _context.FindAsync<CurrencyRate>(entity.GetId());
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
        public void Delete(CurrencyRate entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null");

            _entities.Remove(entity);
            _context.SaveChanges();
        }

    }
}
