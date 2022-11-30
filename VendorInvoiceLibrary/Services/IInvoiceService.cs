/*This whole entities folder is just transferred here in the class library to be used for the unit testing.
 */
using VendorInvoiceLibrary.Entities;

namespace VendorInvoiceLibrary.Services
{
    public interface IInvoiceService
    {
        List<Invoice> GetAllInvoicesByVendorId(int vendorId);
        List<PaymentTerms> GetAllPaymentTerms();
        List<InvoiceLineItem> GetInvoiceLineItemsByInvoiceId(int invoiceId);
        PaymentTerms GetPaymentTermOfActiveInvoiceById(Invoice invoice);
        double GetTotalAmountOfLineItemsByInvoiceId(int invoideId);
        void AddInvoice(Invoice invoice);
        PaymentTerms GetPaymentTermOfAnInvoice(int termId);
        void AddInvoiceLineItem(InvoiceLineItem invoiceLineItem);

    }
}
