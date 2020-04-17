using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionImporter.Rest.Formatters;

namespace TransactionImporter.Rest.Services
{
    public class CsvFormatStrategy : IFormatStrategy
    {
        private readonly CsvImportFormatter _csvImportFormatter;
        private readonly IMapper _mapper;
        public CsvFormatStrategy(IMapper mapper)
        {
            _csvImportFormatter = new CsvImportFormatter(new CsvFormatterOptions());
            _mapper = mapper;
        }

        public async Task<List<TOut>> Format<TOut, TIn>(IFormFile formFile)
            where TOut : class
            where TIn : class
        {
            var formattedModel = await _csvImportFormatter.ReadStreamCsvAsync<TIn>(formFile);
            var outputList = new List<TOut>();
            foreach (var item in formattedModel)
            {
                var outputItem = _mapper.Map<TOut>(item);
                outputList.Add(outputItem);
            }

            return outputList;
        }
    }
}