using BLL;
using DAL;
using Microsoft.EntityFrameworkCore;
using webclasslib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// 1. Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("WWDB");

// 2. Call the Backend Extension Method to register the Context and DbVersionServices classes
builder.Services.AddBackendDependencies(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
