﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace International_Business_Men.DAL.Models
{
    public class Transaction: IEntity
    {
        public string SKU { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
