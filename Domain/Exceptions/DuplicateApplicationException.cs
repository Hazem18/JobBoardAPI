
namespace Domain.Exceptions
{
    public class DuplicateApplicationException : Exception
    {
        public DuplicateApplicationException(string message) : base(message) { }

        public static DuplicateApplicationException For<T>(int id)
            => new($"You have already applied for this job");
    }
}
