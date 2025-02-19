using Microsoft.AspNetCore.Mvc;
using Wicked.Services;
using WickedGame.Services;

namespace Wicked.Controllers
{
    [Route("api/scores")]
    public class ScoreController: ControllerBase
    {
        private readonly GameService gameService;
        public ScoreController(GameService gameService)
        {
            this.gameService = gameService;
        }
        [HttpPost("save")]
        public async Task<ActionResult> SaveScore([FromBody] WickedScores score)
        {
            if (score == null || string.IsNullOrEmpty(score.Name) || score.Score < 0)
                return BadRequest("Invalid score data.");

            await gameService.SaveScoreAsync(score.Name, score.Score);
            return Ok("Score saved successfully.");
        }
        [HttpGet("getScore")]
        public async Task<ActionResult> GetTopScores(int limit=1)
        {
            var topScores= await gameService.GetTopScores(limit);
            return Ok(topScores);
        }

    }
}
