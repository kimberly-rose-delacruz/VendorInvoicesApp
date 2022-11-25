using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Models
{
    public class VendorViewModel
    {
        private List<string> vendorGroups = new List<string>();

        public VendorViewModel()
        {
            vendorGroups.Add("A-E");
            vendorGroups.Add("F-K");
            vendorGroups.Add("L-R");
            vendorGroups.Add("S-Z");

        }

        public ICollection<Vendor> Vendors { get; set; }
        public int? SelectedAlphabeticalGroupLink { get; set; }
        public string? ActiveVendor { get; set; }

        public List<string> ListOfVendorGroupsFilters { get
            { 
                return vendorGroups;
            }
        }


    }
}
