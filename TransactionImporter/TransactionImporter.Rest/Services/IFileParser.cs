using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionImporter.Rest.Models;

namespace TransactionImporter.Rest.Services
{
    public interface IFileParser
    {
        Task<List<TransactionImportModel>> CreateTransactionModelFromFile(IFormFile file);
    }
}