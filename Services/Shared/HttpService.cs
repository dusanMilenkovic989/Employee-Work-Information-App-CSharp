using System.Text.Json;
using employee_information_csharp_app.Constants;

namespace employee_information_csharp_app.Services.Shared;

public class HttpService(HttpClient httpClient) : IHttpService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<List<T>> FetchDataAsync<T>(string endpoint)
    {
        HttpResponseMessage? response = null;

        try
        {
            response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            Console.WriteLine(ErrorMessages.ErrorDuringHttpRequest(response?.StatusCode.ToString()));

            return [];  // Or throw an error, depending on the application needs and specifications
        }

        try
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            List<T>? data = JsonSerializer.Deserialize<List<T>>(responseBody);

            return data ?? [];
        }
        catch (JsonException)
        {
            Console.WriteLine(ErrorMessages.ErrorDuringDataParsing());

            return [];  // Or throw an error, depending on the application needs and specifications
        }
    }
}