namespace AspReactTestApp.ResponseModels
{
    public class EmailVerificationResult
    {
        public EmailVerificationResult()
        {
            
        }

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new();
    }
}
