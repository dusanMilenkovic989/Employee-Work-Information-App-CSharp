namespace employee_information_csharp_app.Services.Shared;

public interface IHttpService
{
    Task<List<T>> FetchDataAsync<T>(string endpoint);
}

public interface IChartService
{
    Task GenerateChartPngAsync(string pngPath, string chartLabels, string chartData);
}