using System.Threading.Tasks;
using RestSharp;

namespace RecipeBox.Models
{
   class ApiHelper
    {
        public static async Task<string> ApiCall(string apiKey, string endpoint)
        {
            RestClient client = new RestClient("https://www.themealdb.com/api/json/v1/");
            RestRequest request = new RestRequest(endpoint, Method.Get);
            RestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }
    }
}