using International_Business_Men.API.Models;
using International_Business_Men.DAL.Models;
using International_Business_Men.DL.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace International_Business_Men.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyRatesController
    {
        private readonly ILogger<CurrencyRatesController> _logger;
        private readonly IService<CurrencyRate> _currencyRatesService;

        public CurrencyRatesController(ILogger<CurrencyRatesController> logger, 
            IService<CurrencyRate> currencyRatesService)
        {
            _logger = logger;
            _currencyRatesService = currencyRatesService;
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

            return result;
        }

    }
}
