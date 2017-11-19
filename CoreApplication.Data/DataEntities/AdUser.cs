using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApplication.Data.DataEntities
{
    public class AdUser :IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
