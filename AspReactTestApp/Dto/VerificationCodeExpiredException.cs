using Org.BouncyCastle.Bcpg.OpenPgp;

namespace AspReactTestApp.DTOs
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
