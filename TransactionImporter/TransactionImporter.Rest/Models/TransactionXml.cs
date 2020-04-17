using System;
using System.Xml.Serialization;

namespace TransactionImporter.Rest.Models
{
    [Serializable]
    [XmlRoot("Transaction")]
    public class TransactionXml
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlElement("TransactionDate", IsNullable = true)]
        public DateTime? TransactionDate { get; set; }

        [XmlIgnore]
        public XmlTransactionStatus? TransactionStatus { get; set; }

        [XmlElement("PaymentDetails", IsNullable = true)]
        public PaymentDetail PaymentDetails { get; set; }

        [XmlElement("Status", IsNullable = true)]
        public string TransactionStatusAsString
        {
            get { return TransactionStatus.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(XmlTransactionStatus), value))
                    TransactionStatus = null;
                else
                    TransactionStatus = (XmlTransactionStatus)Enum.Parse(typeof(XmlTransactionStatus), value);
            }
        }
    }
}
