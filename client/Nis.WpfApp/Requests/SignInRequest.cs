using System.Text;
using System.Net.Http;
using System.Text.Json;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.Requests;

public class SignInRequest : BaseRequest
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public Student Student { get; set; }
    }

    public async Task<AuthenticationResponse> SignInAsync(string username, string password)
    {
        var token = await GetTokenAsync(username, password);

        if (string.IsNullOrWhiteSpace(token))
            return null;

        var response = await GetAsync<IEnumerable<IDictionary<string, object>>>(
            uri: $"{Endpoint}/students/{username}",
            headers: new Dictionary<string, string> { { "token", token } }
        );

        var students = response.Select(student => new Student
        {
            FirstName = student["firstname"].ToString(),
            LastName = student["lastname"].ToString(),
            ProfileImage = student["profileimageurl"].ToString()
        });

        return await Task.FromResult(new AuthenticationResponse
        {
            Token = token,
            Student = students.Single()
        });
    }

    private async Task<string> GetTokenAsync(string username, string password)
    {
        var response = await PostAsync<Dictionary<string, string>>(uri: $"{Endpoint}/auth/login", new StringContent(
            JsonSerializer.Serialize(new { username, password }),
            Encoding.UTF8,
            "application/json"
        ));

        return response["token"];
    }
}

