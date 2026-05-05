
namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }

        public static UnauthorizedException For<T>(int id)
            => new($"{typeof(T).Name} with id {id} is Unauthorized");

        public static UnauthorizedException ForInvalidCredentials()
            => new("You are not authorized to perform this action");
    }
}
