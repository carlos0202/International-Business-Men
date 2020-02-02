using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{

    public class TransactionService : IService<ProductTransaction>
    {
        private readonly IRepository<ProductTransaction> _onlineRepository;
        private readonly ILocalSourceRepository<ProductTransaction> _localRepository;

        public TransactionService(IRepository<ProductTransaction> onlineRepository, 
            ILocalSourceRepository<ProductTransaction> localRepository)
        {
            _onlineRepository = onlineRepository;
            _localRepository = localRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetAsync()
        {
            IEnumerable<ProductTransaction> transactions;
            try
            {
                transactions = await _onlineRepository.GetAll();
                if (!transactions.Any())
                {
                    transactions = await _localRepository.GetAll();
                }
                _localRepository.Refresh(transactions);
            } catch(Exception)
            {
                //TODO log that online repo is dead.
                transactions = await _localRepository.GetAll();
            }

            return transactions;
        }

        public IEnumerable<ProductTransaction> Where(Expression<Func<ProductTransaction, bool>> exp)
        {
            IEnumerable<ProductTransaction> transactions;

            try
            {
                transactions = _onlineRepository.Where(exp);
                if (!transactions.Any())
                {
                    transactions = _localRepository.Where(exp);
                }
            }
            catch (Exception)
            {
                //TODO log that online repo is dead.
                transactions = _localRepository.Where(exp);
            }

            return transactions;
        }
    }
}
