﻿/*This whole entities folder is just transferred here in the class library to be used for the unit testing.
 */
using System.ComponentModel.DataAnnotations;

namespace VendorInvoiceLibrary.Entities
{
    public class PaymentTerms
    {
        public int PaymentTermsId { get; set; }

        public string Description { get; set; } = null!;

        public int DueDays { get; set; }

        // Nav to invoices:
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
