using International_Business_Men.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DL.Contracts
{
    public interface ILocalSourceRepository<T> : IRepository<T>
        where T: IEntity
    {
        IEnumerable<T> Refresh(IEnumerable<T> newSource);
    }
}
