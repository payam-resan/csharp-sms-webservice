using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class TokenListRequest
    {
        public string ApiKey { get; set; }
    }

    public static async Task<string> TokenListAsync()
    {
        var apiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        var url = "http://api.sms-webservice.com/api/V3/TokenList";

        var requestData = new TokenListRequest
        {
            ApiKey = apiKey
        };

        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode(); // اگر موفق نبود استثنا می‌اندازد
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            // در صورت بروز خطا، پیام خطا را برمی‌گرداند
            return $"Error: {e.Message}";
        }
    }
}
