using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionImporter.Rest.Models;

namespace TransactionImporter.Rest.Services
{
    public class FileParser
    {
        private IFormatStrategy _formatStrategy;
        private readonly IMapper _mapper;

        public FileParser(IMapper mapper)
           => _mapper = mapper;

        public async Task<List<TransactionImportModel>> CreateTransactionModelFromFile(IFormFile file)
        {
            List<TransactionImportModel> modelList;
            switch (file.ContentType)
            {
                case "text/csv":
                    _formatStrategy = new CsvFormatStrategy(_mapper);
                    modelList = await _formatStrategy.Format<TransactionImportModel, TransactionCsv>(file);
                    break;
                case "application/xml":
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