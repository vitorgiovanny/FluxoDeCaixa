using CashBalance.Domain.Domain;

namespace CashBalance.Interfaces
{
    public interface IExtractServices
    {
        Task CreateExtract(Extract extract);
        List<Extract> GetExtract(Guid idCashier);
    }
}