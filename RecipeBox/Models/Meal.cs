using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using RestSharp;
using RecipeBox.Models;

namespace RecipeBox.Models
{
    public class RandomMeal
    {
        public string strMeal { get; set; }
        public string Instructions { get; set; }
        public string strIngredient1 { get; set; }
        public string strIngredient2 { get; set; }
        public string strIngredient3 { get; set; }
        public string strIngredient4 { get; set; }
        public string strIngredient5 { get; set; }
        public string strIngredient6 { get; set; }
        public string strIngredient7 { get; set; }
        public string strIngredient8 { get; set; }
        public string strIngredient9 { get; set; }
        public string strIngredient10 { get; set; }
        public string strIngredient11 { get; set; }
        public string strIngredient12 { get; set; }
        public string strIngredient13 { get; set; }
        public string strIngredient14 { get; set; }
        public string strIngredient15 { get; set; }
        public string strIngredient16 { get; set; }
        public string strIngredient17 { get; set; }
        public string strIngredient18 { get; set; }
        public string strIngredient19 { get; set; }
        public string strIngredient20 { get; set; }
        public string strMeasure1 { get; set; }
        public string strMeasure2 { get; set; }
        public string strMeasure3 { get; set; }
        public string strMeasure4 { get; set; }
        public string strMeasure5 { get; set; }
        public string strMeasure6 { get; set; }
        public string strMeasure7 { get; set; }
        public string strMeasure8 { get; set; }
        public string strMeasure9 { get; set; }
        public string strMeasure10 { get; set; }
        public string strMeasure11 { get; set; }
        public string strMeasure12 { get; set; }
        public string strMeasure13 { get; set; }
        public string strMeasure14 { get; set; }
        public string strMeasure15 { get; set; }
        public string strMeasure16 { get; set; }
        public string strMeasure17 { get; set; }
        public string strMeasure18 { get; set; }
        public string strMeasure19 { get; set; }
        public string strMeasure20 { get; set; }

        public static async Task<RandomMeal> GetMeal(string apiKey)
        {
              string endpoint = "1/random.php"; // Replace with the actual API endpoint
            string result = await ApiHelper.ApiCall(apiKey, endpoint);
            
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
            JArray meals = (JArray)jsonResponse["meals"];
            if (meals != null && meals.Count > 0)
            {
                RandomMeal randomMeal = meals[0].ToObject<RandomMeal>();
                return randomMeal;
            }
            return new RandomMeal();
    }
}
}