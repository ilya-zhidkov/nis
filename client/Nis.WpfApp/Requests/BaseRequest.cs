using System.Net.Http;
using System.Net.Http.Json;
using Nis.WpfApp.Extensions;
using System.Net.Http.Headers;

namespace Nis.WpfApp.Requests;

public abstract class BaseRequest
{
    private readonly HttpClient _http;
    protected static readonly Dictionary<string, string> Headers = new();
    protected readonly string Endpoint = App.Configuration["Api:Endpoint"];

    protected BaseRequest()
    {
        _http = new HttpClient { BaseAddress = new Uri(Endpoint) };
        _http.DefaultRequestHeaders.Accept.Clear();
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _http.DefaultRequestHeaders.ToDictionary(header => header.Key, header => header.Value.First()).Merge(Headers);
    }

    protected async Task<TResult> GetAsync<TResult>(string uri, IDictionary<string, string> headers = null)
    {
        using var response = headers is null ? await _http.GetAsync(uri) : await _http.GetWithHeadersAsync(uri, headers);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<TResult>()
            : throw new Exception(response.ReasonPhrase);
    }

    protected async Task<TResult> PostAsync<TResult>(string uri, HttpContent content, IDictionary<string, string> headers = null)
    {
        using var response = headers is null
            ? await _http.PostAsync(uri, content)
            : await _http.PostWithHeadersAsync(uri, content, headers);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<TResult>()
            : throw new Exception(response.ReasonPhrase);
    }

    protected void Authenticate(string token)
    {
        if (!_http.DefaultRequestHeaders.Contains("Authorization"))
            _http.DefaultRequestHeaders.Add("Authorization", token);
    }
}

