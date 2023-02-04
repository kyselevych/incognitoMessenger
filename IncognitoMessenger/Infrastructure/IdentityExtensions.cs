using System.Security.Claims;
using System.Security.Principal;

namespace IncognitoMessenger.Infrastructure
{
    public static class IdentityExtensions
    {
        public static int? GetUserId(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var id = claimsIdentity?.Claims.First(x => x.Type == "id").Value;

            return id != null ? int.Parse(id) : null;
        }
    }
}
