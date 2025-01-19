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

        public int Score => throw new NotImplementedException();

        public Level difficultyLevel;
        private bool isGameOver;
        public DateTime startTime;
        public TimeSpan timeLimit;

        public TimeSpan GetRemainingTime()
        {
 
            if (startTime == default)
            {
                return TimeSpan.Zero;
            }

            TimeSpan elapsedTime = DateTime.Now - startTime;

            TimeSpan remainingTime = timeLimit - elapsedTime;

            if (remainingTime < TimeSpan.Zero)
            {
                remainingTime = TimeSpan.Zero;
            }

            return remainingTime;
        }


        public GameInstance(Level level)
        {
            TakenSpots = new Dictionary<Point, string>();
            mainBody = new List<Point>();
            currentHead = new Point(3, 4);
            mainBody.Add(currentHead);
            TakenSpots[currentHead] = "mainC";
            currentDirection = Direction.Up;
            difficultyLevel = level;
        }




        private bool HasGainedFollower()
        { 

            return TakenSpots.ContainsKey(currentHead) && TakenSpots[currentHead] == "creature";
        }


        private Point GetNextHeadPosition()
        {
            return currentDirection switch
            {
                Direction.Up => new Point(currentHead.X, currentHead.Y - 1),
                Direction.Down => new Point(currentHead.X, currentHead.Y + 1),
                Direction.Right => new Point(currentHead.X + 1, currentHead.Y),
                Direction.Left => new Point(currentHead.X - 1, currentHead.Y),
                _ => currentHead
            };
        }


        public void ChangeDirection(Direction newDirection)
        {

            if (mainBody.Count == 1 || (currentDirection != newDirection &&
                !((currentDirection == Direction.Up && newDirection == Direction.Down) ||
                  (currentDirection == Direction.Left && newDirection == Direction.Right) ||
                  (currentDirection == Direction.Down && newDirection == Direction.Up) ||
                  (currentDirection == Direction.Right && newDirection == Direction.Left))))
            {
                currentDirection = newDirection;
            }
        }


        private void UpdateMap()
        {
            Point newCreaturePosition = Point.GeneratePoint(map);
            while (TakenSpots.ContainsKey(newCreaturePosition))
            {
                newCreaturePosition = Point.GeneratePoint(map);
            }
            TakenSpots[newCreaturePosition] = "creature";
        }


        private bool IsCollisionWithTree(Point position) =>
            TakenSpots.ContainsKey(position) && TakenSpots[position] == "tree";
        private bool IsCoordinateMinus(Point position) => position.X < 0 || position.Y < 0;

        private bool IsCollisionWithBody(Point position) =>
            TakenSpots.ContainsKey(position) && TakenSpots[position] == "mainC";


        private bool IsCollisionWithWall(Point position) =>
            position.X < 0 || position.X >= map.SizeX ||
            position.Y < 0 || position.Y >= map.SizeY;


        public bool IsGameOver()
        {
            return isGameOver;
        }


        private bool IsTimeUp()
        {
            return DateTime.Now - startTime >= timeLimit;
        }


        private bool CheckCollisions()
        {
            if (IsCollisionWithTree(currentHead) || IsCollisionWithWall(currentHead) || 
                IsCollisionWithBody(currentHead) || IsTimeUp() || 
                IsCoordinateMinus(currentHead))
            {
                isGameOver = true;
                return true;
            }
            return false;
        }


   

        private void UpdateVisuals()
        {


            for (int i = 0; i < mainBody.Count; i++)
            {
                var segment = mainBody[i];

                if (i == mainBody.Count - 1)
                {
                    TakenSpots[segment] = "mainC";
                }
                else
                {
                    TakenSpots[segment] = "creature";
                }
            }
        }
        private void UpdateGameState(Point newHead)
        {
            mainBody.Add(newHead);

            if (HasGainedFollower())
            {
                TakenSpots[newHead] = "creature";
                UpdateMap();
            }
            else
            {
                TakenSpots[newHead] = "mainC";
                Point tail = mainBody[0];
                TakenSpots.Remove(tail);
                mainBody.RemoveAt(0);
            }

            UpdateVisuals();
        }
        public void Move(Direction newDirection)
        {
            if (isGameOver) return;

            ChangeDirection(newDirection);

            Point newHead = GetNextHeadPosition();
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
       


        private void InitializeGameState(Levels difficulty)
        {

            startTime = DateTime.Now;
            timeLimit = difficulty switch
            {
                Levels.Easy => TimeSpan.FromMinutes(15),
                Levels.Medium => TimeSpan.FromMinutes(10),
                Levels.Hard => TimeSpan.FromMinutes(5),
                Levels.Extreme => TimeSpan.FromMinutes(1),
                _ => TimeSpan.FromMinutes(15)
            };
        }

        public void StartGame(Levels difficulty)
        {


            InitializeMap(difficulty);
            InitializeInterrupts();
            InitializeGameState(difficulty);


        }

        public void InitializeInterrupts()
        {
            MapManager.GenerateInterrupts(TakenSpots, map, "creature", 5);
            MapManager.GenerateInterrupts(TakenSpots, map, "tree", 3);
            TakenSpots[currentHead] = "mainC";
        }

        private void InitializeMap(Levels difficulty)
        {
            map = difficulty switch
            {
                Levels.Easy => new Map(20, 20),
                Levels.Medium => new Map(15, 15),
                Levels.Hard => new Map(10, 10),
                Levels.Extreme => new Map(5, 5),
                _ => new Map(15, 15)
            };
            Console.WriteLine(map.ToString());
        }
    }
}
