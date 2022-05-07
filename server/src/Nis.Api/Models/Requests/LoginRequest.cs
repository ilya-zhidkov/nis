using System.ComponentModel.DataAnnotations;

namespace Nis.Api.Models.Requests;

public class LoginRequest
{
    public string UserName { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
}
