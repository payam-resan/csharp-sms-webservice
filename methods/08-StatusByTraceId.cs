using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class StatusByUserTraceIdRequest
    {
        public string ApiKey { get; set; }
        public string[] UserTraceIds { get; set; }
    }

    public static async Task<string> StatusByUserTraceIdAsync(string[] userTraceIds)
    {
        var apiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        var url = "http://api.sms-webservice.com/api/V3/StatusByUserTraceId";

        var requestData = new StatusByUserTraceIdRequest
        {
            ApiKey = apiKey,
            UserTraceIds = userTraceIds
        };

        var json = JsonSerializer.Serialize(requestData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            return $"Error: {e.Message}";
        }
    }
}
