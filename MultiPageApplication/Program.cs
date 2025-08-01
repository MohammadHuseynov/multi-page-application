using Microsoft.EntityFrameworkCore;
using MultiPageApplication.ApplicationServices.Services;
using MultiPageApplication.ApplicationServices.Services.Contracts;
using MultiPageApplication.Models;
using MultiPageApplication.Models.Services.Contracts;
using MultiPageApplication.Models.Services.Repositories;

#region [- Building the app object -]
var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MultiPageApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Default")
));


//AddScope goes here

// This tells ASP.NET: "When a class asks for IProductRepository, create a ProductRepository."
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// This tells ASP.NET: "When a class asks for IProductApplicationService, create a ProductApplicationService."
builder.Services.AddScoped<IProductApplicationService, ProductApplicationService>();


var app = builder.Build();

#endregion



#region [- adding middlewares in the road of running -]

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

#endregion

app.Run();
