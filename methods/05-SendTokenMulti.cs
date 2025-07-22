using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class Recipient
    {
        public string Destination { get; set; }
        public string UserTraceId { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }

    public class RequestData
    {
        public string ApiKey { get; set; }
        public string TemplateKey { get; set; }
        public List<Recipient> Recipients { get; set; }
    }

    public static async Task<string> SendTokenMultiAsync(
        string templateKey,
        string destination,
        string userTraceId,
        Dictionary<string, string> parameters)
    {
        var apiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        var url = "http://api.sms-webservice.com/api/V3/SendTokenMulti";

        var recipient = new Recipient
        {
            Destination = destination,
            UserTraceId = userTraceId,
            Parameters = parameters
        };

        var requestData = new RequestData
        {
            ApiKey = apiKey,
            TemplateKey = templateKey,
            Recipients = new List<Recipient> { recipient }
        };

        var json = JsonSerializer.Serialize(requestData);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();  // اگر وضعیت موفق نبود، استثنا پرتاب می‌کند
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            return $"Error: {e.Message}";
        }
    }
}
