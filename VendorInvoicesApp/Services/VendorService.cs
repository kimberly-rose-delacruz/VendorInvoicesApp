using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public class VendorService:IVendorService
    {
        private VendorDbContext _vendorDbContext;
        private ICollection<Vendor> vendors;
        public VendorService(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public bool IsPhoneNumberUnique(string phoneNumber)
        {
            var result = false;
            List<string> phoneNumbers = _vendorDbContext.Vendors.Select(v => v.VendorPhone).ToList();

            foreach (var phone in phoneNumbers)
            {
                if (phoneNumber != phone)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public ICollection<Vendor> GetAllVendorsByGroupLink(int selectedGroupLink)
        {
            if(selectedGroupLink == 1)
            {
                //get list of vendors A-E by default
                vendors =  _vendorDbContext.Vendors.Where(v => v.Name.StartsWith("A") || v.Name.StartsWith("a") || v.Name.StartsWith("B")
                || v.Name.StartsWith("b") || v.Name.StartsWith("C") || v.Name.StartsWith("c") || v.Name.StartsWith("D") || v.Name.StartsWith("d") || v.Name.StartsWith("e") || v.Name.StartsWith("E")).ToList();
            }
            if(selectedGroupLink == 2)
            {
                //get list of vendors F-K 2nd Group Link
                vendors = _vendorDbContext.Vendors.Where(v => v.Name.StartsWith("F") || v.Name.StartsWith("f") || v.Name.StartsWith("g")
                || v.Name.StartsWith("G") || v.Name.StartsWith("H") || v.Name.StartsWith("h") || v.Name.StartsWith("i") || v.Name.StartsWith("I") || v.Name.StartsWith("J") || v.Name.StartsWith("j") || v.Name.StartsWith("K") || v.Name.StartsWith("k")).ToList();
            }
            if (selectedGroupLink == 3)
            {
                //get list of vendors L-R 3rd Group Link
                vendors = _vendorDbContext.Vendors.Where(v => v.Name.StartsWith("L") || v.Name.StartsWith("l") || v.Name.StartsWith("m")
                || v.Name.StartsWith("M") || v.Name.StartsWith("n") || v.Name.StartsWith("N") || v.Name.StartsWith("O") || v.Name.StartsWith("o") || v.Name.StartsWith("P") || v.Name.StartsWith("p") || v.Name.StartsWith("Q") || v.Name.StartsWith("q") || v.Name.StartsWith("R") || v.Name.StartsWith("r")).ToList();
            }
            if (selectedGroupLink == 4)
            {
                //get list of vendors S-Z Group Link
                vendors = _vendorDbContext.Vendors.Where(v => v.Name.StartsWith("s") || v.Name.StartsWith("S") || v.Name.StartsWith("T")
                || v.Name.StartsWith("t") || v.Name.StartsWith("U") || v.Name.StartsWith("u") || v.Name.StartsWith("V") || v.Name.StartsWith("v") || v.Name.StartsWith("W") || v.Name.StartsWith("w") || v.Name.StartsWith("x") || v.Name.StartsWith("X") || v.Name.StartsWith("Y") || v.Name.StartsWith("y") || v.Name.StartsWith("z") || v.Name.StartsWith("Z")).ToList();
            }

            return vendors;
        }

        public string GetActiveVendorByGroup(int id)
        {
            if(id == 1)
            {
                return "A-E";
            }
            else if(id==2)
            {
                return "F-K";
            }
            else if(id==3)
            {
                return "L-R";
            }
            else
            {
                return "S-Z";
            }
        }
    }
}
