using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json; // یا Newtonsoft.Json برای سریالایز کردن

public class SmsService
{
    private static readonly HttpClient client = new HttpClient();

    public class Recipient
    {
        public string Destination { get; set; }
        public string UserTraceId { get; set; }
    }

    public class SendBulkRequest
    {
        public string ApiKey { get; set; }
        public string Text { get; set; }
        public long Sender { get; set; }
        public Recipient[] Recipients { get; set; }
    }

    public static async Task<string> SendBulkAsync(string destination, string userTraceId, string text)
    {
        string ApiKey = "e883424d-d70f-4e58-8ee3-4e21ea390ff1";
        long Sender = 30007546464646;

        var recipient = new Recipient
        {
            Destination = destination,
            UserTraceId = userTraceId
        };

        var requestData = new SendBulkRequest
        {
            ApiKey = ApiKey,
            Text = text,
            Sender = Sender,
            Recipients = new Recipient[] { recipient }
        };

        string url = "http://api.sms-webservice.com/api/V3/SendBulk";

        // سریالایز کردن به JSON
        string json = JsonSerializer.Serialize(requestData);

        try
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

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
