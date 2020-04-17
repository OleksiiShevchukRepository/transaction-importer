using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TransactionImporter.Rest.Models;

namespace TransactionImporter.Rest.Validators
{
    public class TransactionModelValidator
    {
        public static ModelStateDictionary ValidateModels(ModelStateDictionary modelState, IEnumerable<TransactionImportModel> entities)
        {
            var collectedModelErrors = entities
                .SelectMany(e => ValidateModelEntity(e));

            foreach (var error in collectedModelErrors)
                modelState.AddModelError(error.ErrorKey, error.ErrorMessage);

            return modelState;
        }

        private static IEnumerable<(string ErrorKey, string ErrorMessage)> ValidateModelEntity(TransactionImportModel entity)
        {
            var entityIdSpecified = !string.IsNullOrWhiteSpace(entity.Id);
            var entityIdInterpolated = entityIdSpecified ? $" : {entity.Id} " : string.Empty;

            var modelErrorsCollection = new List<(string errorKey, string errorMessage)>();
            if (!entityIdSpecified)
                modelErrorsCollection.Add(("Id", "Transaction Id is not specified."));
            if (entityIdSpecified && entity.Id.Length > 50)
                modelErrorsCollection.Add(("Id", $"Transaction Id{entityIdInterpolated} length must be no greater than 50 characters."));

            var currencyCodeSpecified = !string.IsNullOrEmpty(entity.CurrencyCode);
            if (!currencyCodeSpecified)
                modelErrorsCollection.Add(("CurrencyCode", $"CurrencyCode for Transaction{entityIdInterpolated} was not specified."));
            else
            {
                var currencyIso4217Match = Regex.Match(entity.CurrencyCode ?? string.Empty, "[A-Z]{3}");
                if (!currencyIso4217Match.Success || currencyIso4217Match.Value != entity.CurrencyCode)
                    modelErrorsCollection.Add(("CurrencyCode", $"Currency Code for Transaction{entityIdInterpolated} doesn't comply with ISO_4217 format."));
            }

            if (entity.TransactionDate == null)
                modelErrorsCollection.Add(("TransactionDate", $"TransactionDate for Transaction{entityIdInterpolated} doesn't comply with appropriate format."));

            if (entity.TransactionStatus == null)
                modelErrorsCollection.Add(("TransactionStatus", $"TransactionStatus for Transaction{entityIdInterpolated} was not specified or has wrong format."));

            if (entity.Amount == null)
                modelErrorsCollection.Add(("Amount", $"Amount for Transaction{entityIdInterpolated} was not specified."));

            return modelErrorsCollection;
        }
    }
}