using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public interface IVendorService
    {
        bool IsPhoneNumberUnique(string phoneNumber);
        ICollection<Vendor> GetAllVendorsByGroupLink(int selectedGroupLink);
        string GetActiveVendorByGroup(int id);
    }
}
