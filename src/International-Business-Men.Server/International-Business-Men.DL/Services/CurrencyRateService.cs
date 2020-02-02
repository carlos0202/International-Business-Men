using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{

    public class CurrencyRateService : IService<CurrencyRate>
    {
        private readonly IRepository<CurrencyRate> _onlineRepository;
        private readonly ILocalSourceRepository<CurrencyRate> _localCurrencyRateRepository;

        public CurrencyRateService(IRepository<CurrencyRate> repository, 
            ILocalSourceRepository<CurrencyRate> localCurrencyRateRepository)
        {
            _onlineRepository = repository;
            _localCurrencyRateRepository = localCurrencyRateRepository;
        }

        public async Task<IEnumerable<CurrencyRate>> GetAsync()
        {
            IEnumerable<CurrencyRate> rates;
            try
            {
                rates = await _onlineRepository.GetAll();
                if (!rates.Any()) {
                    rates = await _localCurrencyRateRepository.GetAll();
                }
                _localCurrencyRateRepository.Refresh(rates);
            } catch(Exception)
            {
                //TODO log that online repo is dead.
                rates = await _localCurrencyRateRepository.GetAll();
            }

            return rates;
        }

        public IEnumerable<CurrencyRate> Where(Expression<Func<CurrencyRate, bool>> exp)
        {
            IEnumerable<CurrencyRate> rates;

            try
            {
                rates = _onlineRepository.Where(exp);
                if (!rates.Any())
                {
                    rates = _localCurrencyRateRepository.Where(exp);
                }
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
