using Microsoft.EntityFrameworkCore;
using VendorInvoicesApp.Entities;
using VendorInvoicesApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connStr = builder.Configuration.GetConnectionString("VendorDB");
builder.Services.AddDbContext<VendorDbContext>(options=>options.UseSqlServer(connStr));

builder.Services.AddScoped<IVendorService, VendorService>();

builder.Services.AddScoped<IInvoiceService, InvoiceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{Action=Index}/{id?}");

app.Run();
