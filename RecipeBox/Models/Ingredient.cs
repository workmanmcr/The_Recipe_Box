namespace RecipeBox.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int Measurement { get; set; }
        public int RecipeId { get; set; }
        public Recipe recipe { get; set; }
    }
}