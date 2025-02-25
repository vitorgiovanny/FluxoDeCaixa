using CashBalance.Domain.Domain;
using CashBalance.Domain.Interfaces;
using CashBalance.Interfaces;

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

        public Task<Extract> GetExtract(Guid idCashier)
        {
            throw new NotImplementedException();
        }
    }
}