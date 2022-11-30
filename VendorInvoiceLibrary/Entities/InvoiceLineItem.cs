/*This whole entities folder is just transferred here in the class library to be used for the unit testing.
 */
using System.ComponentModel.DataAnnotations;

namespace VendorInvoiceLibrary.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        [Required(ErrorMessage = "Please enter an amount.")]
        public double? Amount { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string? Description { get; set; }

        // FK:
        public int? InvoiceId { get; set; }

        // Nav prop to invoice:
        public Invoice? Invoice { get; set; }
    }
}
