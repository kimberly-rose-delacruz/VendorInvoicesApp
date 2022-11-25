using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("/vendors")]
        public IActionResult GetAllVendors(int filterIndex = 1)
        {
            ICollection<Vendor> vendors = _vendorService.GetAllVendorsByGroupLink(filterIndex);
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Vendors = vendors,
            };

            return View("VendorsList",vendorViewModel);
        }

        [HttpGet("/vendors/add-new-vendor")]
        public IActionResult GetAddVendorRequest()
        {
            ViewBag.ActionTitle = "Add";
            return View("Add", new Vendor());
        }

        [HttpPost("/vendors")]
        public IActionResult AddVendor(Vendor newVendor)
        {
            int filterIndex = _vendorService.GetFilterIndexBasedOnName(newVendor.Name);
            ICollection<Vendor> vendors = _vendorService.GetAllVendorsByGroupLink(filterIndex);

            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Vendors = vendors,
                ActiveVendor = newVendor
            };

            if (ModelState.IsValid)
            {
                _vendorService.AddVendor(vendorViewModel.ActiveVendor);

                return RedirectToAction("GetAllVendors", "Vendors");
            }
            else
            {
                return View("Add", vendorViewModel.ActiveVendor);

            }
        }

        [HttpGet("/vendors/{id}/edit-vendor")]
        public IActionResult GetEditVendorById(int id)
        {
            Vendor vendor = _vendorService.GetVendorById(id);

            return View("Edit", vendor);
        }

        [HttpPost("/vendors/{id}")]

        public IActionResult ProcessEditVendorRequest(Vendor editVendor)
        {

            int filterIndex = _vendorService.GetFilterIndexBasedOnName(editVendor.Name);
            ICollection<Vendor> vendors = _vendorService.GetAllVendorsByGroupLink(filterIndex);

            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Vendors = vendors,
                ActiveVendor = editVendor
            };

            if (ModelState.IsValid)
            {
                _vendorService.UpdateVendor(vendorViewModel.ActiveVendor);

                return RedirectToAction("GetAllVendors", "Vendors");
            }
            else
            {
                return View("Edit", vendorViewModel.ActiveVendor);
            }
        }


    }

}
