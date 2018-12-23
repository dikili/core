using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CoreApplication.Data.DataEntities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}