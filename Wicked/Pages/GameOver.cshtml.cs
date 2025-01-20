using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WickedGame.Services;

namespace WickedGame.Pages
{
    public class GameOverModel : PageModel
    {
        private readonly GameService gameService;
        public int Score { get; private set; }
        public GameOverModel(GameService GameService)
        {
            gameService = GameService;
        }

        public void OnGet()
        {
            Score = gameService.Score;
        }
    }
}
