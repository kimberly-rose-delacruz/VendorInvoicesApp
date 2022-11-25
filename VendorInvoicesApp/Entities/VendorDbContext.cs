using Microsoft.EntityFrameworkCore;

namespace VendorInvoicesApp.Entities
{
    public class VendorDbContext : DbContext
    {
        public VendorDbContext(DbContextOptions<VendorDbContext> options) : base(options)
        {

        }

        //Db Sets of the entities to connect to the Database
        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<PaymentTerms> Terms { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasOne(i => i.Vendor).WithMany(v => v.Invoices);
            modelBuilder.Entity<Invoice>().HasOne(i => i.PaymentTerms).WithMany(v => v.Invoices);
            modelBuilder.Entity<InvoiceLineItem>().HasOne(i => i.Invoice).WithMany(i => i.InvoiceLineItems);

            modelBuilder.Entity<InvoiceLineItem>().HasData(
                //1st invoice - line items for Vendor 1
                new InvoiceLineItem
                {
                    InvoiceLineItemId = 1,
                    InvoiceId = 1,
                    Description = "Line Item 1",
                    Amount = 1100.42
                },
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 2,
                   InvoiceId = 1,
                   Description = "Line Item 2",
                   Amount = 120.00
               },
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 3,
                   InvoiceId = 1,
                   Description = "Line Item 3",
                   Amount = 550.00
               },

               //2nd Invoice - line items for Vendor 1
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 4,
                   InvoiceId = 2,
                   Description = "Line Item 1",
                   Amount = 2323.21
               },
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 5,
                   InvoiceId = 2,
                   Description = "Line Item 2",
                   Amount = 199.00
               },
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 6,
                   InvoiceId = 2,
                   Description = "Line Item 3",
                   Amount = 659.00
               },

               //line items for Vendor 2
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 7,
                   InvoiceId = 5,
                   Description = "Line Item 5 for invoice 7",
                   Amount = 123.22
               },
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 8,
                   InvoiceId = 6,
                   Description = "Line Item 6 for invoice 6",
                   Amount = 212
               },
               new InvoiceLineItem
               {
                   InvoiceLineItemId = 9,
                   InvoiceId = 7,
                   Description = "Line Item 7 for invoice 7",
                   Amount = 323.11
               }
               );


            modelBuilder.Entity<Invoice>().HasData(
                //1st Vendor and Payment Term 30
                new Invoice
                {
                    InvoiceId = 1,
                    InvoiceDate = new DateTime(2022, 11, 24).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 1,
                    PaymentTermsId = 1
                },
                new Invoice
                {
                    InvoiceId = 2,
                    InvoiceDate = new DateTime(2022, 12, 08).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 1,
                    PaymentTermsId = 1
                },
                new Invoice
                {
                    InvoiceId = 3,
                    InvoiceDate = new DateTime(2022, 08, 05).AddDays(-30),
                    PaymentTotal = 1221.99,
                    PaymentDate= new DateTime(2022, 08, 01),
                    VendorId = 1,
                    PaymentTermsId = 1
                },
                new Invoice
                {
                    InvoiceId = 4,
                    InvoiceDate = new DateTime(2022, 11, 12).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 1,
                    PaymentTermsId = 1
                },

                //2nd Vendor Payment Term 10
                new Invoice
                {
                    InvoiceId = 5,
                    InvoiceDate = new DateTime(2022, 11, 24).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 2,
                    PaymentTermsId = 3
                },
                new Invoice
                {
                    InvoiceId = 6,
                    InvoiceDate = new DateTime(2022, 12, 08).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 2,
                    PaymentTermsId = 3
                },
                new Invoice
                {
                    InvoiceId = 7,
                    InvoiceDate = new DateTime(2022, 08, 05).AddDays(-30),
                    PaymentTotal = 1221.99,
                    PaymentDate = new DateTime(2022, 08, 01),
                    VendorId = 2,
                    PaymentTermsId = 3
                },
                new Invoice
                {
                    InvoiceId = 8,
                    InvoiceDate = new DateTime(2022, 11, 12).AddDays(-30),
                    PaymentTotal = 0.00,
                    VendorId = 2,
                    PaymentTermsId = 3
                }
                );


            modelBuilder.Entity<Vendor>().HasData(
                new Vendor() { IsDeleted = false, VendorId = 1, Name = "Allen Hill Company", Address1 = "PO Box 87373", Address2 = "", City = "Chicago", ProvinceOrState = "IL", ZipOrPostalCode = "60290", VendorPhone = "+14844458615", VendorContactFirstName = "Allen", VendorContactLastName = "Hill", VendorContactEmail = "allen.hill@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 2, Name = "Bolton Gas & Electric", Address1 = "Box 52001", Address2 = "", City = "San Francisco", ProvinceOrState = "CA", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 3, Name = "Carling Publishers Weekly", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 4, Name = "Famouse Cookies Inc", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 5, Name = "Kimberly Cakes Inc", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 6, Name = "Lalamove Company", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 7, Name = "Romblon Company", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 8, Name = "Southern Importers Company", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 9, Name = "XYZ Company", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" },
                new Vendor() { IsDeleted = false, VendorId = 10, Name = "Zeebra Company", Address1 = "Marion", Address2 = "", City = "Marion County", ProvinceOrState = "OH", ZipOrPostalCode = "94101", VendorPhone = "+14844458715", VendorContactFirstName = "Bolton", VendorContactLastName = "Smith", VendorContactEmail = "bolton.smith@company.com" }
            );

            modelBuilder.Entity<PaymentTerms>().HasData(
                new PaymentTerms()
                {
                    PaymentTermsId = 1,
                    DueDays = 30,
                    Description = "Net 30 days"
                },
                new PaymentTerms()
                {
                    PaymentTermsId = 2,
                    DueDays = 7,
                    Description = "Net 7 days"
                },
                new PaymentTerms()
                {
                    PaymentTermsId = 3,
                    DueDays = 10,
                    Description = "Net 10 days"
                }
                );

        }

    }
}
