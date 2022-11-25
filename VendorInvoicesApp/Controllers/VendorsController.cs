using Microsoft.AspNetCore.Mvc;
using VendorInvoicesApp.Entities;
using VendorInvoicesApp.Models;
using VendorInvoicesApp.Services;

namespace VendorInvoicesApp.Controllers
{
    public class VendorsController : Controller
    {
        private IVendorService _vendorService;


        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet("/vendors/{id}")]
        public IActionResult GetAllVendors(int id)
        {
            ICollection<Vendor> vendors = _vendorService.GetAllVendorsByGroupLink(id);
            string activeVendorGroup = _vendorService.GetActiveVendorByGroup(id);

            if (id == 1 || id == 0)
             {
                VendorViewModel vendorViewModel = new VendorViewModel()
                {
                    Vendors = vendors,
                    SelectedAlphabeticalGroupLink = id,
                    ActiveVendor = activeVendorGroup,
                };
                return RedirectToAction("Items", vendorViewModel);
            }
            else
            {
                VendorViewModel vendorViewModel = new VendorViewModel()
                {
                    Vendors = vendors,
                    SelectedAlphabeticalGroupLink = id,
                    ActiveVendor = activeVendorGroup,
                };
                return RedirectToAction("Items", vendorViewModel);
            }

        }
    }
}
