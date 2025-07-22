using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class StatusByIdRequest
    {
        public string ApiKey { get; set; }
        public string[] Ids { get; set; }
    }

    public static async Task<string> StatusByIdAsync(string[] ids)
    {
        var apiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        var url = "http://api.sms-webservice.com/api/V3/StatusById";

        var requestData = new StatusByIdRequest
        {
            ApiKey = apiKey,
            Ids = ids
        };

        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode(); // اگر درخواست موفق نبود استثنا می‌اندازد
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
