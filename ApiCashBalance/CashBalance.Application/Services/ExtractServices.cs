using CashBalance.Domain.Entities;
using CashBalance.Domain.Interfaces;
using CashBalance.Domain.Interfaces.Repository;
using CashBalance.Interfaces;
using System.Diagnostics.Metrics;

namespace CashBalance.Application.Services
{
    public class ExtractServices : IExtractServices
    {
        private readonly IRepository<Extract> _extractRepository;

        public ExtractServices(IRepository<Extract> extractRepository)
        {
            _extractRepository = extractRepository;
        }

        public async Task CreateExtract(Extract extract)
        {
            extract.Id = Guid.NewGuid();
            await _extractRepository.Add(extract);
            await _extractRepository.Save();
        }

        public async Task<List<Extract>> GetExtract(Guid idCashier)
        {
            var response = await _extractRepository.GetByFilter(x => x.IdCash == idCashier);
            return response.ToList();
        }

        public async Task<double> GetReportPerDay(Guid idCash)
        {
            try
            {
                var filterTeste = await _extractRepository.GetAllAsync(); 
                var filter = await _extractRepository.GetByFilter(x => x.Register.Date == DateTime.UtcNow.Date);
                return filter.Sum(x => x.Amount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}