using CashBalance.Domain.Entities;

namespace CashBalance.Interfaces
{
    public interface IExtractServices
    {
        Task CreateExtract(Extract extract);
        Task<List<Extract>> GetExtract(Guid idCashier);
        Task<double> GetReportPerDay(Guid idCash);
    }
}