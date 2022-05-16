using System.Net.Http;

namespace Nis.WpfApp.Extensions;

public static class HttpClientExtensions
{
    public static async Task<HttpResponseMessage> GetWithHeadersAsync(
        this HttpClient http,
        string uri,
        IDictionary<string, string> headers
    )
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);

        foreach (var (key, value) in headers)
            request.Headers.Add(key, value);

        return await http.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PostWithHeadersAsync(
        this HttpClient http,
        string uri,
        HttpContent content,
        IDictionary<string, string> headers
    )
    {
        var request = new HttpRequestMessage
        {
            Content = content,
            Method = HttpMethod.Post,
            RequestUri = new Uri(uri)
        };

        foreach (var (key, value) in headers)
            request.Headers.Add(key, value);

        return await http.SendAsync(request);
    }
}
