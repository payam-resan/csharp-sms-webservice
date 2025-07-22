using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class Recipient
    {
        public long Sender { get; set; }
        public string Text { get; set; }
        public string Destination { get; set; }
        public string UserTraceId { get; set; }
    }

    public class SendMultipleRequest
    {
        public string ApiKey { get; set; }
        public Recipient[] Recipients { get; set; }
    }

    public static async Task<string> SendMultipleAsync(string destination, string userTraceId, string text)
    {
        string ApiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        long Sender = 30007546464646;

        var recipient = new Recipient
        {
            Sender = Sender,
            Text = text,
            Destination = destination,
            UserTraceId = userTraceId
        };

        var requestData = new SendMultipleRequest
        {
            ApiKey = ApiKey,
            Recipients = new Recipient[] { recipient }
        };

        string url = "http://api.sms-webservice.com/api/V3/SendMultiple";

        string json = JsonSerializer.Serialize(requestData);

        try
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
