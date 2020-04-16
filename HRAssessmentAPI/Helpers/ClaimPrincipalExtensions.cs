using System;
using System.Security.Claims;

namespace HRAssessmentAPI.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get claimed user id from the principles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="principal">Pass the claim principal</param>
        /// <returns>Will return logged in user id</returns>
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }

        /// <summary>
        /// Get claimed user name from the principles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="principal">Pass the claim principal</param>
        /// <returns>Will return logged in user name</returns>
        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Get claimed user email from the principles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="principal">Pass the claim principal</param>
        /// <returns>Will return logged in user email</returns>
        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
