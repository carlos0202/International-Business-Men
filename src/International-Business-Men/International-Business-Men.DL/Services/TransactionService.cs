using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{

    public class TransactionService : IService<ProductTransaction>
    {
        private readonly IRepository<ProductTransaction> _onlineRepository;
        private readonly ILocalSourceRepository<ProductTransaction> _localCurrencyRateRepository;

        public TransactionService(IRepository<ProductTransaction> onlineRepository, 
            ILocalSourceRepository<ProductTransaction> localRepository)
        {
            _onlineRepository = onlineRepository;
            _localCurrencyRateRepository = localRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetAsync()
        {
            IEnumerable<ProductTransaction> rates;
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

        public IEnumerable<ProductTransaction> Where(Expression<Func<ProductTransaction, bool>> exp)
        {
            IEnumerable<ProductTransaction> rates;

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
