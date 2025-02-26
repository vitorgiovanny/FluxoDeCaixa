using CashBalance.Domain;
using CashBalance.Domain.Entities;

namespace CashBalance.Interfaces
{
    public interface ICashierServices
    {
        Task<(Cashier, Guid)> CreateCashier(string name);
        Task<Cashier> GetCashier(Guid id);
        Task<IEnumerable<Cashier>> GetAllCashiers();
        Task<Cashier> UpdateCashier(Cashier cashier);
        Task DeleteCashier(Guid id);
    }
}