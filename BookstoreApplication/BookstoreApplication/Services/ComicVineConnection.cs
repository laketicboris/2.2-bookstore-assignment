using BookstoreApplication.Exceptions;
using System.Net;
using System.Text.Json;

namespace BookstoreApplication.Services
{
    public class ComicVineConnection : IComicVineConnection
    {
        private readonly HttpClient _client;
        private readonly ILogger<ComicVineConnection> _logger;

        public ComicVineConnection(HttpClient client, ILogger<ComicVineConnection> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<string> Get(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("BookstoreApp");

            HttpResponseMessage response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDocument = JsonDocument.Parse(json);

            if (!response.IsSuccessStatusCode)
                HandleUnsuccessfulRequest(response, jsonDocument);

            int statusCode = jsonDocument.RootElement.GetProperty("status_code").GetInt32();
            if (statusCode != 1)
                HandleUnsuccessfulRequest(response, jsonDocument);

            return json;
        }

        private void HandleUnsuccessfulRequest(HttpResponseMessage response, JsonDocument jsonDocument)
        {
            var errorMessage = "";
            try
            {
                errorMessage = jsonDocument.RootElement.GetProperty("error").GetString();
                _logger.LogError($"Request to API failed: {(int)response.StatusCode} - {response.ReasonPhrase}: {errorMessage}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured with message: {ex.Message}");
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new RateLimitException();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiAccessException();
            }
            else
            {
                string apiError = string.IsNullOrEmpty(errorMessage) ?
                  "Error occured when sending request to the external API" : errorMessage;
                throw new ApiCommunicationException(apiError);
            }
        }
    }
}