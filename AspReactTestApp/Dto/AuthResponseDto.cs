namespace AspReactTestApp.DTOs
{
    public class AuthResponseDto
    {
        public AuthResponseDto() { }

        public AuthResponseDto(string message) => Message = message;

        public bool IsSuccessfull { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string> Errors { get; set; } = new();
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpires { get; set; }
    }
}
