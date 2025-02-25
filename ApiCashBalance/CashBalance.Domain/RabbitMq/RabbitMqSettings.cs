using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Domain.RabbitMq
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = "localhost";
        public List<string> QueueName { get; set; }
    }
}
