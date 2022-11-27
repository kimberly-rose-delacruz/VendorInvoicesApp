using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public interface IVendorService
    {
        bool IsPhoneNumberUnique(string phoneNumber);
        ICollection<Vendor> GetAllVendorsByGroupLink(int selectedGroupLink);
        void AddVendor(Vendor vendor);
        int GetFilterIndexBasedOnName(string name);
        Vendor GetVendorById(int id);
        void UpdateVendor(Vendor vendor);
        void UpdateIsDeleteStatusToYes(int vendorId);
        void UpdateIsDeletedStatusToNo(int vendorId);

    }
}
