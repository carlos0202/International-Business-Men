using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{

    public class TransactionService : IService<ProductTransaction>
    {
        private readonly IRepository<ProductTransaction> _onlineRepository;
        private readonly ILocalSourceRepository<ProductTransaction> _localRepository;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(IRepository<ProductTransaction> onlineRepository,
            ILocalSourceRepository<ProductTransaction> localRepository,
            ILogger<TransactionService> logger)
        {
            _onlineRepository = onlineRepository;
            _localRepository = localRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductTransaction>> GetAsync()
        {
            IEnumerable<ProductTransaction> transactions;
            try
            {
                transactions = await _onlineRepository.GetAll();
                if (!transactions.Any())
                {
                    _logger.Log(
                   LogLevel.Information,
                   "No se obtuvo información de transacciones online. Usando servicio local.");
                    transactions = await _localRepository.GetAll();
                }
                _localRepository.Refresh(transactions);
            }
            catch (Exception ex)
            {
                _logger.Log(
                   LogLevel.Warning, ex,
                   "Error obteniendo información de transacciones local.");
                Debug.WriteLine($"Ha ocurrido un error: {ex.Message}");
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
                    _logger.Log(
                   LogLevel.Information,
                   "No se obtuvo información de transacciones online. Usando servicio local.");
                    transactions = _localRepository.Where(exp);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(
                   LogLevel.Warning, ex,
                   "Error obteniendo información de transacciones local.");
                transactions = _localRepository.Where(exp);
            }

            return transactions;
        }
    }
}
