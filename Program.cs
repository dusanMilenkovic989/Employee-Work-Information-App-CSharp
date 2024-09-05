using employee_information_csharp_app.Constants;
using employee_information_csharp_app.Services;
using employee_information_csharp_app.Services.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddHttpClient<IHttpService, HttpService>();
builder.Services.AddSingleton<IEmployeeInformationFormatService, EmployeeInformationFormatService>();
builder.Services.AddSingleton<IChartService, ChartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Dashboard/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect($"/{AppRoutes.DASHBOARD}");
    }
    else
    {
        await next();
    }
});

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}"
);

app.Run();
