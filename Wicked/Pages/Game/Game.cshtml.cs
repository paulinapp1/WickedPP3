using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Serialization;
using WickedGame.Services;
using WickedLogic;

namespace WickedGame.Pages.Game
{
    public class GameModel : PageModel
    {
        private readonly GameService gameService;

        public List<List<string>> MapGrid { get; private set; }
        public TimeSpan RemainingTime { get; private set; }
        [BindProperty]
        public Levels SelectedDifficulty { get; set; }

        public void OnPost()
        {
            Console.WriteLine($"Selected Difficulty: {SelectedDifficulty}");

            // Start the game with the selected difficulty
            gameService.StartGame(SelectedDifficulty);
        }
        public GameModel(GameService gameService)
        {
            this.gameService = gameService;
        }


        public void OnGet()
        {
            LoadMapGrid();
            LoadRemainingTime();
        }

        // Method to load the map grid based on the selected difficulty
        private void LoadMapGrid()
        {
            MapGrid = gameService.GetMapGrid(SelectedDifficulty);
            Console.WriteLine("Map grid from Game.cs:");
            foreach (var row in MapGrid)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }

        // Method to load the remaining time from the game service
        private void LoadRemainingTime()
        {
            RemainingTime = gameService.GetRemainingTime();
        }
    

    public IActionResult OnPostMoveUp()
        {
            gameService.Move(Direction.Up);
       
            if (gameService.IsGameOver())
            {
                Console.WriteLine("Game Over!");
                return RedirectToPage("/GameOver");
            }

             MapGrid = gameService.GetMapGrid(SelectedDifficulty);
            RemainingTime = gameService.GetRemainingTime();
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

            MapGrid = gameService.GetMapGrid(SelectedDifficulty);
            RemainingTime = gameService.GetRemainingTime();
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

            MapGrid = gameService.GetMapGrid(SelectedDifficulty);
            RemainingTime = gameService.GetRemainingTime();
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

            MapGrid = gameService.GetMapGrid(SelectedDifficulty);
            RemainingTime = gameService.GetRemainingTime();
            return Page();

        }

        public IActionResult OnGetTime()
        {
            RemainingTime = gameService.GetRemainingTime();

            // Send the remaining time as JSON
            return new JsonResult(new
            {
                minutes = RemainingTime.Minutes,
                seconds = RemainingTime.Seconds
            });
        }


      
    }
}