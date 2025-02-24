using CashBalance.Domain.Domain;

namespace CashBalance.Interfaces
{
    public interface IExtractServices
    {
        Task CreateExtract(Extract extract);
        Task<Extract> GetExtract(Guid idCashier);
    }
}