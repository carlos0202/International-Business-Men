using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{

    public class TransactionService : IService<Transaction>
    {
        private readonly IRepository<Transaction> _onlineRepository;
        private readonly ILocalSourceRepository<Transaction> _localCurrencyRateRepository;

        public TransactionService(IRepository<Transaction> onlineRepository, 
            ILocalSourceRepository<Transaction> localRepository)
        {
            _onlineRepository = onlineRepository;
            _localCurrencyRateRepository = localRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAsync()
        {
            IEnumerable<Transaction> rates;
            try
            {
                rates = await _onlineRepository.GetAll();
                _localCurrencyRateRepository.Refresh(rates);
            } catch(Exception)
            {
                //TODO log that online repo is dead.
                rates = await _localCurrencyRateRepository.GetAll();
            }

            return rates;
        }

        public IEnumerable<Transaction> Where(Expression<Func<Transaction, bool>> exp)
        {
            IEnumerable<Transaction> rates;

            try
            {
                rates = _onlineRepository.Where(exp);
            }
            catch (Exception)
            {
                //TODO log that online repo is dead.
                rates = _localCurrencyRateRepository.Where(exp);
            }

            return rates;
        }
    }
}
