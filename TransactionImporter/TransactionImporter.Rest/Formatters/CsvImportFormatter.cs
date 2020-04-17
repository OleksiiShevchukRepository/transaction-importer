using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TransactionImporter.Rest.Formatters
{
    public class CsvImportFormatter
    {
        private readonly CsvFormatterOptions _options;
        public CsvImportFormatter(CsvFormatterOptions options) => _options = options;

        public async Task<List<T>> ReadStreamCsvAsync<T>(IFormFile formFile) where T : class
        {
            var constructedListType = typeof(List<>).MakeGenericType(typeof(T));
            var list = (List<T>)Activator.CreateInstance(constructedListType);

            using (var stream = formFile.OpenReadStream())
            {
                bool skipFirstLine = _options.UseSingleLineHeaderInCsv;
                using (var reader = new StreamReader(stream, _options.Encoding))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        var values = line.Split(_options.CsvDelimiter.ToCharArray());

                        if (skipFirstLine)
                            skipFirstLine = false;
                        else
                        {
                            var itemTypeInGeneric = list.GetType().GetTypeInfo().GenericTypeArguments[0];
                            var item = Activator.CreateInstance(itemTypeInGeneric);
                            var properties = item.GetType().GetProperties().Where(pi => !pi.GetCustomAttributes<JsonIgnoreAttribute>().Any()).ToArray();

                            for (int i = 0; i < values.Length; i++)
                            {
                                object propertyValue;
                                Type propertyType = properties[i].PropertyType;
                                Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                                if (underlyingType.IsEnum)
                                    propertyValue = string.IsNullOrEmpty(values[i]) || !Enum.IsDefined(underlyingType, values[i])
                                        ? default : Enum.Parse(underlyingType, values[i]);
                                else
                                    propertyValue = string.IsNullOrEmpty(values[i]) ? null : Convert.ChangeType(values[i], underlyingType);

                                properties[i].SetValue(item, propertyValue, null);
                            }

                            list.Add((T)item);
                        }
                    }
                }
            }

            return list;
        }
    }
}
