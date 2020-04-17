using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TransactionImporter.Rest.Services;
using TransactionImporter.Services;
using TransactionImporter.Rest.Validators;
using TransactionImporter.DataAccess.Entities;
using TransactionImporter.Rest.Models;
using TransactionImporter.Rest.Helpers;
using TransactionImporter.Rest.Attributes;

namespace TransactionImporter.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly FileParser _fileParser;
        private readonly ITransactionReader _transactionReader;
        private readonly ITransactionImporter _transactionImporter;
        private readonly IMapper _mapper;

        public TransactionController(IMapper mapper, ITransactionReader transactionReader, ITransactionImporter transactionImporter)
        {
            _mapper = mapper;
            _fileParser = new FileParser(_mapper);
            _transactionReader = transactionReader;
            _transactionImporter = transactionImporter;
        }

        [HttpPost]
        [FormFileValidation(5 * 1024 * 1024, new string[] { ".csv", ".xml" })]
        public async Task<IActionResult> Post()
        {
            var models = await _fileParser.CreateTransactionModelFromFile(Request.Form.Files[0]);
            var modelState = TransactionModelValidator.ValidateModels(ModelState, models);

            if (!modelState.IsValid)
                return BadRequest(modelState);

            var modelsToImport = models.Select(e => _mapper.Map<Transaction>(e));
            await _transactionImporter.ImportTransactions(modelsToImport);

            return Ok("File has been successfully imported.");
        }

        [HttpGet()]
        public ActionResult<List<TransactionReadModel>> Get(string currencyCode = "",
            DateTime? dateFrom = null, DateTime? dateTo = null, string status = "")
        {
            var predicate = PredicateCreator.CreateTransactionFilter(currencyCode, dateFrom, dateTo, status);
            var result = _transactionReader
                .GetTransactionsByPredicate(predicate)
                .Select(t => _mapper.Map<TransactionReadModel>(t));

            return Ok(result);
        }
    }
}