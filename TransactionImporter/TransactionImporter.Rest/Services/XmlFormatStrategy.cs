using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionImporter.Rest.Formatters;

namespace TransactionImporter.Rest.Services
{
    public class XmlFormatStrategy : IFormatStrategy
    {
        private readonly XmlImportFormatter _xmlImportFormatter;
        private readonly IMapper _mapper;
        public XmlFormatStrategy(IMapper mapper)
        {
            _mapper = mapper;
            _xmlImportFormatter = new XmlImportFormatter();
        }

        public async Task<List<TOut>> Format<TOut, TIn>(IFormFile formFile)
            where TOut : class
            where TIn : class
        {
            var formattedModel = await _xmlImportFormatter.ReadStreamXmlAsync<TIn>(formFile);
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
