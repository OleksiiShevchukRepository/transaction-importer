using LinqKit;
using System;
using TransactionImporter.DataAccess.Entities;
using TransactionImporter.Rest.Models;

namespace TransactionImporter.Rest.Helpers
{
    public class PredicateCreator
    {
        public static Func<Transaction, bool> CreateTransactionFilter(string currencyCode, DateTime? dateFrom,
            DateTime? dateTo, string status)
        {
            var predicate = PredicateBuilder.New<Transaction>(true);

            if (!string.IsNullOrEmpty(currencyCode))
                predicate = predicate.And(c => c.CurrencyCode == currencyCode);

            if (dateFrom != null)
                predicate = predicate.And(c => c.TransactionDate >= dateFrom);

            if (dateTo != null)
                predicate = predicate.And(c => c.TransactionDate <= dateTo);

            if (!string.IsNullOrEmpty(status) && Enum.IsDefined(typeof(TransactionStatusEnum), status))
                predicate = predicate.And(c => 
                    c.TransactionStatus == (TransactionStatusEnum)Enum.Parse(typeof(TransactionStatus), status));

            return predicate.Compile();
        }
    }
}