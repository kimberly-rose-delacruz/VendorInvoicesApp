/*InvoiceController.cs
 * Purpose: This is the controller for all invoices of a vendor. This includes the adding of invoice and within that invoice you can add a new line item.
 *
 * Revision History
 *      Created by Kimberly Rose Dela Cruz on November 28, 2022
 */
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VendorInvoiceLibrary.Entities;
using VendorInvoicesApp.Models;
using VendorInvoiceLibrary.Services;

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

        //this http get will serves as getting all invoice by vendorId
        [HttpGet("/vendors/{id}")]
        public IActionResult GetAllInvoice(int id)
        {
            //by doing so we can now prepare all the data for viewing in the invoice page of a certain vendor.
            Vendor vendor = _vendorService.GetVendorById(id);

            //invoices can be retrieved using vendor's id
            List<Invoice> invoices = _invoiceService.GetAllInvoicesByVendorId(id);
            
            //by doing this we can get the first or default activeInvoice.
            Invoice activeInvoice = invoices.FirstOrDefault();

            //terms can also be processed (for the form)
            List<PaymentTerms> terms = _invoiceService.GetAllPaymentTerms();

            //saving the filterIndex for the going back to specific alphabetical order that were previously visited by the user.
            ViewBag.VendorFilterIndex = _vendorService.GetFilterIndexBasedOnName(vendor.Name);

            //using viewmodel i mapped each of the data
            InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
            {
                Invoices = invoices,
                ActiveVendor = vendor,
                Terms = terms
            };

            //checking if the activeinvoice is not equal to null.
            if (activeInvoice !=null)
            {
                //if yes redirect it using GetInvoiceById so it will redirected to a new action where it aill get the invoice by id.
                return RedirectToAction("GetInvoiceById", new { id = vendor.VendorId, invoiceId = activeInvoice.InvoiceId });
            }
            else
            {

                //otherwise it will not show the line items and also will show an empty list of invoice.
                return View("Invoices", invoicesViewModel);
            }
        }

        //when the active invoice has a data for a specific vendor that has been visited by the user. it will be getting this action
        [HttpGet("/vendors/{id}/invoices/{invoiceId}")]
        public IActionResult GetInvoiceById(int id,int invoiceId)
        {
            //same case from the previous, collating all data.
            Vendor vendor = _vendorService.GetVendorById(id);
            List<Invoice> invoices = _invoiceService.GetAllInvoicesByVendorId(id);
            Invoice activeInvoice = invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            List<PaymentTerms> terms = _invoiceService.GetAllPaymentTerms();
            ViewBag.VendorFilterIndex = _vendorService.GetFilterIndexBasedOnName(vendor.Name);

            //store all of it in the mapping of invoices view model.
            InvoicesViewModel invoicesViewModel = new InvoicesViewModel()
            {
                Invoices = invoices,
                ActiveVendor = vendor,
                Terms = terms
            };

            //validating again if the active invoice is not equal to null
            if (activeInvoice != null)
            {
                invoicesViewModel.ActiveInvoice = activeInvoice;
                invoicesViewModel.Term = _invoiceService.GetPaymentTermOfActiveInvoiceById(activeInvoice);
                invoicesViewModel.Terms = terms;
                invoicesViewModel.InvoiceLineItems = _invoiceService.GetInvoiceLineItemsByInvoiceId(activeInvoice.InvoiceId);
                invoicesViewModel.TotalAmountOfActiveLineItems = _invoiceService.GetTotalAmountOfLineItemsByInvoiceId(activeInvoice.InvoiceId);
            }

            //then return all of it into the Invoices View to view all data including the line items if there is.
            return View("Invoices", invoicesViewModel);
        }

        //this is to process a post request to add an invoice within a vendor
        [HttpPost("/vendors/{id}/add-invoice-request")]
        public IActionResult ProcessAddOfInvoice(int id, [Bind(Prefix = "Term.PaymentTermsId")]int paymentTermId, [Bind(Prefix = "ActiveInvoice.InvoiceId")]int activeInvoiceId, [Bind(Prefix ="NewInvoice")]Invoice newInvoice)
        {
            //set all new invoice values according to the data of invoicedate and term selected from form
            newInvoice.VendorId = id;
            Vendor linkedVendorToInvoice = _vendorService.GetVendorById(id);
            newInvoice.PaymentTermsId = paymentTermId;
            newInvoice.PaymentTerms = _invoiceService.GetPaymentTermOfAnInvoice(paymentTermId);
           
            //if all fields were validated as valid it will push inside this statement
            if (ModelState.IsValid)
            {
                //adding the invoice using the service.
                _invoiceService.AddInvoice(newInvoice);

                //then redirect the user to the GetInvoiceId Iaction which is just the same page within the form.
                return RedirectToAction("GetInvoiceById", new { id = id, invoiceId = newInvoice.InvoiceId });
            }
            else
            {
                //otherwise it will be validated again. and show if the invoice date field is blank.
                //all of the below mapping are just to ensure that the data is still intact.
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

        //processing the add line item request for a specific invoice.
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
                //otherwise it will validated the amount and description field and view the error message in the invoices page.
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
