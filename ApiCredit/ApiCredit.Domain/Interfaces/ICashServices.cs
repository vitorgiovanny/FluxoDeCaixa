using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Domain.Interfaces
{
    public interface ICashServices
    {
        Task AddCash(double amount, Guid idCashed, Guid idcash);
    }
}
