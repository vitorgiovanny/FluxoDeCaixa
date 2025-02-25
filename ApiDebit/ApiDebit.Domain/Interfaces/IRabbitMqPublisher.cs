using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDebit.Domain.Interfaces
{
    public interface IRabbitMqPublisher
    {
        Task PublicarMensagem(string fila, string mensagem);
    }
}
