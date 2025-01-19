using System.Reflection.Emit;
using WickedLogic;

namespace WickedGame.Services
{
    public class GameService: IGameService
    {
        public Level level;
        private GameInstance gameInstance;
        public void Initialize(Levels difficulty)
        {
            var level = Level.GetLevel(difficulty);
            gameInstance = new GameInstance(level);
        }
        public void StartGame(Levels difficulty)
        {
            Console.WriteLine($"Starting game with difficulty: {difficulty}");
            var level = Level.GetLevel(difficulty);
            gameInstance = new GameInstance(level);
            gameInstance.StartGame(difficulty);
            Console.WriteLine("Game initialized successfully!");
        }
        public TimeSpan GetRemainingTime()
        {
            return gameInstance.GetRemainingTime();
            
        }

        public bool IsGameOver()
        {
            return gameInstance.IsGameOver();
        }
       
        public void Move(Direction direction)
        {
            
            gameInstance.Move(direction);
        }


        public List<List<string>> GetMapGrid(Levels selectedDifficulty)
        {
            var grid = new List<List<string>>();

            for (int y = 0; y < gameInstance.map.SizeY; y++)
            {
                Console.WriteLine(gameInstance.map.SizeY);
                var row = new List<string>();
                for (int x = 0; x < gameInstance.map.SizeX; x++)
                {
                    Console.WriteLine(gameInstance.map.SizeX);
                    var point = new Point(x, y);

                    if (gameInstance.TakenSpots.TryGetValue(point, out string value))
                    {
                        row.Add(value); 
                    }
                    else
                    {
                        row.Add("empty"); 
                    }
                }
                grid.Add(row);
            }

            Console.WriteLine("Generated MapGrid:");
            foreach (var row in grid)
            {
                Console.WriteLine(string.Join(", ", row));
            }

            return grid;

        }

        public int Score => gameInstance.mainBody.Count;
    }
}