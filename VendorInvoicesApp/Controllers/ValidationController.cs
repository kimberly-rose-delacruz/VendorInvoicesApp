﻿/*InvoiceController.cs
 * Purpose: This is the controller some of the remotely validation of some of the fields for vendor entity.
 *
 * Revision History
 *      Created by Kimberly Rose Dela Cruz on November 28, 2022
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using VendorInvoiceLibrary.Entities;
using VendorInvoiceLibrary.Services;

namespace VendorInvoicesApp.Controllers
{
    public class ValidationController : Controller
    {
        private IVendorService _vendorService;
        public ValidationController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        //this is the validation controller to validate the specific fields remotely using a controller 
        //this will return the message if its valid or not.
        //the below code is for checking the province or state length should only be equivalent to 2.
        public IActionResult CheckProvinceLength(string provinceOrState)
        {
            string msgResult = CheckProvinceStateLength(provinceOrState);

            if(string.IsNullOrEmpty(msgResult))
            {
                return Json(true);
            }
            else
            {
                return Json(msgResult);
            }

            return View();
        }

        //this is actual validation for province state or length
        private string CheckProvinceStateLength(string provinceOrState)
        {
            string msg = "";
            if(provinceOrState.Length != 2)
            {
                msg = "Only 2 characters are allowed for province or state. i.e. ON";
            }

            return msg;
        }

        //this is for the validation of checking the postal code
        public IActionResult CheckPostalcode(string zipOrPostalCode)
        {
            string msgResult = ValidateZipPostalCode(zipOrPostalCode);

            if (string.IsNullOrEmpty(msgResult))
            {
                //this next line allows the client-side data validation libraries to see this field as valid.
                TempData["okPhone"] = true;
                return Json(true);
            }
            else
            {
                return Json(msgResult);
            }

            return View();
        }

        //the actual checking for the zip or postal code for both usa and canada.
        private string ValidateZipPostalCode(string zipCode)
        {
            string msg = "";
            var usRegexZip = @"^\d{5}(?:[-\s]\d{4})?$";
            var canadaRegexZip = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

            if ((!Regex.Match(zipCode, usRegexZip).Success &&
                !Regex.Match(zipCode, canadaRegexZip).Success))
            {
                msg = "Please insert a valid us or canada zip or postal code.";
            }

            return msg;
        }

        //checking if phone number is  unique based from the database.
        public IActionResult CheckPhoneNum(string vendorPhone)
        {
           var msgResult = ValidatePhoneNum(vendorPhone);

            if (string.IsNullOrEmpty(msgResult))
            {
                //this next line allows the client-side data validation libraries to see this field as valid.
                return Json(true);
            }
            else
            {
                return Json(msgResult);
            }


            return View();
        }

        //this is the actual checking from the service whethere the phone number is unique.
        private string ValidatePhoneNum(string phoneNumber)
        {
            string msg = "";
            bool isPhoneUnique = false;
            isPhoneUnique = _vendorService.IsPhoneNumberUnique(phoneNumber);

            if (isPhoneUnique == false)
            {
                msg = $"The phone number {phoneNumber} is already in use.";
            }

            return msg;
        }
    }
}
