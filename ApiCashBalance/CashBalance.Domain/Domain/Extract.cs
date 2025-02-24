using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Domain.Domain
{
    public class Extract : Entity
    {
        public double Balance { get; set; }
        public DateTime Register { get; set; }
        public Guid IdCashier { get; set; }
        
    }
}
