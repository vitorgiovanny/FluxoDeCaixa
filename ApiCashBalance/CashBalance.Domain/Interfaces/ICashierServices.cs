using CashBalance.Domain;

namespace CashBalance.Interfaces
{
    public interface ICashierServices
    {
        Task<Cashier> CreateCashier(string name);
        Task<Cashier> GetCashier(Guid id);
        Task<IEnumerable<Cashier>> GetAllCashiers();
        Task<Cashier> UpdateCashier(Cashier cashier);
        Task DeleteCashier(Guid id);
    }
}