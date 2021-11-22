using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Aiirh.Basic.Entities
{
    public interface IApplicationUser
    {
        ICollection<IdentityUserRole<string>> Roles { get; set; }
        ICollection<IdentityUserClaim<string>> Claims { get; set; }
    }

    public interface IApplicationRole
    {
        ICollection<IdentityUserRole<string>> Users { get; set; }
        ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }
}
