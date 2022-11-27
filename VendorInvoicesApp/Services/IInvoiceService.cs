using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public interface IInvoiceService
    {
        Invoice GetActiveInvoice();
        List<Invoice> GetAllInvoicesByVendorId(int vendorId);
        List<PaymentTerms> GetAllPaymentTerms();
        List<InvoiceLineItem> GetInvoiceLineItemsByInvoiceId(int invoiceId);
        PaymentTerms GetPaymentTermOfActiveInvoiceById(Invoice invoice);
        double GetTotalAmountOfLineItemsByInvoiceId(int invoideId);


    }
}
