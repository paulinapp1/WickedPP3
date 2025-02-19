using MongoDB.Driver;
using System.Reflection.Emit;
using Wicked.Services;
using WickedLogic;

namespace WickedGame.Services
{
    public class GameService
    {
        public Level level;
        private GameInstance gameInstance;
        private readonly IMongoCollection<WickedScores> wickedScores;
        public GameService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("WickedGame");
            wickedScores = database.GetCollection<WickedScores>("WickedScores");
        }

        public int Score => gameInstance.Score;
        public int CurrentScore => gameInstance.CurrentScore;
        public async Task SaveScoreAsync(string name, int score)
        {
            var newScore = new WickedScores
            {
                Score = score,
                Name = name
            };
            await wickedScores.InsertOneAsync(newScore);
            Console.WriteLine($"Saving score for {name} with score {Score}");
        }
        public async Task<List<WickedScores>> GetTopScores(int limit = 10)
        {
            return await wickedScores.Find(_ => true).SortByDescending(x => x.Score).Limit(limit).ToListAsync();
        }
      
        public void StartGame(Levels difficulty)
        {
            Console.WriteLine($"Starting game with difficulty: {difficulty}");
            var level = Level.GetLevel(difficulty);
            gameInstance = new GameInstance(level);
            gameInstance.StartGame(difficulty);
            Console.WriteLine("Game initialized successfully!");
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

        
    }
}