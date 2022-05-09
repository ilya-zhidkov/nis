namespace Nis.Api.Options;

public record MoodleOptions
{
    public string Url { get; set; }
    public byte CourseId { get; set; }
    public string Service { get; set; }
    public string WsFunction { get; set; }
    public string MoodleWsRestFormat { get; set; }

    public void Deconstruct(out string url, out byte courseId, out string service, out string wsFunction, out string format)
    {
        url = Url;
        service = Service;
        courseId = CourseId;
        wsFunction = WsFunction;
        format = MoodleWsRestFormat;
    }
}

