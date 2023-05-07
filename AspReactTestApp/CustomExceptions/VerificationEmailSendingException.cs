namespace AspReactTestApp.CustomExceptions
{
    public class VerificationEmailSendingException : Exception
    {
        public VerificationEmailSendingException()
        {
            
        }

        public VerificationEmailSendingException(string message) : base(message)
        {
            
        }
    }
}
