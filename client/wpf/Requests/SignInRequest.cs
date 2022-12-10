using System.Text;
using Caliburn.Micro;
using System.Net.Http;
using System.Text.Json;
using Nis.WpfApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Nis.WpfApp.Requests;

public class SignInRequest : BaseRequest
{
    private readonly SimpleContainer _container;

    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public Student Student { get; set; }
    }

    public SignInRequest(SimpleContainer container) => _container = container;

    public async Task<AuthenticationResponse> SignInAsync(string username, string password)
    {
        var token = await GetTokenAsync(username, password);

        if (string.IsNullOrWhiteSpace(token))
            return null;

        var response = await GetAsync<IEnumerable<IDictionary<string, object>>>(
            uri: $"{Endpoint}/students/{username}",
            headers: new Dictionary<string, string> { { "token", Headers["Authorization"] } }
        );

        var student = response.Select(student => new Student
        {
            FirstName = student["firstname"].ToString(),
            LastName = student["lastname"].ToString(),
            ProfileImage = GetProfileImage(student["profileimageurl"].ToString())
        }).Single();

        _container.Instance(student);

        return await Task.FromResult(new AuthenticationResponse
        {
            Token = token,
            Student = student
        });
    }

    private async Task<string> GetTokenAsync(string username, string password)
    {
        try
        {
            var response = await PostAsync<Dictionary<string, string>>(uri: $"{Endpoint}/auth/login", new StringContent(
                JsonSerializer.Serialize(new { username, password }),
                Encoding.UTF8,
                Application.Json
            ));

            var token = response["token"];

            Headers["Authorization"] = token;
            Authenticate(Headers["Authorization"]);

            return token;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string GetProfileImage(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return string.Empty;

        var uri = new Uri(url);

        return $"{uri.Scheme}://{uri.Host}/webservice/{uri.Segments[1]}{uri.Segments[2]}{uri.Segments[3]}{uri.Segments[4]}{uri.Segments[6]}?token={Headers["Authorization"]}";
    }
}
