using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Models
{
    public class ProductTransaction: IEntity
    {
        public long Id { get; set; }
        public string SKU { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
