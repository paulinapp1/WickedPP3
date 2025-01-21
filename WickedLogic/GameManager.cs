using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WickedLogic
{
    public static class GameManager
    {
        public static bool IsCollisionWithTree(Point position, Dictionary<Point, string> takenSpots) =>
           takenSpots.ContainsKey(position) && takenSpots[position] == "tree";
        public static bool IsCoordinateMinus(Point position) => position.X < 0 || position.Y < 0;

        public static bool IsCollisionWithBody(Point Position, Dictionary<Point, string> takenSpots) =>
            takenSpots.ContainsKey(Position) && takenSpots[Position] == "mainC" ||
            takenSpots.ContainsKey(Position) && takenSpots[Position] == "follower";

        public static bool IsCollisionWithWall(Point Position,Map map) =>
            Position.X < 0 || Position.X >= map.SizeX ||
            Position.Y < 0 || Position.Y >= map.SizeY;

        public static Point GetNextHeadPosition(Direction CurrentDirection, Point CurrentHead)
        {
            return CurrentDirection switch
            {
                Direction.Up => new Point(CurrentHead.X, CurrentHead.Y - 1),
                Direction.Down => new Point(CurrentHead.X, CurrentHead.Y + 1),
                Direction.Right => new Point(CurrentHead.X + 1, CurrentHead.Y),
                Direction.Left => new Point(CurrentHead.X - 1, CurrentHead.Y),
                _ => CurrentHead
            };
        }


        public static Direction ChangeDirection(Direction NewDirection, Direction CurrentDirection, List<Point> MainBody)
        {

            if (MainBody.Count == 1 || (CurrentDirection != NewDirection &&
                !((CurrentDirection == Direction.Up && NewDirection == Direction.Down) ||
                  (CurrentDirection == Direction.Left && NewDirection == Direction.Right) ||
                  (CurrentDirection == Direction.Down && NewDirection == Direction.Up) ||
                  (CurrentDirection == Direction.Right && NewDirection == Direction.Left))))
            {
                CurrentDirection = NewDirection;
            }
            return CurrentDirection;
        }
            public static bool HasGainedFollower(Dictionary<Point, string> takenSpots, Point currentHead)
            {

                return takenSpots.ContainsKey(currentHead) && takenSpots[currentHead] == "creature";
            }

        }
    }

