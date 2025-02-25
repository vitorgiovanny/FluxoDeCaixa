using ApiCredit.Domain.Entities;
using ApiCredit.Domain.Interfaces;
using ApiCredit.Domain.Model.Push;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiCredit.Application.Services
{
    public class CashServices : ICashServices
    {
        private readonly IRepository<Cash> _cashRepository;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private const string BALANCE_CREDIT = "balance_credit";
        public CashServices(IRepository<Cash> repository, IRabbitMqPublisher publish)
        {
            _cashRepository = repository;
            _rabbitMqPublisher = publish;
        }

        public async Task AddCash(double amount, Guid idCashed)
        {
            if (amount <= 0) throw new ArgumentException("Amount is zero, review.");

            var cash = await GetCash(idCashed);
            cash.Cashs += amount;
            
            if(cash.Id == Guid.Empty)
            {
                cash.Id = Guid.NewGuid();
                await _cashRepository.Add(cash);

            }else
            {
                await UpdateCash(cash);
            }

            await _cashRepository.Save();
            await PublishedMessage(cash);
        }

        private async Task UpdateCash(Cash cash)
            => await _cashRepository.Update(cash);

        private async Task PublishedMessage(Cash cash)
        {
            var message = new Extract()
            {
                IdCashier = cash.IdCashed,
                Cash = cash.Cashs,
                IdCash = cash.Id,
                Description = $"Credit {cash.Cashs} in cash"
            };

            string jsonString = JsonSerializer.Serialize(message);

            await _rabbitMqPublisher.PublicarMensagem(BALANCE_CREDIT, jsonString);
        }

        private async Task<Cash> GetCash(Guid idCashed)
        {
            var cash = await _cashRepository.GetByFilter(x => x.IdCashed == idCashed);

            if (cash.Count == 0) return new Cash()
            {
                Id = Guid.Empty,
                IdCashed = idCashed,
                AtRegister = DateTime.UtcNow
            };

            return await Task.FromResult(cash.FirstOrDefault());
        }
    }
}
