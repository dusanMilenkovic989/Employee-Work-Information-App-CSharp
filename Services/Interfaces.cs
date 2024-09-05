using employee_information_csharp_app.Models;

namespace employee_information_csharp_app.Services;

public interface IEmployeeInformationFormatService
{
    Task<List<EmployeeDataFormatted>> FetchAndFormatEntriesAsync();
}