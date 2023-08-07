using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  public class IngredientsController : Controller
  {
    private readonly RecipeBoxContext _db;

    public IngredientsController(RecipeBoxContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
        List<Ingredient> model = _db.Ingredients.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Ingredient ingredient)
    {
      _db.Ingredients.Add(ingredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.Include(ingredient => ingredient.JoinEntities).FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }

    public ActionResult Edit(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
      return View(thisIngredient);
    }

    [HttpPost]
    public ActionResult Edit(Ingredient ingredient)
    {
      _db.Ingredients.Update(ingredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      return View(thisIngredient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Ingredient thisIngredient = _db.Ingredients
                           .FirstOrDefault(ingredient => ingredient.IngredientId == id);
      _db.Ingredients.Remove(thisIngredient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      Ingredient thisIngredient = _db.Ingredients.FirstOrDefault(ingredient => ingredient.IngredientId == id);
      ViewBag.Recipes = new SelectList(_db.Recipes, "RecipeId", "Ingredient");
      return View(thisIngredient);
    }
    public ActionResult AddIngredient(Ingredient ingredient, int recipeId)
    {
#nullable enable
      RecipeIngredient? joinEntity = _db.RecipeIngredients.FirstOrDefault(join => (join.IngredientId == ingredient.IngredientId && join.RecipeId == recipeId));
#nullable disable

      if (joinEntity == null && ingredient.IngredientId != 0)
      {
        _db.RecipeIngredients.Add(new RecipeIngredient() { IngredientId = ingredient.IngredientId, RecipeId = recipeId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = ingredient.IngredientId });
    }
  }

}
