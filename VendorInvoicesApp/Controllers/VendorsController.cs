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
        public IActionResult GetAllVendors(int filterIndex=1)
        {
            ViewBag.FilterIndex = filterIndex;
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

                return RedirectToAction("GetAllVendors", "Vendors", new { filterIndex });
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

        [HttpPost("/vendors/{id}/edit-requests")]
        public IActionResult ProcessEditVendorRequest(Vendor editVendor)
        {

            int filterIndex = _vendorService.GetFilterIndexBasedOnName(editVendor.Name);

            if (ModelState.IsValid)
            {

                _vendorService.UpdateVendor(editVendor);

                return RedirectToAction("GetAllVendors", "Vendors", new { filterIndex });
            }
            else
            { 
                return View("Edit", editVendor);
            }
        }

        [HttpGet("/vendors/{id}/delete-vendor")]
        public IActionResult GetDeleteVendorById(int id)
        {
            Vendor vendor = _vendorService.GetVendorById(id);

            return View("Delete", vendor);
        }

        [HttpPost("/vendors/{id}/delete")]
        public IActionResult ProcessSoftDeleteVendorRequest(int id)
        {
            Vendor vendor = _vendorService.GetVendorById(id);
            _vendorService.UpdateIsDeleteStatusToYes(id);
            TempData["UndoVendorDeletion"] = $"{vendor.Name}";
            TempData["UndoVendorForUndoDeletionID"] = $"{vendor.VendorId}";

            return RedirectToAction("GetAllVendors", "Vendors");
        }

        [HttpGet("/vendors/{id}/undo-delete")]
        public IActionResult ProcessUndoSoftDeleteVendorRequest(int id)
        {
            _vendorService.UpdateIsDeletedStatusToNo(id);
            return RedirectToAction("GetAllVendors", "Vendors");
        }

    }

}
