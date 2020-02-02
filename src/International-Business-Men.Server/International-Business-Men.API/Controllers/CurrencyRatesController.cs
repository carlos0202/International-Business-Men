using International_Business_Men.API.Models;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace International_Business_Men.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyRatesController
    {
        private readonly IService<CurrencyRate> _currencyRatesService;
        private readonly ILogger _logger;

        public CurrencyRatesController(
            IService<CurrencyRate> currencyRatesService,
            ILogger<CurrencyRatesController> logger)
        {
            _currencyRatesService = currencyRatesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ResponseModel<IEnumerable<CurrencyRate>>> Get()
        {
            var transactions = await _currencyRatesService.GetAsync();
            var result = new ResponseModel<IEnumerable<CurrencyRate>>
            {
                StatusCode = 200,
                Result = transactions
            };
            _logger.LogInformation($"Currency Rates info retreived at: {DateTime.Now}");

            return result;
        }

    }
}
