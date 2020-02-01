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

    public class LocalTransactionRepository : ILocalSourceRepository<Transaction>
    {
        private readonly LocalDbContext _context;

        private readonly DbSet<Transaction> _entities;

        public LocalTransactionRepository(LocalDbContext context)
        {
            _context = context;
            _entities = context.Transactions;
        }
        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public IEnumerable<Transaction> Refresh(IEnumerable<Transaction> newSource)
        {
            _entities.RemoveRange(_entities.ToList());
            _entities.AddRange(newSource);

            return _entities.ToList();
        }

        public IEnumerable<Transaction> Where(Expression<Func<Transaction, bool>> exp)
        {
            return _entities.Where(exp);
        }
    }
}
