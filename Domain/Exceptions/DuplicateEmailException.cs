

namespace Domain.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string message) : base(message) { }

        public static DuplicateEmailException For<T>(string email)
            => new($"{typeof(T).Name} with email {email} already exists ");
    }
}
