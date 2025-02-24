using CashBalance.Application.Dto;
using CashBalance.Domain;
using CashBalance.Domain.Interfaces;
using CashBalance.Interfaces;

namespace CashBalance.Application.Services
{
    public class CashierServices : ICashierServices
    {
        private readonly IRepository<Cashier> _cashierRepository;

        public CashierServices(IRepository<Cashier> cashierRepository)
        {
            _cashierRepository = cashierRepository;
        }

        public async Task<Cashier> CreateCashier(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name is required");

            Cashier cash = new Cashier()
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            await _cashierRepository.Add(cash);
            await _cashierRepository.Save();

            return cash;
        }

        public async Task<Cashier> GetCashier(Guid id)
        {
            if(id == Guid.Empty) throw new ArgumentNullException("Id is required");

            return await _cashierRepository.GetById(id);
        }

        public async Task<IEnumerable<Cashier>> GetAllCashiers()
        {
            return await _cashierRepository.GetAllAsync();
        }

        public async Task<Cashier> UpdateCashier(Cashier cashier)
        {
            if(cashier == null ||  cashier?.Id == Guid.Empty) throw new ArgumentNullException("Cashier is required");
           
           try{ await _cashierRepository.Update(cashier); await _cashierRepository.Save(); }catch(Exception e){ throw new Exception(e.Message); }

            return cashier;
        }

        public async Task DeleteCashier(Guid id)
        {
            if(id == Guid.Empty) throw new ArgumentNullException("Id is required");

            try
            {
                var obj = new Cashier()
                {
                    Id = id
                };

                await _cashierRepository.Update(obj);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}