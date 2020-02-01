using AutoMapper;
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
    public class TransactionsController
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IService<ProductTransaction> _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ILogger<TransactionsController> logger, 
            IService<ProductTransaction> transactionService,
            IMapper mapper)
        {
            _logger = logger;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseModel<IEnumerable<TransactionDTO>>> Get()
        {
            var transactions = await _transactionService.GetAsync();
            var result = new ResponseModel<IEnumerable<TransactionDTO>>
            {
                StatusCode = 200,
                Result = _mapper.Map<IEnumerable<TransactionDTO>>(transactions)
            };

            return result;
        }

    }
}
