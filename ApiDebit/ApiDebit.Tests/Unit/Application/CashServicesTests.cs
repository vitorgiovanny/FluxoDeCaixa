using ApiDebit.Application.Services;
using ApiDebit.Domain.Entities;
using ApiDebit.Domain.Interfaces;
using ApiDebit.Domain.Model.Push;
using ApiDebit.Domain.Value_Object;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiDebit.Tests.Unit.Application
{
    public class CashServicesTests
    {
        private readonly Mock<IRepository<Cash>> _mockRepository;
        private readonly Mock<IRabbitMqPublisher> _mockPublisher;
        private readonly CashServices _cashServices;

        public CashServicesTests()
        {
            _mockRepository = new Mock<IRepository<Cash>>();
            _mockPublisher = new Mock<IRabbitMqPublisher>();
            _cashServices = new CashServices(_mockRepository.Object, _mockPublisher.Object);
        }

        [Fact]
        public async Task AddCash_ShouldThrowArgumentException_WhenAmountIsZero()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _cashServices.AddCash(0, Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task AddCash_ShouldThrowArgumentException_WhenResultingBalanceIsZeroOrNegative()
        {
            var idCashed = Guid.NewGuid();
            var idCash = Guid.NewGuid();
            var existingCash = new Cash(50, idCashed) { Id = idCash };

            _mockRepository.Setup(r => r.GetByFilter(It.IsAny<Expression<Func<Cash, bool>>>()))
                .ReturnsAsync(new List<Cash> { existingCash });

            await Assert.ThrowsAsync<ArgumentException>(() => _cashServices.AddCash(100, idCashed, idCash));
        }

        [Fact]
        public async Task AddCash_ShouldAddNewCash_WhenCashDoesNotExist()
        {
            var idCashed = Guid.NewGuid();
            var idCash = Guid.NewGuid();
            _mockRepository.Setup(r => r.GetByFilter(It.IsAny<Expression<Func<Cash, bool>>>()))
                .ReturnsAsync(new List<Cash>() { new Cash(1000, idCashed) { Id = idCash} });

            await _cashServices.AddCash(100, idCashed, idCash);

            _mockRepository.Verify(r => r.Update(It.IsAny<Cash>()), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task AddCash_ShouldUpdateExistingCash_WhenCashExists()
        {
            var idCashed = Guid.NewGuid();
            var idCash = Guid.NewGuid();
            var existingCash = new Cash(200, idCashed) { Id = idCash};

            _mockRepository.Setup(r => r.GetByFilter(It.IsAny<Expression<Func<Cash, bool>>>()))
                .ReturnsAsync(new List<Cash> { existingCash });

            await _cashServices.AddCash(50, idCashed, idCash);

            _mockRepository.Verify(r => r.Update(It.IsAny<Cash>()), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }
    }
}
