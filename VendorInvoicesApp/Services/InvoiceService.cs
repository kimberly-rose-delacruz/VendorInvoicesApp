using Microsoft.EntityFrameworkCore;
using VendorInvoiceLibrary.Entities;
using VendorInvoiceLibrary.Services;
using VendorInvoicesApp.DatabaseAccess;

namespace VendorInvoicesApp.Services
{
    public class InvoiceService : IInvoiceService
    {
        private VendorDbContext _vendorDbContext;
        public InvoiceService(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public List<Invoice> GetAllInvoicesByVendorId(int vendorId)
        {
            return _vendorDbContext.Invoices.Include(i => i.PaymentTerms).Where(i => i.VendorId == vendorId).ToList();
        }

        public List<InvoiceLineItem> GetInvoiceLineItemsByInvoiceId(int invoiceId)
        {
            return _vendorDbContext.InvoiceLineItems.Where(i => i.InvoiceId == invoiceId).OrderBy(i => i.Description).ToList();
        }

        public PaymentTerms GetPaymentTermOfActiveInvoiceById(Invoice invoice)
        {
            PaymentTerms term = _vendorDbContext.Terms.Where(p => p.PaymentTermsId == invoice.PaymentTermsId).FirstOrDefault();

            return term;
        }

        public List<PaymentTerms> GetAllPaymentTerms()
        {
            return _vendorDbContext.Terms.ToList();
        }

        public double GetTotalAmountOfLineItemsByInvoiceId(int invoideId)
        {
            double totalAmountOfLineItems = 0;

            List<InvoiceLineItem> items = _vendorDbContext.InvoiceLineItems.Where(i => i.InvoiceId == invoideId).ToList();

            totalAmountOfLineItems = (double)items.Select(i => i.Amount).DefaultIfEmpty().Sum();
            return totalAmountOfLineItems;
        }

        public void AddInvoice(Invoice invoice)
        {
            _vendorDbContext.Add(invoice);
            _vendorDbContext.SaveChanges();
        }

        public void AddInvoiceLineItem(InvoiceLineItem lineItem)
        {
            _vendorDbContext.Add(lineItem);
            _vendorDbContext.SaveChanges();
        }

        public PaymentTerms GetPaymentTermOfAnInvoice(int termId)
        {
            return _vendorDbContext.Terms.Where(p => p.PaymentTermsId == termId).FirstOrDefault();
        }
    }
}
