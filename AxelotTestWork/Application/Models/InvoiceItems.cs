namespace AxelotTestWork.Application.Models
{
    public class InvoiceItems
    {
        public long InvoiceLineId { get; set; }
        public long InvoiceId { get; set; }
        public long TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public long Quantity { get; set; }

        public virtual Invoices Invoice { get; set; }
        public virtual Tracks Track { get; set; }
    }
}
