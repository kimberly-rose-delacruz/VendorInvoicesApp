/*This whole entities folder is just transferred here in the class library to be used for the unit testing.
 */
using VendorInvoiceLibrary.Entities;

namespace VendorInvoiceLibrary.Services
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
