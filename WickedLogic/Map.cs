
namespace WickedLogic
{
    public class Map
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public override string ToString()
        {
            return $"({SizeX}, {SizeY})";
        }

        public Map(int sizeX , int sizeY )
        {
         
            SizeX = sizeX;
            SizeY = sizeY;

        }
    }
}
