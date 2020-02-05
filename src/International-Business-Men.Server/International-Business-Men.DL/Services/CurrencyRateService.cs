using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CurrencyRateService> _logger;

        public CurrencyRateService(IRepository<CurrencyRate> repository,
            ILocalSourceRepository<CurrencyRate> localCurrencyRateRepository,
            ILogger<CurrencyRateService> logger)
        {
            _onlineRepository = repository;
            _localCurrencyRateRepository = localCurrencyRateRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CurrencyRate>> GetAsync()
        {
            IEnumerable<CurrencyRate> rates;
            try
            {
                rates = await _onlineRepository.GetAll();
                if (!rates.Any())
                {
                    _logger.Log(
                   LogLevel.Information,
                   "No se obtuvo información de rates online. Usando servicio local.");
                    rates = await _localCurrencyRateRepository.GetAll();
                }
                _localCurrencyRateRepository.Refresh(rates);
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LogLevel.Warning, ex,
                    "Error obteniendo información de rates online. Usando servicio local.");
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
                    _logger.Log(
                   LogLevel.Information,
                   "No se obtuvo información de rates online. Usando servicio local.");
                    rates = _localCurrencyRateRepository.Where(exp);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(
                    LogLevel.Warning, ex,
                    "Error obteniendo información de rates online. Usando servicio local.");
                rates = _localCurrencyRateRepository.Where(exp);
            }

            return rates;
        }
    }
}
