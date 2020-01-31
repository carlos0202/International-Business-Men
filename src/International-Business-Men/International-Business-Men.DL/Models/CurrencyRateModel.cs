using System;
using System.Collections.Generic;
using System.Text;

namespace International_Business_Men.DL.Models
{
    public class CurrencyRateModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}
