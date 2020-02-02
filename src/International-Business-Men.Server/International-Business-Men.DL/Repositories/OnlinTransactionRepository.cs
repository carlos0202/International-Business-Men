using International_Business_Men.DAL.Context;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Repositories
{
    public class OnlineTransactionRepository : IRepository<ProductTransaction>
    {
        private readonly IOnlineContext _context;

        public OnlineTransactionRepository(IOnlineContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductTransaction>> GetAll()
        {
            return await _context.GetTransactionsAsync();
        }

        public IEnumerable<ProductTransaction> Where(Expression<Func<ProductTransaction, bool>> exp)
        {
            return _context.GetTransactions().AsQueryable().Where(exp).ToList();
        }
    }
}
