using International_Business_Men.DAL.Context;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly RatesContext _context;

        private readonly DbSet<T> _entities;

        public BaseRepository(RatesContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }
        public async Task<T> GetById(object id)
        {
            return await _entities.FindAsync(id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _entities.Where(exp);
        }
        public async void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null");
            await _entities.AddAsync(entity);
            _context.SaveChanges();
        }
        public async void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null");

            var oldEntity = await _context.FindAsync<T>(entity.GetId());
            _context.Entry(oldEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("Input data is null");

            _entities.Remove(entity);
            _context.SaveChanges();
        }

    }
}
