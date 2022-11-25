using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VendorInvoicesApp.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    PaymentTermsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.PaymentTermsId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceOrState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipOrPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTotal = table.Column<double>(type: "float", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentTermsId = table.Column<int>(type: "int", nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Terms_PaymentTermsId",
                        column: x => x.PaymentTermsId,
                        principalTable: "Terms",
                        principalColumn: "PaymentTermsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineItems",
                columns: table => new
                {
                    InvoiceLineItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItems", x => x.InvoiceLineItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId");
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "PaymentTermsId", "Description", "DueDays" },
                values: new object[,]
                {
                    { 1, "Net 30 days", 30 },
                    { 2, "Net 7 days", 7 },
                    { 3, "Net 10 days", 10 }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "Address1", "Address2", "City", "IsDeleted", "Name", "ProvinceOrState", "VendorContactEmail", "VendorContactFirstName", "VendorContactLastName", "VendorPhone", "ZipOrPostalCode" },
                values: new object[,]
                {
                    { 1, "PO Box 87373", "", "Chicago", false, "Allen Hill Company", "IL", "allen.hill@company.com", "Allen", "Hill", "+14844458615", "60290" },
                    { 2, "Box 52001", "", "San Francisco", false, "Bolton Gas & Electric", "CA", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 3, "Marion", "", "Marion County", false, "Carling Publishers Weekly", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 4, "Marion", "", "Marion County", false, "Famouse Cookies Inc", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 5, "Marion", "", "Marion County", false, "Kimberly Cakes Inc", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 6, "Marion", "", "Marion County", false, "Lalamove Company", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 7, "Marion", "", "Marion County", false, "Romblon Company", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 8, "Marion", "", "Marion County", false, "Southern Importers Company", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 9, "Marion", "", "Marion County", false, "XYZ Company", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" },
                    { 10, "Marion", "", "Marion County", false, "Zeebra Company", "OH", "bolton.smith@company.com", "Bolton", "Smith", "+14844458715", "94101" }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "InvoiceDate", "PaymentDate", "PaymentTermsId", "PaymentTotal", "VendorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 0.0, 1 },
                    { 2, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 0.0, 1 },
                    { 3, new DateTime(2022, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1221.99, 1 },
                    { 4, new DateTime(2022, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 0.0, 1 },
                    { 5, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 2 },
                    { 6, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 2 },
                    { 7, new DateTime(2022, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1221.99, 2 },
                    { 8, new DateTime(2022, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 0.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "InvoiceLineItems",
                columns: new[] { "InvoiceLineItemId", "Amount", "Description", "InvoiceId" },
                values: new object[,]
                {
                    { 1, 1100.4200000000001, "Line Item 1", 1 },
                    { 2, 120.0, "Line Item 2", 1 },
                    { 3, 550.0, "Line Item 3", 1 },
                    { 4, 2323.21, "Line Item 1", 2 },
                    { 5, 199.0, "Line Item 2", 2 },
                    { 6, 659.0, "Line Item 3", 2 },
                    { 7, 123.22, "Line Item 5 for invoice 7", 5 },
                    { 8, 212.0, "Line Item 6 for invoice 6", 6 },
                    { 9, 323.11000000000001, "Line Item 7 for invoice 7", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceId",
                table: "InvoiceLineItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentTermsId",
                table: "Invoices",
                column: "PaymentTermsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VendorId",
                table: "Invoices",
                column: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLineItems");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
