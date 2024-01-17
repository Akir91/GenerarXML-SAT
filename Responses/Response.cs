using System.Net;

namespace SP_Entities.Responses
{
    public class Response
    {
        public bool Success { get; set; } = false;
        public bool Critical { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
        public HttpStatusCode Code { get; set; } = HttpStatusCode.BadRequest;
    }
}
