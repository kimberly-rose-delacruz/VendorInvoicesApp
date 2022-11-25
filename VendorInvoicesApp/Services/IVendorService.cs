using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public interface IVendorService
    {
        bool IsPhoneNumberUnique(string phoneNumber);
        ICollection<Vendor> GetAllVendorsByGroupLink(int selectedGroupLink);
        ICollection<Vendor> GetAllVendors();
        void AddVendor(Vendor vendor);
        int GetFilterIndexBasedOnName(string name);
        Vendor GetVendorById(int id);
        void UpdateVendor(Vendor vendor);
    }
}
