using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using employee_information_csharp_app.Services.Shared;
using employee_information_csharp_app.Models;
using employee_information_csharp_app.Models.Views;
using employee_information_csharp_app.Constants;

namespace employee_information_csharp_app.Controllers;

public class DashboardController(IChartService chartService) : Controller
{
    private readonly IChartService _chartService = chartService;

    public IActionResult Index()
    {
        ViewBag.EmployeeInfoUrl = AppRoutes.EMPLOYEE_INFORMATION;

        return View();
    }

    public IActionResult EmployeeInformation()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GenerateTableAndChart([FromBody] List<EmployeeDataFormatted> employeeData)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "employeeChart.png");

        var employeeNames = employeeData.Select(e => $"\"{e.Name}\"");
        var employeeHours = employeeData.Select(e => e.TotalTimeWorked);

        var labelsJson = $"[{string.Join(',', employeeNames)}]";
        var dataJson = $"[{string.Join(',', employeeHours)}]";

        _ = _chartService.GenerateChartPngAsync(filePath, labelsJson, dataJson);

        return PartialView("EmployeeTable", employeeData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
