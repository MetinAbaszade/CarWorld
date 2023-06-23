namespace AspReactTestApp.DTOs
{
    public class ResponseDto
    {
        public ResponseDto() { } 

        public bool IsSuccessfull { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string> Errors { get; set; } = new();
    }
}
