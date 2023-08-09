using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RecipeBox.Models
{
    public class RandomMeal
    {
        public string Meal { get; set; }
        public string Instructions { get; set; }
        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; }
        public string Ingredient3 { get; set; }
        public string Ingredient4 { get; set; }
        public string Ingredient5 { get; set; }
        public string Ingredient6 { get; set; }
        public string Ingredient7 { get; set; }
        public string Ingredient8 { get; set; }
        public string Ingredient9 { get; set; }
        public string Ingredient10 { get; set; }
        public string Ingredient11 { get; set; }
        public string Ingredient12 { get; set; }
        public string Ingredient13 { get; set; }
        public string Ingredient14 { get; set; }
        public string Ingredient15 { get; set; }
        public string Ingredient16 { get; set; }
        public string Ingredient17 { get; set; }
        public string Ingredient18 { get; set; }
        public string Ingredient19 { get; set; }
        public string Ingredient20 { get; set; }
        public string Measure1 { get; set; }
        public string Measure2 { get; set; }
        public string Measure3 { get; set; }
        public string Measure4 { get; set; }
        public string Measure5 { get; set; }
        public string Measure6 { get; set; }
        public string Measure7 { get; set; }
        public string Measure8 { get; set; }
        public string Measure9 { get; set; }
        public string Measure10 { get; set; }
        public string Measure11 { get; set; }
        public string Measure12 { get; set; }
        public string Measure13 { get; set; }
        public string Measure14 { get; set; }
        public string Measure15 { get; set; }
        public string Measure16 { get; set; }
        public string Measure17 { get; set; }
        public string Measure18 { get; set; }
        public string Measure19 { get; set; }
        public string Measure20 { get; set; }

        public static List<RandomMeal> GetMeal(string apiKey)
        {
            Task<string> apiCallTask = ApiHelper.ApiCall(apiKey);
            string result = apiCallTask.Result;

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
            List<RandomMeal> mealList = JsonConvert.DeserializeObject<List<RandomMeal>>(jsonResponse["results"].ToString());

            return mealList;
        }
    }
}