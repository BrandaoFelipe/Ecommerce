using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
}

//As we apply the migrations a few tables will be created,
//if we want to add more tables, here is where we need to do so.