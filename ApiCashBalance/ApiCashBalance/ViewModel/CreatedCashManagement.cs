using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.ViewModel
{
    public class CreatedCashManagement
    {
        /// <summary>
        /// Id do Comerciante
        /// </summary>
        public Guid Id { get; }
        /// <summary>
        /// Id do Caixa do Comerciante
        /// </summary>
        public Guid IdCash { get; }
        /// <summary>
        /// Nome do Comerciante
        /// </summary>
        public string Name { get; }

        public CreatedCashManagement(Guid id, Guid idCash, string name)
        {
            Id = id;
            IdCash = idCash;
            Name = name;
        }
    }
}
