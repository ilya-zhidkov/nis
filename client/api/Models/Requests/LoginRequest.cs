using System.ComponentModel.DataAnnotations;

namespace Nis.Api.Models.Requests;

public sealed class LoginRequest
{
    public required string Username { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }

    public void Deconstruct(out string username, out string password)
    {
        username = Username;
        password = Password;
    }
}
