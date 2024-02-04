namespace AspReactTestApp.DTO;

public class ResponseDTO
{
    public bool IsSuccessfull { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string> Errors { get; set; } = [];
}
