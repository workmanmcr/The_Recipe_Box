namespace RecipeBox.Models
{
  public class RecipeBox
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Instruction {get; set; }
    public List<Ingredient> Ingredients { get; set; } 
  }
}