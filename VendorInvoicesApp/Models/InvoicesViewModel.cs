using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Models
{
    public class InvoicesViewModel
    {
        public List<Invoice> Invoices { get; set; }

        public List<InvoiceLineItem> InvoiceLineItems { get; set; }


        public Vendor ActiveVendor { get; set; }

        public PaymentTerms Term { get; set; }

        public Invoice ActiveInvoice { get; set; }

        public List<PaymentTerms> Terms { get; set; }

        public double TotalAmountOfActiveLineItems { get; set; }

        public InvoiceLineItem LineItem { get; set; }

        public Invoice NewInvoice { get; set; }
    }
}
