using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Domain.RabbitMq
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = "localhost";
        public string QueueName { get; set; } = "Extracted";
    }
}
