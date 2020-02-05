using International_Business_Men.API;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using International_Business_Men.DL.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace International_Business_Men.DL.Tests.ServiceTestCases
{
    public class TransactionServiceTest : IClassFixture<TestFixture<Startup>>
    {

        private Mock<IRepository<ProductTransaction>> OnlineRepository { get; }
        private Mock<ILocalSourceRepository<ProductTransaction>> LocalRepository { get; }
        private IService<ProductTransaction> Service { get; }


        public TransactionServiceTest(TestFixture<Startup> fixture)
        {
            var transactions = new List<ProductTransaction>
            {
                new ProductTransaction
                {
                    SKU = "2600",
                    Currency = "CAD",
                    Amount = 91.97M
                }
            };

            OnlineRepository = new Mock<IRepository<ProductTransaction>>();
            OnlineRepository.Setup(x => x.GetAll())
                .ReturnsAsync(transactions);
            OnlineRepository.Setup(x => x.Where(It.IsAny<Expression<Func<ProductTransaction, bool>>>()))
                .Returns((Expression<Func<ProductTransaction, bool>> exp) => transactions.AsQueryable().Where(exp));

            LocalRepository = new Mock<ILocalSourceRepository<ProductTransaction>>();
            LocalRepository.Setup(x => x.GetAll())
                .ReturnsAsync(transactions);
            LocalRepository.Setup(x => x.Where(It.IsAny<Expression<Func<ProductTransaction, bool>>>()))
                .Returns((Expression<Func<ProductTransaction, bool>> exp) => transactions.AsQueryable().Where(exp));
            LocalRepository.Setup(x => x.Refresh(transactions));

            var logger = (ILogger<TransactionService>)fixture.Server.Host.Services.GetService(typeof(ILogger<TransactionService>));
            Service = new TransactionService(OnlineRepository.Object, LocalRepository.Object, logger);
        }

        [Fact]
        public void Can_Get_All()
        {
            // Act
            var transactions = Service.GetAsync().Result;
            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(transactions);
        }

        [Fact]
        public void Can_Filter_Rates()
        {
            // Arrange
            var SKU = "2600";

            // Act
            var filteredRates = Service.Where(s => s.SKU == SKU).First();

            // Assert
            OnlineRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            Assert.Equal(SKU, filteredRates.SKU);
            Assert.Equal("CAD", filteredRates.Currency);
        }

        [Fact]
        public void Can_Get_From_Local_When_Online_Empty()
        {
            // Arrange
            var SKU = "2600";
            OnlineRepository.Setup(x => x.GetAll())
                .ReturnsAsync(Enumerable.Empty<ProductTransaction>());
            OnlineRepository.Setup(x => x.Where(It.IsAny<Expression<Func<ProductTransaction, bool>>>()))
                .Returns((Expression<Func<CurrencyRate, bool>> exp) => Enumerable.Empty<ProductTransaction>());

            // Act
            var rates = Service.GetAsync().Result;
            var filteredRates = Service.Where(s => s.SKU == SKU).First();

            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            LocalRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(rates);
            OnlineRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            LocalRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            Assert.Equal(SKU, filteredRates.SKU);
            Assert.Equal("CAD", filteredRates.Currency);
        }

        [Fact]
        public void Can_Get_From_Local_When_Online_Not_Available()
        {
            // Arrange
            var SKU = "2600";
            OnlineRepository.Setup(x => x.GetAll())
                .Throws(new Exception());
            OnlineRepository.Setup(x => x.Where(It.IsAny<Expression<Func<ProductTransaction, bool>>>()))
                .Throws(new Exception());

            // Act
            var rates = Service.GetAsync().Result;
            var filteredRates = Service.Where(s => s.SKU == SKU).First();

            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            LocalRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(rates);
            OnlineRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            LocalRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            Assert.Equal(SKU, filteredRates.SKU);
            Assert.Equal("CAD", filteredRates.Currency);
        }
    }
}
