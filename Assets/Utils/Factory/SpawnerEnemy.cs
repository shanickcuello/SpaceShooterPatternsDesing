using Features.Asteroid;
using UnityEngine;

namespace Utils.Factory
{
    public class SpawnerEnemy : MonoBehaviour
    {
        private static SpawnerEnemy _instance;

        public static SpawnerEnemy Instance { get { return _instance; } }

        public Enemy enemyPrefab;
        public Asteroid asteroidPrefab;

        public int enemyInitialStock;
        public int asteroidInitialStock;

        public ObjectPool<Enemy> enemyPool;
        public ObjectPool<Asteroid> asteroidPool;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(_instance);
            }

            _instance = this;

            enemyPool = new ObjectPool<Enemy>(EnemyFactory, Enemy.TurnOn, Enemy.TurnOff, enemyInitialStock, true);
            asteroidPool = new ObjectPool<Asteroid>(AsteroidFactory, Asteroid.TurnOn, Asteroid.TurnOff, asteroidInitialStock, true);
        }
        public Enemy EnemyFactory()
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.transform.parent = GameObject.Find("Game").transform;
            return enemy;
        }
        public Asteroid AsteroidFactory()
        {
            Asteroid e = Instantiate(asteroidPrefab);
            e.transform.parent = GameObject.Find("Game").transform;
            return e;
        }
        public void ReturnEnemy(Enemy enemy)
        {
            enemyPool.ReturnObject(enemy);
        }
    
        public void ReturnAsteroid(Asteroid aste)
        {
            asteroidPool.ReturnObject(aste);
        }
    
    }
}
