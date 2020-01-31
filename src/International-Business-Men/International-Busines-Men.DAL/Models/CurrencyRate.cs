using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Models
{
    public class CurrencyRate: IEntity
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }

        public object GetId()
        {
            return new { From, To };
        }
    }
}
