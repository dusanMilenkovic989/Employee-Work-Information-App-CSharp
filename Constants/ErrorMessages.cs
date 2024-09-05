namespace employee_information_csharp_app.Constants;

// Purpose - Developer loggings, hence messages will be descriptive
public record ErrorMessages
{
    public static string ErrorDuringHttpRequest(string? responseCode = "No code") => $"An error occured during the http request! Either the request did not reach the server, or response from the server was not inside success range. Response code: {responseCode}";
    public static string ErrorDuringDataParsing() => $"An error occured during data parsing!";
}