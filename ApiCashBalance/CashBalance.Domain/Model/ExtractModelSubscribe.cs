using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Domain.Model
{
    public class ExtractModelSubscribe
    {
        public double Cash { get; set; }
        public string Description { get; set; }
        public Guid IdCashier { get; set; }
        public Guid IdCash { get; set;}
    }
}
