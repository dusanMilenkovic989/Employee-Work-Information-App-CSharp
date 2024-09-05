using employee_information_csharp_app.Constants;
using employee_information_csharp_app.Models;
using employee_information_csharp_app.Services.Shared;

namespace employee_information_csharp_app.Services;

public class EmployeeInformationFormatService
(
    IConfiguration configuration,
    IHttpService httpService
) : IEmployeeInformationFormatService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IHttpService _httpService = httpService;

    public async Task<List<EmployeeDataFormatted>> FetchAndFormatEntriesAsync()
    {
        var employeeDataEntries = await _httpService.FetchDataAsync<EmployeeDataEntry>(_configuration[AppSettings.EMPLOYEE_INFORMATION_CONNECTION_STRING] ?? "");

        var employeeDataFlattened = employeeDataEntries
            .Where(e => string.IsNullOrEmpty(e.DeletedOn))
            .GroupBy(e => e.EmployeeName)
            .Select(g => new EmployeeDataFormatted
            {
                Name = g.Key ?? AppSettings.EMPLOYEE_NOT_SPECIFIED,
                TotalTimeWorked = g.Sum(e =>
                {
                    DateTime startDate = DateTime.Parse(e.StarTimeUtc);
                    DateTime endDate = DateTime.Parse(e.EndTimeUtc);

                    return (endDate - startDate).TotalHours;
                })
            })
            .Select(e => new EmployeeDataFormatted
            {
                Name = e.Name,
                TotalTimeWorked = (uint)Math.Ceiling(e.TotalTimeWorked)
            })
            .OrderByDescending(e => e.TotalTimeWorked)
            .ToList();

        return employeeDataFlattened;
    }
}