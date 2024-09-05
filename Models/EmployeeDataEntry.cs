namespace employee_information_csharp_app.Models;

public class EmployeeDataEntry
{
    public Guid Id { get; set; }

    required public string EmployeeName { get; set; }

    required public string StarTimeUtc { get; set; }

    required public string EndTimeUtc { get; set; }

    required public string EntryNotes { get; set; }

    required public string DeletedOn { get; set; }
}