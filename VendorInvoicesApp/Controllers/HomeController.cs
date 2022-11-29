/*.cs
 * Purpose: This controller will be serving as the vendor's list page, adding of vendor, editing a vendor and deleting a vendor.
 * 
 * Revision History:
 *          Created by Kimberly Rose Dela Cruz on November 28, 2022
 */
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VendorInvoicesApp.Models;

namespace VendorInvoicesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //this is to redirect the Vendor's list directly when the page loads. I used the HomeController and assign the direct to action to use the GetAllVendors Action from the Vendors Controller.
        //I am passing the filterIndex as 1 to send the default filter index that will show the A-E filtering of list of vendors
        public IActionResult Index(int filterIndex = 1)
        {
            return RedirectToAction("GetAllVendors", "Vendors", new {filterIndex});
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}