using Microsoft.AspNetCore.Identity;

namespace RecipeBox.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<RecipeUser> JoinUser { get; }
    }
}
