using Microsoft.AspNetCore.Identity;

namespace CoreApplication.Data.DataEntities
{
    public class UserRole : IdentityUserRole<int>
    {
        public LoginUser User { get; set; }

        public Role Role { get; set; }

    }
}