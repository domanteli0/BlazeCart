using System.Net.Http;

namespace Scraper {
    public class HttpSender<T> {
        private HttpClient _httpClient;
        private Func<HttpResponseMessage, T> _responseHandler;
        public HttpSender(HttpClient httpClient, Func<HttpResponseMessage, T> responseHandler) {
            _httpClient = httpClient;
            _responseHandler = responseHandler;
        }

        public T Send(HttpRequestMessage req) {
            var response = _httpClient.Send(req);
            // _logger.LogInformation($"Sent request to {req.RequestUri}");

            return _responseHandler(response);
        }

        public async Task<T> SendAsync(HttpRequestMessage req) {
            var response = await _httpClient.SendAsync(req);
            // _logger.LogInformation($"Sent request to {req.RequestUri}");

            return _responseHandler(response);
        }

    }
}