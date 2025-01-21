using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Serialization;
using WickedGame.Services;
using WickedLogic;

namespace WickedGame.Pages.Game
{
    public class GameElphabaModel : PageModel
    {
        private readonly GameService gameService;
        public List<List<string>> MapGrid { get; private set; }
        public int CurrentScore;
      
        [BindProperty]
        public Levels SelectedDifficulty { get; set; }

        public void OnPost()
        {
            Console.WriteLine($"Selected Difficulty: {SelectedDifficulty}");
            gameService.StartGame(SelectedDifficulty);
        }
        public GameElphabaModel(GameService gameService)
        {
            this.gameService = gameService;
        }
        public void OnGet()
        {
            MapGrid = gameService.GetMapGrid(SelectedDifficulty);
            Console.WriteLine("Map grid from Game.cs:");
            foreach (var row in MapGrid)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }
        public IActionResult OnPostMoveUp()
        {
            gameService.Move(Direction.Up);

            if (gameService.IsGameOver())
            {
                Console.WriteLine("Game Over!");
                return RedirectToPage("/GameOver");
            }
            CurrentScore = gameService.Score;
            MapGrid = gameService.GetMapGrid(SelectedDifficulty);

            return Page();
        }
        public IActionResult OnPostMoveDown()
        {
            if (gameService.IsGameOver())
            {
                Console.WriteLine("Game Over!");
                return RedirectToPage("/GameOver");
            }
            gameService.Move(Direction.Down);
            CurrentScore = gameService.Score;
            MapGrid = gameService.GetMapGrid(SelectedDifficulty);

            return Page();
        }
        public IActionResult OnPostMoveLeft()
        {
            gameService.Move(Direction.Left);

            if (gameService.IsGameOver())
            {
                Console.WriteLine("Game Over!");
                return RedirectToPage("/GameOver");
            }
            CurrentScore = gameService.Score;
            MapGrid = gameService.GetMapGrid(SelectedDifficulty);

            return Page();
        }
        public IActionResult OnPostMoveRight()
        {
            gameService.Move(Direction.Right);

            if (gameService.IsGameOver())
            {
                Console.WriteLine("Game Over!");
                return RedirectToPage("/GameOver");
            }
            CurrentScore = gameService.Score;
            MapGrid = gameService.GetMapGrid(SelectedDifficulty);

            return Page();

        }
    }
}