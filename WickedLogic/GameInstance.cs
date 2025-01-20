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

        public Level difficultyLevel;
        private bool isGameOver;


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

      

        private bool IsCollisionWithTree(Point position) =>
            TakenSpots.ContainsKey(position) && TakenSpots[position] == "tree";
        private bool IsCoordinateMinus(Point position) => position.X < 0 || position.Y < 0;

        private bool IsCollisionWithBody(Point position) =>
            TakenSpots.ContainsKey(position) && TakenSpots[position] == "mainC" || 
            TakenSpots.ContainsKey(position) && TakenSpots[position] == "follower";


        private bool IsCollisionWithWall(Point position) =>
            position.X < 0 || position.X >= map.SizeX ||
            position.Y < 0 || position.Y >= map.SizeY;


        public bool IsGameOver()
        {
            return isGameOver;
        }

        private bool CheckCollisions()
        {
            if (IsCollisionWithTree(currentHead) || IsCollisionWithWall(currentHead) || 
                IsCollisionWithBody(currentHead) || IsCoordinateMinus(currentHead))
            {
                isGameOver = true;
                return true;
            }
            return false;
        }
       
        private void UpdateGameState(Point newHead)
        {
            mainBody.Add(newHead);

            if (HasGainedFollower())
            {
                TakenSpots[newHead] = "creature";
                MapManager.UpdateMap(TakenSpots, map);
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

        public void StartGame(Levels difficulty)
        {
            map = MapManager.InitializeMap(difficulty);
            InitializeInterrupts();
        }

        public void InitializeInterrupts()
        {
            MapManager.GenerateInterrupts(TakenSpots, map, "creature", 5);
            MapManager.GenerateInterrupts(TakenSpots, map, "tree", 3);
            TakenSpots[currentHead] = "mainC";
        }

       
    }
}
