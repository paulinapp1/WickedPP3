using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WickedGame.Services;
using WickedLogic;
using System;

namespace WickedGame.Pages
{
    public class GlindaDifficultyModel : PageModel
    {
        [BindProperty]
        public string SelectedDifficulty { get; set; }

        public Levels? DifficultyLevel { get; private set; }


        private readonly GameService gameService;

        // Constructor to inject the GameService
        public GlindaDifficultyModel(GameService gameService)
        {
            this.gameService = gameService;
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost triggered");
            Console.WriteLine($"Selected Difficulty: {SelectedDifficulty}");

            // Convert the string input to the enum
            if (Enum.TryParse<Levels>(SelectedDifficulty, true, out var level))
            {
                DifficultyLevel = level;
                gameService.StartGame(level);

                // Redirect to the target page with the difficulty level as a query parameter
                return RedirectToPage("/Game/Game");
            }
            ModelState.AddModelError(string.Empty, "Invalid difficulty selected.");
            return Page();
        }

    }
}
