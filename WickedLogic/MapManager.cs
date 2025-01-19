using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WickedLogic
{
    public class MapManager
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
    }
}
