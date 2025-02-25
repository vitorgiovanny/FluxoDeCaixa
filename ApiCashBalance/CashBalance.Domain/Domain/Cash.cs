using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Domain.Domain
{
    public class Cash : Entity
    {
        public double Cashs { get; set; }
        public Guid IdCashed { get; set; }
        public DateTime AtRegister { get; set; }
        public DateTime? AtLastUpdate { get; set; }
    }
}