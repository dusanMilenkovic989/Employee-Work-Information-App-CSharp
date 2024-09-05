using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using employee_information_csharp_app.Models;
using employee_information_csharp_app.Models.Views;
using employee_information_csharp_app.Constants;

namespace employee_information_csharp_app.Controllers;

public class DashboardController : Controller
{
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
    public IActionResult GenerateTable([FromBody] List<EmployeeDataFormatted> employeeData)
    {
        return PartialView("EmployeeTable", employeeData);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
