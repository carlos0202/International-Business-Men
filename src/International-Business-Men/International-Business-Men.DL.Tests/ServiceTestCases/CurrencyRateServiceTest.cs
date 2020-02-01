using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using International_Business_Men.DL.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace International_Business_Men.DL.Tests.ServiceTestCases
{
    public class CurrencyRateServiceTest
    {

        private Mock<IRepository<CurrencyRate>> OnlineRepository { get; }
        private Mock<ILocalSourceRepository<CurrencyRate>> LocalRepository { get; }
        private IService<CurrencyRate> Service { get; }


        public CurrencyRateServiceTest()
        {
            var rates = new List<CurrencyRate>
            {
                new CurrencyRate
                {
                    From = "USD",
                    To = "CAD",
                    Rate = 1.97M
                }
            };

            OnlineRepository = new Mock<IRepository<CurrencyRate>>();
            OnlineRepository.Setup(x => x.GetAll())
                .ReturnsAsync(rates);
            OnlineRepository.Setup(x => x.Where(It.IsAny<Expression<Func<CurrencyRate, bool>>>()))
                .Returns((Expression<Func<CurrencyRate, bool>> exp) => rates.AsQueryable().Where(exp));

            LocalRepository = new Mock<ILocalSourceRepository<CurrencyRate>>();
            LocalRepository.Setup(x => x.GetAll())
                .ReturnsAsync(rates);
            LocalRepository.Setup(x => x.Where(It.IsAny<Expression<Func<CurrencyRate, bool>>>()))
                .Returns((Expression<Func<CurrencyRate, bool>> exp) => rates.AsQueryable().Where(exp));
            LocalRepository.Setup(x => x.Refresh(rates));

            Service = new CurrencyRateService(OnlineRepository.Object, LocalRepository.Object);
        }

        [Fact]
        public void Can_Get_All()
        {
            // Act
            var rates = Service.GetAsync().Result;
            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(rates);
        }

        [Fact]
        public void Can_Filter_Rates()
        {
            // Arrange
            var From = "USD";

            // Act
            var filteredRates = Service.Where(s => s.From == From).First();

            // Assert
            OnlineRepository.Verify(x => x.Where(s => s.From == From), Times.Once);
            Assert.Equal(From, filteredRates.From);
            Assert.Equal("CAD", filteredRates.To);
        }

        [Fact]
        public void Can_Get_From_Local_When_Online_Empty()
        {
            // Arrange
            var From = "USD";
            OnlineRepository.Setup(x => x.GetAll())
                .ReturnsAsync(Enumerable.Empty<CurrencyRate>());
            OnlineRepository.Setup(x => x.Where(It.IsAny<Expression<Func<CurrencyRate, bool>>>()))
                .Returns((Expression<Func<CurrencyRate, bool>> exp) => Enumerable.Empty<CurrencyRate>());

            // Act
            var rates = Service.GetAsync().Result;
            var filteredRates = Service.Where(s => s.From == From).First();

            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            LocalRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(rates);
            OnlineRepository.Verify(x => x.Where(s => s.From == From), Times.Once);
            LocalRepository.Verify(x => x.Where(s => s.From == From), Times.Once);
            Assert.Equal(From, filteredRates.From);
            Assert.Equal("CAD", filteredRates.To);
        }

        [Fact]
        public void Can_Get_From_Local_When_Online_Not_Available()
        {
            // Arrange
            var From = "USD";
            OnlineRepository.Setup(x => x.GetAll())
                .Throws(new Exception());
            OnlineRepository.Setup(x => x.Where(It.IsAny<Expression<Func<CurrencyRate, bool>>>()))
                .Throws(new Exception());

            // Act
            var rates = Service.GetAsync().Result;
            var filteredRates = Service.Where(s => s.From == From).First();

            // Assert
            OnlineRepository.Verify(x => x.GetAll(), Times.Once);
            LocalRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.Single(rates);
            OnlineRepository.Verify(x => x.Where(s => s.From == From), Times.Once);
            LocalRepository.Verify(x => x.Where(s => s.From == From), Times.Once);
            Assert.Equal(From, filteredRates.From);
            Assert.Equal("CAD", filteredRates.To);
        }
    }
}
