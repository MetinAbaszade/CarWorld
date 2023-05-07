namespace AspReactTestApp.CustomExceptions
{
    public class VerificationCodeAttemptsLimitException : Exception
    {
        public VerificationCodeAttemptsLimitException()
        {
            
        }

        public VerificationCodeAttemptsLimitException(string message) : base(message)
        {
            
        }
    }
}
