using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class AccountInfoRequest
    {
        public string ApiKey { get; set; }
    }

    public static async Task<string> AccountInfoAsync()
    {
        var apiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        var url = "http://api.sms-webservice.com/api/V3/AccountInfo";

        var requestData = new AccountInfoRequest
        {
            ApiKey = apiKey
        };

        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
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
