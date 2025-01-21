
namespace WickedLogic
{
    public class Level
    {
        public Levels Difficulty { get; set; }
        public Level(Levels difficulty = Levels.Medium)
        {
            Difficulty = difficulty;
        }
        public static Level GetLevel(Levels difficulty)
        {
            switch (difficulty)
            {
                case Levels.Easy:
                    return new Level(Levels.Easy);
                case Levels.Medium:
                    return new Level(Levels.Medium); 
                case Levels.Hard:
                    return new Level(Levels.Hard);
                default:
                    return new Level(Levels.Extreme); 
            }
        }
    }
}
