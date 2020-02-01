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
    public class OnlineTransactionRepository : IRepository<Transaction>
    {
        private readonly OnlineDbContext _context;

        public OnlineTransactionRepository(OnlineDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _context.GetTransactionsAsync();
        }

        public IEnumerable<Transaction> Where(Expression<Func<Transaction, bool>> exp)
        {
            return _context.GetTransactions().AsQueryable().Where(exp);
        }
    }
}
