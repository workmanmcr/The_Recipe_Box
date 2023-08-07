using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using Microsoft.EntityFrameworkCore;
using system.Collections.Generic;


namespace RecipeBox.Controllers
{
public class RecipesController : Controller
{
private readonly RecipseBoxContext _dc;
public RecipesController(RecipeBoxContext db)
{
  _db = db;
}
public ActionResult Index()
{
  return View();
}

public ActionResult Create()
{
    ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName")
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
    Recipe thisRecipe = _db.Recipes.Include(recipe => recipe.Ingredients).FirstOrDefault(recipe => recipe.RecipeId == id);
    return View(thisRecipe)
}

public ActionResult Edit(int id)
{
    Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "IngredientName")
    return View(thisRecipe);
}

[HttpPost]
public ActionResult Edit(Recipe recipe)
{
    _db.Recipes.Update(recipe);
    _db.SaveChanges();
    return RedirectToAction("Index")
}
}

}