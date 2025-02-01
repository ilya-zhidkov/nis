using System.Net.Http;
using System.Net.Http.Json;
using Nis.WpfApp.Extensions;
using Nis.Core.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Nis.WpfApp.Requests;

public abstract class BaseRequest
{
    private static HttpClient _http = null!;
    protected static readonly Dictionary<string, string?> Headers = new();
    protected readonly string Endpoint = Settings.Configuration["Api:Endpoint"]!;

    protected BaseRequest()
    {
        _http = new() { BaseAddress = new(Endpoint) };
        _http.DefaultRequestHeaders.Accept.Clear();
        _http.DefaultRequestHeaders.Accept.Add(new(Application.Json));
        _http.DefaultRequestHeaders.ToDictionary(header => header.Key, header => header.Value.First()).Merge(Headers!);
    }

    protected static async Task<TResult?> GetAsync<TResult>(string uri, IDictionary<string, string>? headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, uri);

        if (headers is not null)
            foreach (var (key, value) in headers)
                request.Headers.Add(key, value);

        var response = await _http.SendAsync(request);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<TResult>()
            : throw new(response.ReasonPhrase);
    }

    protected static async Task<TResult?> PostAsync<TResult>(string uri, HttpContent content, IDictionary<string, string>? headers = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };

        if (headers is not null)
            foreach (var (key, value) in headers)
                request.Headers.Add(key, value);

        var response = await _http.SendAsync(request);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<TResult>()
            : throw new(response.ReasonPhrase);
    }

    protected static void Authenticate(string? token)
    {
        if (token is not null && !_http.DefaultRequestHeaders.Contains("Authorization"))
            _http.DefaultRequestHeaders.Add("Authorization", token);
    }
}

