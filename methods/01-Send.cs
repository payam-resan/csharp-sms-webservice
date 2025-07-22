using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web; // برای HttpUtility.UrlEncode

class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> SendAsync(string recipients, string text)
    {
        string ApiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        string sender = "30007546464646";

        var query = HttpUtility.ParseQueryString(string.Empty);
        query["ApiKey"] = ApiKey;
        query["Text"] = HttpUtility.UrlEncode(text);
        query["Sender"] = sender;
        query["Recipients"] = recipients;

        string url = "http://api.sms-webservice.com/api/V3/Send?" + query.ToString();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            return $"Error: {e.Message}";
        }
    }
}
