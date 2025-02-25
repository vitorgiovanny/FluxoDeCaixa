using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDebit.Domain.Entities
{
    public class Cash : _Entity
    {
        public double Cashs { get; set; }
        public Guid IdCashed { get; set; }
        public DateTime AtRegister { get; set; }
        public DateTime? AtLastUpdate { get; set; }
    }
}
