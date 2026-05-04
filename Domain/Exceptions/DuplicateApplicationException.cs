
namespace Domain.Exceptions
{
    public class DuplicateApplicationException : Exception
    {
        public DuplicateApplicationException(string message) : base(message) { }

        public static DuplicateApplicationException For<T>(int id)
            => new($"{typeof(T).Name} with id {id} already exists ");
    }
}
