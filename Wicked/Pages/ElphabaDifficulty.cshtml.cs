using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WickedGame.Services;
using WickedLogic;
using System;

namespace WickedGame.Pages
{
    public class ElphabaDifficultyModel : PageModel
    {
        [BindProperty]
        public string SelectedDifficulty { get; set; }
        public Levels? DifficultyLevel { get; private set; }
        private readonly GameService gameService;

        public ElphabaDifficultyModel(GameService gameService)
        {
            this.gameService = gameService;
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("OnPost triggered");
            Console.WriteLine($"Selected Difficulty: {SelectedDifficulty}");
            if (Enum.TryParse<Levels>(SelectedDifficulty, true, out var level))
            {
                DifficultyLevel = level;
                gameService.StartGame(level);
                return RedirectToPage("/Game/GameElphaba");
            }
            ModelState.AddModelError(string.Empty, "Invalid difficulty selected.");
            return Page();
        }
    }
}
