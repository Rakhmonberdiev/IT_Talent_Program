using System.Security.Claims;

namespace IT_Talent_Program.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetLogin(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
