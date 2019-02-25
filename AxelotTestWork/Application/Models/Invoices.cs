using System;
using System.Collections.Generic;

namespace AxelotTestWork.Application.Models
{
    public class Invoices
    {
        public Invoices()
        {
            InvoiceItems = new HashSet<InvoiceItems>();
        }

        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual IEnumerable<InvoiceItems> InvoiceItems { get; set; }
    }
}
