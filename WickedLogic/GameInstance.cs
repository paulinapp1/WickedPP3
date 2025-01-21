namespace WickedLogic
{
    public class GameInstance :IGameInstance
    {

        public Map map { get; private set; }
        public List<Point> mainBody;

        public Dictionary<Point, string> TakenSpots { get; set; }
        public Point point;

        public Point currentHead { get; set; }

        public Direction currentDirection { get; set; }
      
        public int Score => mainBody.Count()-1;
        public int CurrentScore;
        public Level difficultyLevel;
        private bool isGameOver;


        public GameInstance(Level level)
        {
            TakenSpots = new Dictionary<Point, string>();
            mainBody = new List<Point>();
            currentHead = new Point(3, 4);
            mainBody.Add(currentHead);
            TakenSpots[currentHead] = "mainC";
            difficultyLevel = level;
        }

        

        public bool IsGameOver()
        {
            return isGameOver;
        }

        private bool CheckCollisions()
        {
            if (GameManager.IsCollisionWithTree(currentHead, TakenSpots) || GameManager.IsCollisionWithWall(currentHead, map) || 
                GameManager.IsCollisionWithBody(currentHead, TakenSpots) || GameManager.IsCoordinateMinus(currentHead))
            {
                isGameOver = true;
                return true;
            }
            return false;
        }
       
        private void UpdateGameState(Point newHead)
        {
            mainBody.Add(newHead);

            if (GameManager.HasGainedFollower(TakenSpots,currentHead))
            {
                TakenSpots[newHead] = "creature";
                MapManager.UpdateMap(TakenSpots, map);
                CurrentScore++;
            }
            else
            {
                TakenSpots[newHead] = "mainC";
                Point tail = mainBody[0];
                TakenSpots.Remove(tail);
                mainBody.RemoveAt(0);
            }

            MapManager.UpdateVisuals(TakenSpots,mainBody);
        }
        public void Move(Direction newDirection)
        {
            if (isGameOver) return;

            currentDirection= GameManager.ChangeDirection(newDirection, currentDirection, mainBody);

            Point newHead = GameManager.GetNextHeadPosition(currentDirection, currentHead);
            Console.WriteLine($"New head position: {newHead}");
            currentHead = newHead;

            if (CheckCollisions())
            {
                Console.WriteLine("Collision detected!");
                isGameOver = true;
                return;
            }

            UpdateGameState(newHead);
        }

        public void StartGame(Levels difficulty)
        {
            map = MapManager.InitializeMap(difficulty);
            InitializeInterrupts(difficulty);
        }

        public void InitializeInterrupts(Levels difficulty)
        {
            if(difficulty == Levels.Easy)
            {
                MapManager.GenerateInterrupts(TakenSpots, map, "creature", 5);
                MapManager.GenerateInterrupts(TakenSpots, map, "tree", 6);
            }
            else if (difficulty == Levels.Medium)
            {
                MapManager.GenerateInterrupts(TakenSpots, map, "creature", 5);
                MapManager.GenerateInterrupts(TakenSpots, map, "tree", 4);
            }
            else
            {
                MapManager.GenerateInterrupts(TakenSpots, map, "creature", 3);
                MapManager.GenerateInterrupts(TakenSpots, map, "tree", 3);
            }

            
            TakenSpots[currentHead] = "mainC";
        }

       
    }
}
