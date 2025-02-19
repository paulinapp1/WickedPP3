using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using Wicked.Services;
using WickedGame.Services;

namespace Wicked.Pages
{
    public class HallOfFameModel : PageModel
   {
        private readonly GameService gameService; 
        public List<WickedScores> TopScores= new List<WickedScores>();
        private readonly IHttpClientFactory httpClientFactory;
        public HallOfFameModel(IHttpClientFactory HttpClientFactory, GameService gameService)
        {
            httpClientFactory = HttpClientFactory ?? throw new ArgumentNullException(nameof(HttpClientFactory)); 
            this.gameService = gameService;  
        }

        public async Task OnGetAsync()
        {
            var httpClient = httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7013/api/scores/getScore?limit=5");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                TopScores = JsonSerializer.Deserialize<List<WickedScores>>(json);
            }
            else
            {
                TopScores = new List<WickedScores>();
            }
        }
    }
}

