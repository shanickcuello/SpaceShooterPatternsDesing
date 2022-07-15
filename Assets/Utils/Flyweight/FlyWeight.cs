namespace Utils.Flyweight
{
    public class FlyWeight
    {
        public float asteroidSpeed;
        public float asteroidPartSpeed;
        public float asteroidPartPoint;
        public float asteroidPoint;

        public static readonly FlyWeight Asteroid = new FlyWeight
        {
            asteroidSpeed = 1f,
            asteroidPoint = 5,
        };
        public static readonly FlyWeight AsteroidPart = new FlyWeight
        {
            asteroidPartSpeed = 2f,
            asteroidPartPoint = 10,
        };
    }
}
