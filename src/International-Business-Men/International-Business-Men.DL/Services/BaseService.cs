using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Services
{
    public class BaseService<T> : IService<T> where T : IEntity
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(object id)
        {
            return await _repository.GetById(id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _repository.Where(exp);
        }

        public void AddOrUpdate(T entry)
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
            var label = _repository.GetById(id).Result;
            _repository.Delete(label);
        }
    }
}
