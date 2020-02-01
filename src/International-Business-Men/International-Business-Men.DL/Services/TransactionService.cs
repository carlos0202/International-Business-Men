﻿using International_Business_Men.DAL.Models;
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
        private readonly ILocalSourceRepository<ProductTransaction> _localCurrencyRateRepository;

        public TransactionService(IRepository<ProductTransaction> onlineRepository, 
            ILocalSourceRepository<ProductTransaction> localRepository)
        {
            _onlineRepository = onlineRepository;
            _localCurrencyRateRepository = localRepository;
        }

        public async Task<IEnumerable<ProductTransaction>> GetAsync()
        {
            IEnumerable<ProductTransaction> transactions;
            try
            {
                transactions = await _onlineRepository.GetAll();
                if (!transactions.Any())
                {
                    transactions = await _localCurrencyRateRepository.GetAll();
                }
                _localCurrencyRateRepository.Refresh(transactions);
            } catch(Exception)
            {
                //TODO log that online repo is dead.
                transactions = await _localCurrencyRateRepository.GetAll();
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
                    transactions = _localCurrencyRateRepository.Where(exp);
                }
            }
            catch (Exception)
            {
                //TODO log that online repo is dead.
                transactions = _localCurrencyRateRepository.Where(exp);
            }

            return transactions;
        }
    }
}
