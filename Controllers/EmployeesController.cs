using Microsoft.AspNetCore.Mvc;
using employee_information_csharp_app.Services;

namespace employee_information_csharp_app.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController(IEmployeeInformationFormatService employeeInformationFormatService) : ControllerBase
{
    private readonly IEmployeeInformationFormatService _employeeInformationFormatService = employeeInformationFormatService;

    [HttpGet]
    [Route("all", Name = "GetEmployeeDataAsync")]
    public async Task<IActionResult> GetEmployeeDataAsync()
    {
        var employeeData = await _employeeInformationFormatService.FetchAndFormatEntriesAsync();

        return Ok(employeeData);
    }
}