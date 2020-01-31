using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{

    public class CurrencyRateService : IService<CurrencyRate>
    {
        private readonly IRepository<CurrencyRate> _repository;

        public CurrencyRateService(IRepository<CurrencyRate> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CurrencyRate>> GetAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<CurrencyRate> GetById(object id)
        {
            return await _repository.GetById(id);
        }

        public IEnumerable<CurrencyRate> Where(Expression<Func<CurrencyRate, bool>> exp)
        {
            return _repository.Where(exp);
        }

        public void AddOrUpdate(CurrencyRate entry)
        {
            var targetRecord = _repository.GetById(entry.GetId()).Result;
            var exists = targetRecord != null;

            if (exists)
            {
                _repository.Update(entry);
                return;
            }
            _repository.Insert(entry);
        }

        public void Remove(object id)
        {
            var targetRecord = _repository.GetById(id).Result;
            var exists = targetRecord != null;
            if (!exists) throw new Exception("Record not found for deletion.");

            _repository.Delete(targetRecord);
        }
    }
}
