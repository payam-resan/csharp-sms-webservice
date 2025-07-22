using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> SendTokenSingleAsync(
        string templateKey,
        string destination,
        string param1,
        string param2,
        string param3)
    {
        string apiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        long sender = 30007546464646; // اگر نیاز به استفاده داشتید

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["ApiKey"] = apiKey;
        query["TemplateKey"] = templateKey;
        query["Destination"] = destination;
        query["p1"] = param1;
        query["p2"] = param2;
        query["p3"] = param3;

        string baseUrl = "http://api.sms-webservice.com/api/V3/SendTokenSingle";
        string url = $"{baseUrl}?{query}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // اگر وضعیت غیر موفق بود استثنا پرتاب می‌کند
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            return $"Error: {e.Message}";
        }
    }
}
