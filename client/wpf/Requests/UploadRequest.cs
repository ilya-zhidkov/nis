using System.Text;
using System.Net.Http;
using System.Text.Json;
using Nis.WpfApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Nis.WpfApp.Requests;

[UsedImplicitly]
public class UploadRequest : BaseRequest
{
    public async Task UploadAsync(Form form)
    {
        await PostAsync<IDictionary<string, object>>(
            uri: $"{Endpoint}/exams",
            content: new StringContent(
                JsonSerializer.Serialize(form),
                Encoding.UTF8,
                Application.Json
            ),
            headers: new Dictionary<string, string?> { { "token", Headers["Authorization"] } }!
        );
    }
}
