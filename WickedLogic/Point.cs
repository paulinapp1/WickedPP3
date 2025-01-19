
namespace WickedLogic
{
        public readonly struct Point
        {
            public readonly int X, Y;
            private readonly Random _random = new Random();
            public Point(int x, int y) => (X, Y) = (x, y);
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
        public static Point GeneratePoint(Map map)
        {
            Random random = new Random();
            int x = random.Next(map.SizeX);
            int y = random.Next(map.SizeY);
            return new Point(x, y);
        }

        //Ta funkcja jest po to, bo C# nie będzie wiedział jak porównywać obiekty typu Point
        //Domyślnie nie będzie porównywał ich wartości tylko obiekty referencyjne
        public override bool Equals(object obj)
        {
            if (obj is Point other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }
        //Ta metoda upewnia się, że C# nie użyje domyślnej metody, która sprawiłaby, że obiekty
        //klasy Point z tymi samymy współrzędnymi byłyby traktowane jako inne bo są innymi
        //instancjami obiektu ale mają te same wartości
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
 
    }

        }
    


