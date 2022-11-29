using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace VendorInvoiceLibrary.Entities
{
    public class Vendor
    {
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter an address.")]
        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        [Required(ErrorMessage = "Please enter a city.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Please enter a valid province or state.")]
        [Remote("CheckProvinceLength", "Validation")]
        public string? ProvinceOrState { get; set; }

        [Required(ErrorMessage = "Please enter a valid zip or postal code.")]
        [Remote("CheckPostalcode", "Validation")]
        public string? ZipOrPostalCode { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Remote("CheckPhoneNum", "Validation")]
        public string? VendorPhone { get; set; }

        public string? VendorContactLastName { get; set; }

        public string? VendorContactFirstName { get; set; }

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? VendorContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}
