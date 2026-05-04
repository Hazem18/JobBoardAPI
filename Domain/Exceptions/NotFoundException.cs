
namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public static NotFoundException For<T>(string id)
            => new($"{typeof(T).Name} with id {id} was not found");

        public static NotFoundException ForUser<T>(string email)
            => new($"{typeof(T).Name} with email {email} was not found");
    }
}
