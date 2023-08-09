using System.Threading.Tasks;
using RestSharp;

namespace RecipeBox.Models
{
  class ApiHelper
  {
   public static async Task<string> ApiCall(string apiKey)
{
    RestClient client = new RestClient("https://www.themealdb.com/api/json/v1/1/");
    RestRequest request = new RestRequest("random.php", Method.GET); // Use the correct endpoint
    RestResponse response = await client.ExecuteAsync(request);
    return response.Content;
}
  }
}