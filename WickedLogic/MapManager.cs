using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WickedLogic
{
    public static class MapManager
    {
        public static void GenerateInterrupts(Dictionary<Point, string> takenSpots, Map map, string interruptType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Point nextPosition = Point.GeneratePoint(map);
                if (!takenSpots.ContainsKey(nextPosition))
                {
                    takenSpots.Add(nextPosition, interruptType);
                }
            }
        }
        public static void UpdateMap(Dictionary<Point, string> takenSpots, Map map)
        {
            Point newCreaturePosition = Point.GeneratePoint(map);
            while (takenSpots.ContainsKey(newCreaturePosition))
            {
                newCreaturePosition = Point.GeneratePoint(map);
            }
            takenSpots[newCreaturePosition] = "creature";
        }
        public static void UpdateVisuals(Dictionary<Point,string> takenSpots, List<Point> mainBody)
        {


            for (int i = 0; i < mainBody.Count; i++)
            {
                var segment = mainBody[i];

                if (i == mainBody.Count - 1)
                {
                    takenSpots[segment] = "mainC";
                }
                else
                {
                    takenSpots[segment] = "follower";
                }
            }
        }
        public static Map InitializeMap(Levels difficulty)
        {
            Map map = difficulty switch
            {
                Levels.Easy => new Map(20, 20),
                Levels.Medium => new Map(15, 15),
                Levels.Hard => new Map(10, 10),
                Levels.Extreme => new Map(5, 5),
                _ => new Map(15, 15)
            };
            return map;
        }
    }
}
