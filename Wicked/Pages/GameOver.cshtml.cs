using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using WickedGame.Services;


namespace WickedGame.Pages
{
    public class GameOverModel : PageModel
    {
        private readonly GameService gameService;
        private readonly IHttpClientFactory HttpClientFactory;
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public int Score { get; private set; }
        public GameOverModel(IHttpClientFactory httpClientFactory, GameService gameService)
        {
            this.HttpClientFactory = httpClientFactory;
            this.gameService = gameService;
        }
        public void OnGet()
        {
            Score = gameService.Score;
            Console.WriteLine($"GameOverModel: Score is {Score}");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var scoreData = new
                {
                    Name = Name,
                    Score= gameService.Score
                };
                Console.WriteLine($"Submitting score: {gameService.Score} for {Name}");
                var jsonContent = new StringContent(JsonSerializer.Serialize(scoreData), Encoding.UTF8, "application/json");
                var httpClient = HttpClientFactory.CreateClient();

                HttpResponseMessage response = await httpClient.PostAsync("https://localhost:7013/api/scores/save", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/HallOfFame");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error saving score.");
                }
            }

            return Page();
        }
    }
}
