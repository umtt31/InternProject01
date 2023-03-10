using AspNetCoreMvc004.Filters;
using AspNetCoreMvc004.Helpers;
using AspNetCoreMvc004.Middlewares;
using AspNetCoreMvc004.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

/* builder.Services.AddSingleton<IHelper, Helper>(); */
/* builder.Services.AddScoped<IHelper, Helper>(); */
/* builder.Services.AddScoped<Helper> */
builder.Services.AddTransient<IHelper, Helper>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<NotFoundFilter>();

builder.Services.AddSingleton<IFileProvider>( new PhysicalFileProvider(Directory.GetCurrentDirectory()) );

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

app.UseMiddleware<WhiteIpAddressControlMiddleware>();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "blog",
//    pattern: "blog/{*article}",
//    defaults: new { controller = "Blog", Action = "Article"} );

//app.MapControllerRoute(
//    name: "article",
//    pattern: "{controller=Blog}/{action=Article}/{name}/{id}");


//app.MapControllerRoute(
//    name: "pages",
//    pattern: "{controller}/{action}/{page}/{pageSize}");

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
