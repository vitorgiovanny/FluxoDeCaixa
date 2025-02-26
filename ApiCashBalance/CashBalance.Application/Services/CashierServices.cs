using CashBalance.Application.Dto;
using CashBalance.Domain;
using CashBalance.Domain.Entities;
using CashBalance.Domain.Interfaces;
using CashBalance.Domain.Interfaces.Repository;
using CashBalance.Interfaces;

namespace CashBalance.Application.Services
{
    public class CashierServices : ICashierServices
    {
        private readonly IRepository<Cashier> _cashierRepository;
        private readonly IRepository<Cash> _repositoryCash;
        private readonly int INITIAL_AMOUNT = 0;

        public CashierServices(IRepository<Cashier> cashierRepository, IRepository<Cash> repositoryCash)
        {
            _cashierRepository = cashierRepository;
            _repositoryCash = repositoryCash;
        }

        public async Task<(Cashier, Guid)> CreateCashier(string name)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name is required");

            Cashier cashManagement = new Cashier()
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow
            };

            var cash = new Cash(amount: INITIAL_AMOUNT ,cashierId: cashManagement.Id) {  };

            await _cashierRepository.Add(cashManagement);
            await _cashierRepository.Save();
            await CreaterCash(cash);
            
            return (cashManagement, cash.Id);
        }

        public async Task<Cashier> GetCashier(Guid id)
        {
            if(id == Guid.Empty) throw new ArgumentNullException("Id is required");

            return await _cashierRepository.GetById(id);
        }

        public async Task<IEnumerable<Cashier>> GetAllCashiers()
            => await _cashierRepository.GetAllAsync();

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

        private async Task CreaterCash(Cash cash)
        {
            await _repositoryCash.Add(cash);
            await _cashierRepository.Save();
        }
    }
}