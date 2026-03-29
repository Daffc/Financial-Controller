public class ApiErrorResponse
{
    public required string Message { get; set; }
    public required List<string> Errors { get; set; }
    public required string TraceId { get; set; }
}