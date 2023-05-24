using Microsoft.EntityFrameworkCore;
using Minishop.Application.Contracts;
using Minishop.Application.Services;
using Minishop.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler
      = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<MinishopDBContext>(
       //options => options.UseSqlServer("name=ConnectionStrings:minishop"));
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("minishop")));
//builder.Services.AddScoped<MinishopDBContext>(i => new MinishopDBContext());

builder.Services.AddScoped<ISizeTypeServices, SizeTypeServices>();
builder.Services.AddScoped<IProductCategoryServices, ProductCategoryServices>();
builder.Services.AddScoped<IProductTypeServices, ProductTypeServices>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
