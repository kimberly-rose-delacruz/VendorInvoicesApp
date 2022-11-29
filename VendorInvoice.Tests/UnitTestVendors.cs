using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using VendorInvoiceLibrary.Entities;
using VendorInvoiceLibrary.Services;
using VendorInvoicesApp.Controllers;
using VendorInvoicesApp.DatabaseAccess;
using VendorInvoicesApp.Services;

namespace VendorInvoice.Tests
{
    public class UnitTestVendors
    { 

        [Fact]
        public void TestVendorNameShouldBeCorrectBasedOnFilteringIndex()
        {
            //Microsoft.EntityFrameworkCore.InMemory nuget package used this one to mock the dbcontext seed my own data and test it in here.
            var options = new DbContextOptionsBuilder<VendorDbContext>()
                .UseInMemoryDatabase(databaseName: "VendorsKDelacruz7597")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new VendorDbContext(options))
            {
                context.Vendors.Add(new Vendor()
                {
                    IsDeleted = false,
                    VendorId = 1,
                    Name = "Allen Hill Company",
                    Address1 = "PO Box 87373",
                    Address2 = "",
                    City = "Chicago",
                    ProvinceOrState = "IL",
                    ZipOrPostalCode = "60290",
                    VendorPhone = "+14844458615",
                    VendorContactFirstName = "Allen",
                    VendorContactLastName = "Hill",
                    VendorContactEmail = "allen.hill@company.com"
                });

                context.Vendors.Add(new Vendor() { IsDeleted = false, VendorId = 2, Name = "Bolton Gas & Electric", Address1 = "Box 52001", Address2 = "", City = "San Francisco", ProvinceOrState = "CA", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" });
                
                context.Vendors.Add(new Vendor() { IsDeleted = false, VendorId = 3, Name = "Carling Publishers Weekly", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" });

                context.SaveChanges();
            }

            //create a variable for regex expression to see if there is a match within the list of vendors I added as a seed data.
            var filterOfVendorNameNotMatchFromAToE = @"^[a-eA-E]";

            //use a clean instance of the context to run the test
            using (var context = new VendorDbContext(options))
            {
                VendorService vendorService = new VendorService(context);
                var indexAToE = 1;
                var vendors = vendorService.GetAllVendorsByGroupLink(indexAToE);

                foreach(var vendor in vendors)
                {
                    //assert if Expected should be true when passing correct data for Vendor's Name starts with A to E should be equal to the actual test for regex expression of if it matches to the list of vendor we added in the database In memory.
                    Assert.Equal(true, Regex.IsMatch(vendor.Name, filterOfVendorNameNotMatchFromAToE));
                }
            }
        }

        [Fact]
        public void TestCorrecctCalculatedTotalnvoiceLineItemsAmount()
        {
            var options = new DbContextOptionsBuilder<VendorDbContext>()
                .UseInMemoryDatabase(databaseName: "VendorsKDelacruz7597")
                .Options;

            using (var context = new VendorDbContext(options))
            {
                context.Invoices.Add(new Invoice()
                {
                    InvoiceId = 1,
                    InvoiceDate = new DateTime(2022, 11, 24).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 1,
                    PaymentTermsId = 1
                });

                context.InvoiceLineItems.Add(new InvoiceLineItem()
                {
                    InvoiceLineItemId = 1,
                    InvoiceId = 1,
                    Description = "Line Item 1",
                    Amount = 1100.42
                });

                context.InvoiceLineItems.Add(new InvoiceLineItem()
                {
                    InvoiceLineItemId = 2,
                    InvoiceId = 1,
                    Description = "Line Item 2",
                    Amount = 120.00
                });

                context.InvoiceLineItems.Add(new InvoiceLineItem()
                {
                    InvoiceLineItemId = 3,
                    InvoiceId = 1,
                    Description = "Line Item 3",
                    Amount = 550.00
                });

                context.SaveChanges();
            }

            using (var context = new VendorDbContext(options))
            {
                InvoiceService invoiceService = new InvoiceService(context);
                int invoiceId = 1;
                double expectedTotalAmount = 1100.42 + 120.00 + 550.00;
                //testing the service from the invoice services.
              double actualTotalAmount =   invoiceService.GetTotalAmountOfLineItemsByInvoiceId(invoiceId);

                Assert.Equal(expectedTotalAmount, actualTotalAmount);
            }
        }

        [Fact]
        public void TestVendorZipOrPostalCodeBasedOnUSOrCanada()
        {
            //Microsoft.EntityFrameworkCore.InMemory nuget package used this one to mock the dbcontext seed my own data and test it in here.
            var options = new DbContextOptionsBuilder<VendorDbContext>()
                .UseInMemoryDatabase(databaseName: "VendorsKDelacruz7597")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new VendorDbContext(options))
            {
                context.Vendors.Add(new Vendor()
                {
                    IsDeleted = false,
                    VendorId = 1,
                    Name = "Allen Hill Company",
                    Address1 = "PO Box 87373",
                    Address2 = "",
                    City = "Chicago",
                    ProvinceOrState = "IL",
                    ZipOrPostalCode = "60290",
                    VendorPhone = "+14844458615",
                    VendorContactFirstName = "Allen",
                    VendorContactLastName = "Hill",
                    VendorContactEmail = "allen.hill@company.com"
                });

                context.Vendors.Add(new Vendor() { IsDeleted = false, VendorId = 2, Name = "Bolton Gas & Electric", Address1 = "Box 52001", Address2 = "", City = "Waterloo", ProvinceOrState = "CA", ZipOrPostalCode = "N2J0B3", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" });

                context.SaveChanges();
            }

            //create a variable for regex expression to see if the zip or postal code will match on the following regular expressions based on the added data above.
            var usRegexZip = @"^\d{5}(?:[-\s]\d{4})?$";
            var canadaRegexZip = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

            //use a clean instance of the context to run the test
            using (var context = new VendorDbContext(options))
            {
                VendorService vendorService = new VendorService(context);
                var indexAToE = 1;
                var vendors = vendorService.GetAllVendorsByGroupLink(indexAToE);

                foreach (var vendor in vendors)
                {
                    //assert if Expected should be true when passing correct data for Vendor's Name starts with A to E should be equal to the actual test for regex expression of if it matches to the list of vendor we added in the database In memory.
                    Assert.Equal(true, Regex.IsMatch(vendor.Name, filterOfVendorNameNotMatchFromAToE));
                }
            }
        }
    }
}