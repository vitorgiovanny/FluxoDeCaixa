using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Domain.Model.Push
{
    public class Extract
    {    
        public double Cash { get; set; }
        public string Description { get; set; }
        public Guid IdCashier { get; set; }
        public Guid IdCash { get; set; }
    }
}
