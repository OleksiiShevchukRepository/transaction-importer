using System;
using System.Xml.Serialization;

namespace TransactionImporter.Rest.Models
{
    [Serializable]
    [XmlRoot("PaymentDetails")]
    public class PaymentDetail
    {
        [XmlElement("Amount", IsNullable = true)]
        public decimal? Amount { get; set; }

        [XmlElement("CurrencyCode", IsNullable = true)]
        public string CurrencyCode { get; set; }
    }
}
