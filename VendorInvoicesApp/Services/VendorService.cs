using System.Numerics;
using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public class VendorService : IVendorService
    {
        private VendorDbContext _vendorDbContext;
        private ICollection<Vendor> vendors;
        public VendorService(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public bool IsPhoneNumberUnique(string phoneNumber)
        {
            bool result = false;
            var number = _vendorDbContext.Vendors.Where(v => v.VendorPhone == phoneNumber).FirstOrDefault();

            if (number == null)
            {
                result = true;
            }

            return result;
        }

        public ICollection<Vendor> GetAllVendors()
        {
            var vendors = _vendorDbContext.Vendors.OrderByDescending(v => v.Name).Where(v=>v.IsDeleted == false).ToList();
            return vendors;
        }

        public ICollection<Vendor> GetAllVendorsByGroupLink(int selectedGroupLink)
        {
            if (selectedGroupLink == 1)
            {
                //get list of vendors A-E by default
                vendors = _vendorDbContext.Vendors.Where(v=>v.IsDeleted==false).Where(v => v.Name.StartsWith("A") || v.Name.StartsWith("a") || v.Name.StartsWith("B")
                || v.Name.StartsWith("b") || v.Name.StartsWith("C") || v.Name.StartsWith("c") || v.Name.StartsWith("D") || v.Name.StartsWith("d") || v.Name.StartsWith("e") || v.Name.StartsWith("E")).ToList();
            }
            if (selectedGroupLink == 2)
            {
                //get list of vendors F-K 2nd Group Link
                vendors = _vendorDbContext.Vendors.Where(v => v.IsDeleted == false).Where(v => v.Name.StartsWith("F") || v.Name.StartsWith("f") || v.Name.StartsWith("g")
                || v.Name.StartsWith("G") || v.Name.StartsWith("H") || v.Name.StartsWith("h") || v.Name.StartsWith("i") || v.Name.StartsWith("I") || v.Name.StartsWith("J") || v.Name.StartsWith("j") || v.Name.StartsWith("K") || v.Name.StartsWith("k")).ToList();
            }
            if (selectedGroupLink == 3)
            {
                //get list of vendors L-R 3rd Group Link
                vendors = _vendorDbContext.Vendors.Where(v => v.IsDeleted == false).Where(v => v.IsDeleted == false).Where(v => v.Name.StartsWith("L") || v.Name.StartsWith("l") || v.Name.StartsWith("m")
                || v.Name.StartsWith("M") || v.Name.StartsWith("n") || v.Name.StartsWith("N") || v.Name.StartsWith("O") || v.Name.StartsWith("o") || v.Name.StartsWith("P") || v.Name.StartsWith("p") || v.Name.StartsWith("Q") || v.Name.StartsWith("q") || v.Name.StartsWith("R") || v.Name.StartsWith("r")).ToList();
            }
            if (selectedGroupLink == 4)
            {
                //get list of vendors S-Z Group Link
                vendors = _vendorDbContext.Vendors.Where(v => v.IsDeleted == false).Where(v => v.Name.StartsWith("s") || v.Name.StartsWith("S") || v.Name.StartsWith("T")
                || v.Name.StartsWith("t") || v.Name.StartsWith("U") || v.Name.StartsWith("u") || v.Name.StartsWith("V") || v.Name.StartsWith("v") || v.Name.StartsWith("W") || v.Name.StartsWith("w") || v.Name.StartsWith("x") || v.Name.StartsWith("X") || v.Name.StartsWith("Y") || v.Name.StartsWith("y") || v.Name.StartsWith("z") || v.Name.StartsWith("Z")).ToList();
            }

            return vendors;
        }
        public void AddVendor(Vendor vendor)
        {
            _vendorDbContext.Vendors.Add(vendor);
            _vendorDbContext.SaveChanges();
        }

        public Vendor GetVendorById(int id)
        {
            Vendor vendor = _vendorDbContext.Vendors.Find(id);

            return vendor;
        }

        
        public void UpdateVendor(Vendor updateVendor)
        {
            _vendorDbContext.Update(updateVendor);
            _vendorDbContext.SaveChanges();
        }

        public int GetFilterIndexBasedOnName(string vendorName)
        {
            int index = 0;
            if (vendorName.StartsWith("A") || vendorName.StartsWith("a") || vendorName.StartsWith("B")
                || vendorName.StartsWith("b") || vendorName.StartsWith("C") || vendorName.StartsWith("c") || vendorName.StartsWith("D") || vendorName.StartsWith("d") || vendorName.StartsWith("e") || vendorName.StartsWith("E"))
            {
                index = 1;
            }
            else if (vendorName.StartsWith("F") || vendorName.StartsWith("f") || vendorName.StartsWith("g")
                || vendorName.StartsWith("G") || vendorName.StartsWith("H") || vendorName.StartsWith("h") || vendorName.StartsWith("i") || vendorName.StartsWith("I") || vendorName.StartsWith("J") || vendorName.StartsWith("j") || vendorName.StartsWith("K") || vendorName.StartsWith("k"))
            {
                index = 2;
            }
            else if (vendorName.StartsWith("L") || vendorName.StartsWith("l") || vendorName.StartsWith("m")
                || vendorName.StartsWith("M") || vendorName.StartsWith("n") || vendorName.StartsWith("N") || vendorName.StartsWith("O") || vendorName.StartsWith("o") || vendorName.StartsWith("P") || vendorName.StartsWith("p") || vendorName.StartsWith("Q") || vendorName.StartsWith("q") || vendorName.StartsWith("R") || vendorName.StartsWith("r"))
            {
                index = 3;
            }
            else
            {
                index = 4;
            }

            return index;

        }

        public void UpdateIsDeleteStatusToYes(int vendorId)
        {
            var vendor = _vendorDbContext.Vendors.Find(vendorId);
            if(vendor != null)
            {
                vendor.IsDeleted = true;

                _vendorDbContext.Update(vendor);
                _vendorDbContext.SaveChanges();
            }
        }

        public void UpdateIsDeletedStatusToNo(int vendorId)
        {
            var vendor = _vendorDbContext.Vendors.Find(vendorId);
            if (vendor != null)
            {
                vendor.IsDeleted = false;

                _vendorDbContext.Update(vendor);
                _vendorDbContext.SaveChanges();
            }
        }

    }
}
