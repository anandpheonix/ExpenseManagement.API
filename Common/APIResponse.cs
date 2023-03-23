using System.Net;

namespace Common;

public class APIResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; }
    public object Data { get; set; }
}
