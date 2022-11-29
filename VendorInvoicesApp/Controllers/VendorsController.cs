/*VendorsController.cs
 * Purpose: This controller will be serving as the vendor's list page, adding of vendor, editing a vendor and deleting a vendor.
 * 
 * Revision History:
 *          Created by Kimberly Rose Dela Cruz on November 28, 2022
 */
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VendorInvoiceLibrary.Entities;
using VendorInvoicesApp.Models;
using VendorInvoicesApp.DatabaseAccess;
using VendorInvoiceLibrary.Services;

namespace VendorInvoicesApp.Controllers
{
    public class VendorsController : Controller
    {
        private IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }


        //this action will be serve as the getting of all the vendors based from the group link with filter index that already filters it accordingly to the pagination I applied in the UI.
        [HttpGet("/vendors")]
        public IActionResult GetAllVendors(int filterIndex=1)
        {
            ViewBag.FilterIndex = filterIndex;

            //this Icollections of vendor will process based from the service that I created from the IVendorService as a method to retrieve all vendors based on the filterIndex provided on it.
            ICollection<Vendor> vendors = _vendorService.GetAllVendorsByGroupLink(filterIndex);

            //I included this vendorViewModel since I am in need to get the activeVendor among all the vendors I retrieve from it.
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Vendors = vendors,
            };

            //then return all these vendors to the VendorsList View
            return View("VendorsList",vendorViewModel);
        }

        //this request will get the form of add new vendor.
        [HttpGet("/vendors/add-new-vendor")]
        public IActionResult GetAddVendorRequest(int filterIndex)
        {
            ViewBag.ActionTitle = "Add";
            ViewBag.FilterIndex = filterIndex;
            return View("Add", new Vendor());
        }

        //this post will process all values frmo the form based from user's input and validate if all input were validated based on the requirements I filled out in the entities of vendor.cs
        [HttpPost("/vendors")]
        public IActionResult AddVendor(Vendor newVendor)
        {
            //get filterIndex based on newVendor Name
            int filterIndex = _vendorService.GetFilterIndexBasedOnName(newVendor.Name);
            //Get all vendors by grouplink 
            ICollection<Vendor> vendors = _vendorService.GetAllVendorsByGroupLink(filterIndex);

            //insert it in the vendorViewModel to assign an activeVendor and all the vendors
            VendorViewModel vendorViewModel = new VendorViewModel()
            {
                Vendors = vendors,
                ActiveVendor = newVendor
            };

            //validate if all input are valid in the add vendor form.
            if (ModelState.IsValid)
            {
                //add the vendor based on the Active Vendor
                _vendorService.AddVendor(vendorViewModel.ActiveVendor);

                return RedirectToAction("GetAllVendors", "Vendors", new { filterIndex });
            }
            else
            {
                //else show all errors for incorrect input in the Add Vendor View Page.
                return View("Add", vendorViewModel.ActiveVendor);

            }
        }

        //this is to get an existing vendor for editing
        [HttpGet("/vendors/{id}/edit-vendor")]
        public IActionResult GetEditVendorById(int id, int filterIndex)
        {
            Vendor vendor = _vendorService.GetVendorById(id);
            ViewBag.FilterIndex = filterIndex;
            return View("Edit", vendor);
        }

        //this is to process all the input from the user to edit the form of the vendor selected from the list.
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
        public IActionResult GetDeleteVendorById(int id, int filterIndex)
        {
            Vendor vendor = _vendorService.GetVendorById(id);
            ViewBag.FilterIndex = filterIndex;
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
