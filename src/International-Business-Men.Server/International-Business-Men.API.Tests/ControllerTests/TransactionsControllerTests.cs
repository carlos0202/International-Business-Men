using AutoMapper;
using International_Business_Men.API.Controllers;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using International_Business_Men.DL.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace International_Business_Men.API.Tests.ControllerTests
{
    public class TransactionsControllerTests: IClassFixture<TestFixture<Startup>>
    {
        private Mock<IRepository<ProductTransaction>> OnlineRepository { get; }
        private Mock<ILocalSourceRepository<ProductTransaction>> LocalRepository { get; }
        private IService<ProductTransaction> Service { get; }
        private TransactionsController TestController { get; }

        public TransactionsControllerTests(TestFixture<Startup> fixture)
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

            Service = new TransactionService(OnlineRepository.Object, LocalRepository.Object);
            var Mapper = (IMapper)fixture.Server.Host.Services.GetService(typeof(IMapper));
            var Logger = (ILogger<TransactionsController>)fixture.Server.Host.Services.GetService(typeof(ILogger<TransactionsController>));
            TestController = new TransactionsController(Service, Mapper, Logger);
        }

        [Fact]
        public void Can_Get_All()
        {
            // Act
            var transactions = TestController.Get().Result;
            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(transactions.Result);
        }

        [Fact]
        public void Can_Get_By_SKU()
        {
            // Arrange
            var sku = "2600";

            // Act
            var transactions = TestController.GetBySku(sku).Result;
            // Assert
            Assert.Single(transactions);
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
            var transactions = TestController.Get().Result;
            var filteredTransactions = Service.Where(s => s.SKU == SKU).First();

            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            LocalRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(transactions.Result);
            OnlineRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            LocalRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            Assert.Equal(SKU, filteredTransactions.SKU);
            Assert.Equal("CAD", filteredTransactions.Currency);
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
            var transactions = TestController.Get().Result;
            var filteredTransactions = Service.Where(s => s.SKU == SKU).First();

            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            LocalRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(transactions.Result);
            OnlineRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            LocalRepository.Verify(x => x.Where(s => s.SKU == SKU), Times.Once);
            Assert.Equal(SKU, filteredTransactions.SKU);
            Assert.Equal("CAD", filteredTransactions.Currency);
        }
    }
}
