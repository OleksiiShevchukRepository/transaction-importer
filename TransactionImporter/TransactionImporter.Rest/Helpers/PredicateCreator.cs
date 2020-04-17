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
            ExpressionStarter<Transaction> predicate = PredicateBuilder.New<Transaction>(true);

            if (!string.IsNullOrEmpty(currencyCode))
                predicate = predicate.And(c => c.CurrencyCode == currencyCode);

            if (dateFrom != null)
            predicate = predicate.And(c => c.TransactionDate.CompareTo(dateFrom.Value) >= 0);

            if (dateTo != null)
                predicate = predicate.And(c => c.TransactionDate.CompareTo(dateTo.Value) <= 0);

            if (!string.IsNullOrEmpty(status) && Enum.IsDefined(typeof(TransactionStatusEnum), status))
                predicate = predicate.And(c => 
                    c.TransactionStatus == (TransactionStatusEnum)Enum.Parse(typeof(TransactionStatus), status));

            return predicate.Compile();
        }
    }
}