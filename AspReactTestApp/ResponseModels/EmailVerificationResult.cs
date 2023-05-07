namespace AspReactTestApp.ResponseModels
{
    public class EmailVerificationResult
    {
        public EmailVerificationResult()
        {
            
        }

        public EmailVerificationResult(bool ısSuccessful, string message)
        {
            IsSuccessful = ısSuccessful;
            Message = message;
        }

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }


    }
}
