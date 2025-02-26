using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Domain.Entities
{
    public class Extract : Entity
    {
        public double Amount { get; set; } = 0;
        public string Description { get; set; }
        public DateTime Register { get; set; }
        public DateTime? AtDelete { get; set; }
        public Guid IdCashier { get; set; }
        public Guid IdCash { get; set; }

        public Cash Cash { get; set; }
        public Cashier Cashier { get; set; }
        
    }
}
