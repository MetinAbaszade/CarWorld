

namespace AspReactTestApp.CustomExceptions
{
    public class VerificationCodeExpiredException : Exception
    {
        public VerificationCodeExpiredException()
        {

        }

        public VerificationCodeExpiredException(string message) : base(message)
        {

        }
    }
}
