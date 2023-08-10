using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Instruction {get; set; }
    public int Rating { get; set; }
    public List<RecipeIngredient> JoinEntities { get; } 
    public List<RecipeTag> JoinTags { get; }
    public List<RecipeUser> JoinUser { get; }
    public ApplicationUser User { get; set; } 
  }
}