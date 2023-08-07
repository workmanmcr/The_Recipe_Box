using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace RecipeBox.Controllers
{
public class RecipesController : Controller
{
private readonly RecipeBoxContext _db;
public RecipesController(RecipeBoxContext db)
{
  _db = db;
}
public ActionResult Index()
{
    List<Recipe> model = _db.Recipes.ToList();
  return View(model);
}

public ActionResult Create()
{
    ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
    return View();
}

[HttpPost]
public ActionResult Create(Recipe recipe)
{
    _db.Recipes.Add(recipe);
    _db.SaveChanges();
    return RedirectToAction("Index");
}

public ActionResult Details(int id)
{
    Recipe thisRecipe = _db.Recipes.Include(recipe => recipe.JoinEntities).FirstOrDefault(recipe => recipe.RecipeId == id);
    return View(thisRecipe);
}

public ActionResult Edit(int id)
{
    Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName");
    return View(thisRecipe);
}

[HttpPost]
public ActionResult Edit(Recipe recipe)
{
    _db.Recipes.Update(recipe);
    _db.SaveChanges();
    return RedirectToAction("Index");
}

public ActionResult Delete(int id)
{
    Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    return View(thisRecipe);
}

[HttpPost, ActionName("Delete")]
public ActionResult DeleteConfirmed (int id)
{
   Recipe thisRecipe = _db.Recipes
                        .FirstOrDefault(recipe => recipe.RecipeId == id);
   _db.Recipes.Remove(thisRecipe);
   _db.SaveChanges();
   return RedirectToAction("Index"); 
}

public ActionResult AddIngredient (int id)
{
    Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe=> recipe.RecipeId == id);
    ViewBag.Ingredients = new SelectList(_db.Ingredients, "IngredientId", "Recipe");
    return View(thisRecipe);
}
public ActionResult AddRecipe(Recipe recipe, int ingredientId)
{
    #nullable enable
    RecipeIngredient? joinEntity = _db.RecipeIngredients.FirstOrDefault(join => (join.RecipeId == recipe.RecipeId && join.IngredientId == ingredientId));
    #nullable disable

    if (joinEntity == null && recipe.RecipeId != 0)
    {
        _db.RecipeIngredients.Add(new RecipeIngredient() { RecipeId = recipe.RecipeId, IngredientId = ingredientId });
        _db.SaveChanges();
    }
    return RedirectToAction("Details", new { id = recipe.RecipeId });
}
}

}