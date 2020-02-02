using International_Business_Men.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Contracts
{
    public interface IService<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAsync();

        IEnumerable<T> Where(Expression<Func<T, bool>> exp);
    }
}
