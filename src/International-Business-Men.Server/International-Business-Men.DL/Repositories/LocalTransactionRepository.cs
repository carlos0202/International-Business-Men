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

    public class LocalTransactionRepository : ILocalSourceRepository<ProductTransaction>
    {
        private readonly LocalDbContext _context;

        private readonly DbSet<ProductTransaction> _entities;

        public LocalTransactionRepository(LocalDbContext context)
        {
            _context = context;
            _entities = context.Transactions;
        }
        public async Task<IEnumerable<ProductTransaction>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public IEnumerable<ProductTransaction> Refresh(IEnumerable<ProductTransaction> newSource)
        {
            _entities.RemoveRange(_entities.ToList());
            _entities.AddRange(newSource);
            _context.SaveChanges();
            return _entities.ToList();
        }

        public IEnumerable<ProductTransaction> Where(Expression<Func<ProductTransaction, bool>> exp)
        {
            return _entities.Where(exp);
        }
    }
}
