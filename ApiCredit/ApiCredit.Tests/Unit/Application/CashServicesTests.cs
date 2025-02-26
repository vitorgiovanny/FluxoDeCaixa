using ApiCredit.Application.Services;
using ApiCredit.Domain.Entities;
using ApiCredit.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Tests.Unit.Application
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
        public async Task AddCash_ThrowsException_WhenAmountIsZeroOrNegative()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _cashServices.AddCash(0, Guid.NewGuid(), Guid.NewGuid()));
            await Assert.ThrowsAsync<ArgumentException>(() => _cashServices.AddCash(-10, Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task AddCash_AddsNewCash_WhenCashDoesNotExist()
        {
            // Arrange
            var idCashed = Guid.NewGuid();
            var idCash = Guid.NewGuid();
            var newCash = new Cash(0, idCashed) { Id = Guid.Empty };
            _mockRepository.Setup(r => r.GetByFilter(It.IsAny<Expression<Func<Cash, bool>>>()))
                .ReturnsAsync(new List<Cash>());

            // Act
            await _cashServices.AddCash(100, idCashed, idCashed);

            // Assert
            _mockRepository.Verify(r => r.Add(It.IsAny<Cash>()), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task AddCash_UpdatesExistingCash_WhenCashExists()
        {
            // Arrange
            var idCashed = Guid.NewGuid();
            var idCash = Guid.NewGuid();
            var existingCash = new Cash(50, idCashed) { Id = Guid.NewGuid() };
            _mockRepository.Setup(r => r.GetByFilter(It.IsAny<Expression<Func<Cash, bool>>>()))
                .ReturnsAsync(new List<Cash> { existingCash });

            // Act
            await _cashServices.AddCash(100, idCashed, idCashed);

            // Assert
            _mockRepository.Verify(r => r.Update(It.IsAny<Cash>()), Times.Once);
            _mockRepository.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public async Task AddCash_PublishesMessage()
        {
            // Arrange
            var idCashed = Guid.NewGuid();
            var idCash = Guid.NewGuid();
            var existingCash = new Cash(50, idCashed) { Id = Guid.NewGuid() };
            _mockRepository.Setup(r => r.GetByFilter(It.IsAny<Expression<Func<Cash, bool>>>()))
                .ReturnsAsync(new List<Cash> { existingCash });

            // Act
            await _cashServices.AddCash(100, idCashed, idCashed);

            // Assert
            _mockPublisher.Verify(p => p.PublicarMensagem(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
