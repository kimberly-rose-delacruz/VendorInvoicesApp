using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VendorInvoicesApp.Entities;
using VendorInvoicesApp.Models;
using VendorInvoicesApp.Services;

namespace VendorInvoicesApp.Controllers
{
    public class InvoiceController : Controller
    {

        private IInvoiceService _invoiceService;
        private IVendorService _vendorService;

        public InvoiceController(IInvoiceService invoiceService, IVendorService vendorService)
        {
            _invoiceService = invoiceService;
            _vendorService = vendorService;
        }

        [HttpGet("/vendors/{id}")]
        public IActionResult GetAllInvoice(int id)
        {
            Vendor vendor = _vendorService.GetVendorById(id);
            List<Invoice> invoices = _invoiceService.GetAllInvoicesByVendorId(id);
            Invoice activeInvoice = invoices.FirstOrDefault();
            List<PaymentTerms> terms = _invoiceService.GetAllPaymentTerms();

            ViewBag.VendorFilterIndex = _vendorService.GetFilterIndexBasedOnName(vendor.Name);

            InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
            {
                Invoices = invoices,
                ActiveVendor = vendor,
                Terms = terms
            };

            if (activeInvoice !=null)
            {
                return RedirectToAction("GetInvoiceById", new { id = vendor.VendorId, invoiceId = activeInvoice.InvoiceId });
            }
            else
            {
                //otherwise it will not show the line items and also will show an empty list of invoice.
                return View("Invoices", invoicesViewModel);
            }
        }

        [HttpGet("/vendors/{id}/invoices/{invoiceId}")]
        public IActionResult GetInvoiceById(int id,int invoiceId)
        {
            Vendor vendor = _vendorService.GetVendorById(id);
            List<Invoice> invoices = _invoiceService.GetAllInvoicesByVendorId(id);
            Invoice activeInvoice = invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            List<PaymentTerms> terms = _invoiceService.GetAllPaymentTerms();
            ViewBag.VendorFilterIndex = _vendorService.GetFilterIndexBasedOnName(vendor.Name);

            InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
            {
                Invoices = invoices,
                ActiveVendor = vendor,
                Terms = terms
            };

            if (activeInvoice != null)
            {
                invoicesViewModel.ActiveInvoice = activeInvoice;
                invoicesViewModel.Term = _invoiceService.GetPaymentTermOfActiveInvoiceById(activeInvoice);
                invoicesViewModel.Terms = terms;
                invoicesViewModel.InvoiceLineItems = _invoiceService.GetInvoiceLineItemsByInvoiceId(activeInvoice.InvoiceId);
                invoicesViewModel.TotalAmountOfActiveLineItems = _invoiceService.GetTotalAmountOfLineItemsByInvoiceId(activeInvoice.InvoiceId);
            }
            return View("Invoices", invoicesViewModel);
        }

        [HttpPost("/vendors/{id}/add-invoice-request")]
        public IActionResult ProcessAddOfInvoice(int id, [Bind(Prefix = "Term.PaymentTermsId")]int paymentTermId, [Bind(Prefix = "ActiveInvoice.InvoiceId")]int activeInvoiceId, [Bind(Prefix ="NewInvoice")]Invoice newInvoice)
        {
            //set all new invoice values according to the data of invoicedate and term selected from form
            newInvoice.VendorId = id;
            Vendor linkedVendorToInvoice = _vendorService.GetVendorById(id);
            newInvoice.PaymentTermsId = paymentTermId;
            newInvoice.PaymentTerms = _invoiceService.GetPaymentTermOfAnInvoice(paymentTermId);
           

            if (ModelState.IsValid)
            {
                _invoiceService.AddInvoice(newInvoice);

                return RedirectToAction("GetInvoiceById", new { id = id, invoiceId = newInvoice.InvoiceId });
            }
            else
            {
                Vendor vendor = _vendorService.GetVendorById(id);
                List<Invoice> invoices = _invoiceService.GetAllInvoicesByVendorId(id);
                Invoice activeInvoice = invoices.FirstOrDefault(i => i.InvoiceId == activeInvoiceId);
                List<PaymentTerms> terms = _invoiceService.GetAllPaymentTerms();
                ViewBag.VendorFilterIndex = _vendorService.GetFilterIndexBasedOnName(vendor.Name);

                InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
                {
                    Invoices = invoices,
                    ActiveVendor = vendor,
                    Terms = terms
                };


                if (activeInvoice != null)
                {

                    invoicesViewModel.ActiveInvoice = activeInvoice;
                    invoicesViewModel.Term = _invoiceService.GetPaymentTermOfActiveInvoiceById(activeInvoice);
                    invoicesViewModel.Terms = terms;
                    invoicesViewModel.InvoiceLineItems = _invoiceService.GetInvoiceLineItemsByInvoiceId(activeInvoice.InvoiceId);
                    invoicesViewModel.TotalAmountOfActiveLineItems = _invoiceService.GetTotalAmountOfLineItemsByInvoiceId(activeInvoice.InvoiceId);
                }

                return View("Invoices", invoicesViewModel);
            }

        }

        [HttpPost("/vendors/{id}/invoices/{invoiceId}/add-line-item-request")]
        public IActionResult ProcessAddOfInvoiceLineItem(int id, int invoiceId, [Bind(Prefix = "LineItem")] InvoiceLineItem lineItem)
        {
            //set all values of description and amount in creating the line item for an invoiceId

            lineItem.InvoiceId = invoiceId;
            Vendor linkedVendorToInvoice = _vendorService.GetVendorById(id);

            if (ModelState.IsValid)
            {
                _invoiceService.AddInvoiceLineItem(lineItem);

                return RedirectToAction("GetInvoiceById", new { id = id, invoiceId = invoiceId });
            }
            else
            {
                Vendor vendor = _vendorService.GetVendorById(id);
                List<Invoice> invoices = _invoiceService.GetAllInvoicesByVendorId(id);
                Invoice activeInvoice = invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
                List<PaymentTerms> terms = _invoiceService.GetAllPaymentTerms();
                ViewBag.VendorFilterIndex = _vendorService.GetFilterIndexBasedOnName(vendor.Name);

                InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
                {
                    Invoices = invoices,
                    ActiveVendor = vendor,
                    Terms = terms
                };


                if (activeInvoice != null)
                {

                    invoicesViewModel.ActiveInvoice = activeInvoice;
                    invoicesViewModel.Term = _invoiceService.GetPaymentTermOfActiveInvoiceById(activeInvoice);
                    invoicesViewModel.Terms = terms;
                    invoicesViewModel.InvoiceLineItems = _invoiceService.GetInvoiceLineItemsByInvoiceId(activeInvoice.InvoiceId);
                    invoicesViewModel.TotalAmountOfActiveLineItems = _invoiceService.GetTotalAmountOfLineItemsByInvoiceId(activeInvoice.InvoiceId);
                }

                return View("Invoices", invoicesViewModel);
            }

        }
    }
}
