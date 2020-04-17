using DummyMvc.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionImporter.DataAccess.Entities;
using TransactionImporter.Services;

namespace DummyMvs.Services
{
    public class TransactionImporter : ITransactionImporter
    {
        private readonly TransactionDbContext _dbContext;
        public TransactionImporter(TransactionDbContext dbContext)
           => _dbContext = dbContext;

        public async Task ImportTransactions(IEnumerable<Transaction> transactions)
        {
            var transToUpdate = transactions.Where(tran => _dbContext.Transactions.Select(tr => tr.Id).Contains(tran.Id));
            var transToInsert = transactions.Where(t => !transToUpdate.Select(tr => tr.Id).Contains(t.Id));

            _dbContext.Transactions.UpdateRange(transToUpdate);
            await _dbContext.AddRangeAsync(transToInsert);
            await _dbContext.SaveChangesAsync();
        }
    }
}