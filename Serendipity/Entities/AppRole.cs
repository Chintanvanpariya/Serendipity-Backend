using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Serendipity.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}