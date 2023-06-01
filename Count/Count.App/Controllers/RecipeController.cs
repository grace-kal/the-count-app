using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Count.App.Controllers
{
    public class RecipeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetRecipesBulk()
        {
            await GetRecipesBulk20();
            return View();
        }
        private async Task GetRecipesBulk20()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/random?tags=vegetarian%2Cdessert&number=20"),
                Headers =
    {
        { "X-RapidAPI-Key", "d05285754dmshc154de454e33380p169061jsnc27ef32ddaf4" },
        { "X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
