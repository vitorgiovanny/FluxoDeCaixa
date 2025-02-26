using CashBalance.Application.Services;
using CashBalance.Domain;
using CashBalance.Domain.Entities;
using CashBalance.Domain.Interfaces;
using CashBalance.Domain.Interfaces.Repository;
using Moq;

namespace ApiBalance.Test.Unit.Application
{
    public class CashierServicesTests
    {
        private readonly Mock<IRepository<Cashier>> _cashierRepositoryMock;
        private readonly Mock<IRepository<Cash>> _repositoryCashMock;
        private readonly CashierServices _cashierServices;

        public CashierServicesTests()
        {
            _cashierRepositoryMock = new Mock<IRepository<Cashier>>();
            _repositoryCashMock = new Mock<IRepository<Cash>>();
            _cashierServices = new CashierServices(_cashierRepositoryMock.Object, _repositoryCashMock.Object);
        }

        [Fact]
        public async Task CreateCashier_ShouldCreateCashier_WhenNameIsValid()
        {
            // Arrange
            string name = "Test Cashier";

            // Act
            var result = await _cashierServices.CreateCashier(name);

            // Assert
            Assert.NotNull(result.Item1);
            Assert.Equal(name, result.Item1.Name);
            _cashierRepositoryMock.Verify(repo => repo.Add(It.IsAny<Cashier>()), Times.Once);
            _repositoryCashMock.Verify(repo => repo.Add(It.IsAny<Cash>()), Times.Once);
            _cashierRepositoryMock.Verify(repo => repo.Save(), Times.Exactly(2));
        }

        [Fact]
        public async Task CreateCashier_ShouldThrowException_WhenNameIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cashierServices.CreateCashier(null));
        }

        [Fact]
        public async Task GetCashier_ShouldReturnCashier_WhenIdIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var cashier = new Cashier { Id = id, Name = "Test Cashier" };
            _cashierRepositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(cashier);

            // Act
            var result = await _cashierServices.GetCashier(id);

            // Assert
            Assert.Equal(cashier, result);
        }

        [Fact]
        public async Task GetCashier_ShouldThrowException_WhenIdIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cashierServices.GetCashier(Guid.Empty));
        }

        [Fact]
        public async Task GetAllCashiers_ShouldReturnListOfCashiers()
        {
            // Arrange
            var cashiers = new List<Cashier> { new Cashier(), new Cashier() };
            _cashierRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(cashiers);

            // Act
            var result = await _cashierServices.GetAllCashiers();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateCashier_ShouldUpdateCashier_WhenValid()
        {
            // Arrange
            var cashier = new Cashier { Id = Guid.NewGuid(), Name = "Updated Cashier" };

            // Act
            var result = await _cashierServices.UpdateCashier(cashier);

            // Assert
            Assert.Equal(cashier, result);
            _cashierRepositoryMock.Verify(repo => repo.Update(cashier), Times.Once);
            _cashierRepositoryMock.Verify(repo => repo.Save(), Times.Once);
        }

        [Fact]
        public async Task UpdateCashier_ShouldThrowException_WhenCashierIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cashierServices.UpdateCashier(null));
        }

        [Fact]
        public async Task DeleteCashier_ShouldCallUpdate_WhenIdIsValid()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _cashierServices.DeleteCashier(id);

            // Assert
            _cashierRepositoryMock.Verify(repo => repo.Update(It.Is<Cashier>(c => c.Id == id)), Times.Once);
        }

        [Fact]
        public async Task DeleteCashier_ShouldThrowException_WhenIdIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cashierServices.DeleteCashier(Guid.Empty));
        }
    }
}