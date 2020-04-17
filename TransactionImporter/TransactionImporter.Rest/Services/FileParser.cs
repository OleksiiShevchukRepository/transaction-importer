using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionImporter.Rest.Models;

namespace TransactionImporter.Rest.Services
{
    public class FileParser
    {
        private IFormatStrategy _formatStrategy;
        private readonly IMapper _mapper;

        private readonly string[] csvContentTypes = { "text/csv", "application/vnd.ms-excel" };
        private readonly string[] xmlContentTypes = { "application/xml", "text/xml" };

        public FileParser(IMapper mapper)
           => _mapper = mapper;

        public async Task<List<TransactionImportModel>> CreateTransactionModelFromFile(IFormFile file)
        {
            List<TransactionImportModel> modelList;
            switch (file.ContentType)
            {
                case string a when csvContentTypes.Contains(a):
                    _formatStrategy = new CsvFormatStrategy(_mapper);
                    modelList = await _formatStrategy.Format<TransactionImportModel, TransactionCsv>(file);
                    break;
                case string a when xmlContentTypes.Contains(a):
                    _formatStrategy = new XmlFormatStrategy(_mapper);
                    modelList = await _formatStrategy.Format<TransactionImportModel, TransactionXml>(file);
                    break;
                default:
                    modelList = null;
                    break;
            }

            return modelList;
        }
    }
}