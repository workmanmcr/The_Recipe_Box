using System.Collections.Generic;
namespace RecipeBox.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public List<RecipeIngredient> JoinEntities { get; set; }
    }
}