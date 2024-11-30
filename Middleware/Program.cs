using Middleware.Middlewares;
using Middleware.Services;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDependencyServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//(Lambda Middleware) = Inline Middleware
//app.Run(async (context) => 
//{
//    await context.Response.WriteAsync(
//       $"CurrentCulture.DisplayName 1: {CultureInfo.CurrentCulture.DisplayName}");
//});


//app.Run(async (context) => //Terminal middleware
//{
//    await context.Response.WriteAsync(
//        $" CurrentCulture.DisplayName 2: {CultureInfo.CurrentCulture.DisplayName}");
//});


//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync(
//       $"CurrentCulture.DisplayName 3: {CultureInfo.CurrentCulture.DisplayName}");

//    await next(context);
//});
app.UseMiddleware<ExampleMiddlewareOne>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run(async (context) => //Terminal middleware
{
    await context.Response.WriteAsync(
        $" CurrentCulture.DisplayName 3: {CultureInfo.CurrentCulture.DisplayName}");
});

app.Run();
