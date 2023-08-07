using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using Microsoft.EntityFrameworkCore;
using system.Collections.Generic;

namespace RecipeBox.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly RecipeBoxContext _db
        
        public IngredientsController(RecipeBoxContext db)
        {
            _db = db;
        }
    }
}