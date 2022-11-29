using VendorInvoiceLibrary.Entities;

namespace VendorInvoicesApp.Models
{
    public class VendorViewModel
    {
        public ICollection<Vendor> Vendors { get; set; }
        public Vendor ActiveVendor { get; set; }
    }
}
