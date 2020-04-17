using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TransactionImporter.Rest.Formatters
{
    public class XmlImportFormatter
    {
        public async Task<List<T>> ReadStreamXmlAsync<T>(IFormFile formFile) where T : class
        {
            var model = new List<T>();
            using (var readStream = formFile.OpenReadStream())
            {
                using var streamReader = new StreamReader(readStream);
                var xmlDoc = await XDocument.LoadAsync(streamReader, LoadOptions.None, CancellationToken.None);
                xmlDoc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();

                var serializer = new XmlSerializer(typeof(T));
                foreach (var node in xmlDoc.Root.Nodes())
                {
                    var element = serializer.Deserialize(node.CreateReader());
                    model.Add((T)element);
                }
            }

            return model;
        }
    }
}
