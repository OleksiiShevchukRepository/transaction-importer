using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionImporter.DataAccess.Entities;

namespace TransactionImporter.Services
{
    public interface ITransactionImporter
    {
        Task ImportTransactions(IEnumerable<Transaction> transactions);
    }
}