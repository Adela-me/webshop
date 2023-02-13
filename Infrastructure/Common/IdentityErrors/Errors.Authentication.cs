using Domain.Common;

namespace Infrastructure.Common.IdentityErrors;
public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => new(
            code: "Auth.InvalidCred",
            message: "Invalid credentials.");

        public static Error NotAuthenticated => new(
            code: "Auth.NotAuthenticated",
            message: "User is not logged in.");
    }
}
