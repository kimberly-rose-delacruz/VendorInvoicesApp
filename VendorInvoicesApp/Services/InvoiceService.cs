using Microsoft.EntityFrameworkCore;
using VendorInvoicesApp.Entities;

namespace VendorInvoicesApp.Services
{
    public class InvoiceService: IInvoiceService
    {
        private VendorDbContext _vendorDbContext;
        public InvoiceService(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }


        public List<Invoice> GetAllInvoicesByVendorId(int vendorId)
        {
             return _vendorDbContext.Invoices.Include(i=>i.PaymentTerms).Where(i=>i.VendorId == vendorId).ToList();
        }

        public List<InvoiceLineItem> GetInvoiceLineItemsByInvoiceId(int invoiceId)
        {
            return _vendorDbContext.InvoiceLineItems.Where(i=>i.InvoiceId == invoiceId).OrderByDescending(i=>i.Description).ToList();
        }


        public Invoice GetActiveInvoice()
        {
            return _vendorDbContext.Invoices.FirstOrDefault();
        }
        public PaymentTerms GetPaymentTermOfActiveInvoiceById(Invoice invoice)
        {
           PaymentTerms term = _vendorDbContext.Terms.Where(p=>p.PaymentTermsId == invoice.PaymentTermsId).FirstOrDefault();

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
    }
}
