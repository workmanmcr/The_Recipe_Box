namespace RecipeBox.Models
{
    public class RecipeUser
    {
        public int RecipeUserId { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public ApplicationUser User { get; set; }
    }
}