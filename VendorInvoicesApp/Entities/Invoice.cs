using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VendorInvoicesApp.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        
        [Required(ErrorMessage = "Please enter an invoice date.")]
        public DateTime? InvoiceDate { get; set; }

        public DateTime? InvoiceDueDate
        {
            get
            {
                return InvoiceDate?.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }

        public double? PaymentTotal { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; }

        // FK:
        public int PaymentTermsId { get; set; }

        // Nav to terms:
        public PaymentTerms? PaymentTerms { get; set; }

        // FK:
        public int VendorId { get; set; }

        // Nav to vendor
        public Vendor? Vendor { get; set; }

        // Nav to line items:
        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
    }
}
