using System.Net.Http;
using System.Net.Http.Json;
using Nis.WpfApp.Extensions;
using System.Net.Http.Headers;

namespace Nis.WpfApp.Requests;

public abstract class BaseRequest
{
    private readonly HttpClient _http;
    protected readonly string Endpoint = App.Configuration["Api:Endpoint"];

    protected BaseRequest()
    {
        _http = new HttpClient { BaseAddress = new Uri(Endpoint) };
        _http.DefaultRequestHeaders.Accept.Clear();
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected async Task<TResult> GetAsync<TResult>(string uri, IDictionary<string, string> headers = null)
    {
        using var response = headers is null ? await _http.GetAsync(uri) : await _http.GetWithHeadersAsync(uri, headers);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<TResult>()
            : throw new Exception(response.ReasonPhrase);
    }

    protected async Task<TResult> PostAsync<TResult>(string uri, HttpContent content)
    {
        using var response = await _http.PostAsync(uri, content);

        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<TResult>()
            : throw new Exception(response.ReasonPhrase);
    }
}

