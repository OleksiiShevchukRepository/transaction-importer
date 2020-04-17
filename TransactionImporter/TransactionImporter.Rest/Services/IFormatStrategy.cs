using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransactionImporter.Rest.Services
{
    public interface IFormatStrategy
    {
        Task<List<TOut>> Format<TOut, TIn>(IFormFile formFile) where TIn : class where TOut : class;
    }
}
