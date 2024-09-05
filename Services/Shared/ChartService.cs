using PuppeteerSharp;

namespace employee_information_csharp_app.Services.Shared;

public class ChartService : IChartService
{
    public async Task GenerateChartPngAsync(string pngPath, string chartLabels, string chartData)
    {
        await new BrowserFetcher().DownloadAsync();
        var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        var page = await browser.NewPageAsync();

        var chartHtml = GenerateChartHtml(chartLabels, chartData);

        await page.SetContentAsync(chartHtml);
        await page.ScreenshotAsync(pngPath, new ScreenshotOptions { FullPage = true });
    }

    private static string GenerateChartHtml(string labels, string data)
    {
        return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <script type=""text/javascript"" src=""https://www.gstatic.com/charts/loader.js""></script>
                <script type=""text/javascript"">
                    google.charts.load('current', {{ packages: ['corechart'] }});
                    google.charts.setOnLoadCallback(drawChart);

                    function drawChart() {{
                        const DATA = google.visualization.arrayToDataTable([
                            ['Label', 'Value'],
                            {GenerateGoogleChartData(labels, data)}
                        ]);

                        const OPTIONS = {{
                            title: '',
                            chartArea: {{ width: '90%', height: '80%' }},
                            legend: {{ position: 'right', textStyle: {{ fontSize: 12 }} }},
                            pieSliceTextStyle: {{ fontSize: 10 }}
                        }};

                        const CHART = new google.visualization.PieChart(document.querySelector('#chart'))
                        CHART.draw(DATA, OPTIONS);
                    }}
                </script>
                <style>
                    body {{
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        margin: 0;
                    }}
                    #chart-container {{
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        width: 100%;
                        height: 100%;
                    }}
                </style>
            </head>
            <body>
                <div id=""chart-container"">
                    <div id=""chart"" style=""width: 400px; height: 400px;""></div>
                </div>
            </body>
            </html>";
    }
    private static string GenerateGoogleChartData(string labels, string data)
    {
        var labelArray = labels.Trim('[', ']').Split(',');
        var dataArray = data.Trim('[', ']').Split(',');

        var chartData = new List<string>();
        for (int i = 0; i < labelArray.Length; i++)
        {
            chartData.Add($"['{labelArray[i].Trim('\"')}', {dataArray[i].Trim()}]");
        }

        return string.Join(",", chartData);
    }
}