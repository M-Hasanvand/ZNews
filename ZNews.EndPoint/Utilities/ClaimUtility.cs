using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ZNews.EndPoint.Utilities
{
    public static class ClaimUtility
    {
        public static long UserId(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            var userid = long.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userid;
        }
        public static List<string> GetRoles(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            var roles = identity.Claims.Where(p => p.Type.EndsWith("role"));
            List<string> userRole = new List<string>();
            foreach (var item in roles)
            {
                userRole.Add(item.Value);
            }
            return userRole;

        }
    }
}
