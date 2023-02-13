using Domain.Common;

namespace Infrastructure.Common.IdentityErrors;
public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => new(
            code: "User.DuplicateEmail",
            message: "Email is already in use.");

        public static Error NotFound => new(
           code: "User.NotFound",
           message: "User with speciefied identifier was not found.");

    }
}
