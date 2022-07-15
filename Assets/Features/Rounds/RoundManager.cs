using System.Collections;
using Features.Asteroid;
using Features.UI;
using UnityEngine;
using Utils.Factory;

namespace Features.Rounds
{
    public class RoundManager : MonoBehaviour, IObserver
    {
        public Transform spawnPoints;
        public int lastRound;
        Transform[] _spawnPositions;

        Transform _target;
        [SerializeField]
        int _totalEnemies;
    
        int _actualRound;
        [SerializeField]
        int _spawnedEnemiesAmount;
        bool roundEnded;

        LookUpTable<int, int> _enemyTable;

        Config screenConfig;

        private void Awake()
        {
            screenConfig = GameObject.FindObjectOfType<Config>();
            roundEnded = true;
        }
        void Start()
        {
            _spawnedEnemiesAmount = 0;
            _actualRound = 0;

            _enemyTable = new LookUpTable<int, int>(CalculateEnemiesToSpawn);
            for (int i = 1; i <= 10; i++)
            {
                _enemyTable.ReturnValue(i);
            }

            _target = FindObjectOfType<global::Player>().transform;

            _spawnPositions = spawnPoints.GetComponentsInChildren<Transform>();

            SpawnConfig();
        }
        public void StopSpawn()
        {
            StopAllCoroutines();
        }
        public void ContinueSpawn()
        {
            if(roundEnded)
                StartCoroutine(SpawnEnemies());
        }
        public void EnemyDead()
        {
            _totalEnemies--;

            if (_totalEnemies > 0) return;
            roundEnded = true;
            if (_actualRound < lastRound)
            {
                SpawnConfig();
                StartCoroutine(SpawnEnemies()); 
            }
            else
            {
                StopCoroutine(SpawnEnemies());
                screenConfig.EndGame();
            }
        }
        int CalculateEnemiesToSpawn(int round)
        {
            return round * 2;
        }
        private void SpawnConfig()
        {
            _actualRound++;
            _totalEnemies = _enemyTable.ReturnValue(_actualRound);
        }
        IEnumerator SpawnEnemies()
        {
            int enemiesToSpawn = _totalEnemies;        

            while (enemiesToSpawn > _spawnedEnemiesAmount)
            {
                int posToSpawn = Random.Range(0, _spawnPositions.Length);
                Enemy newEnemy;

                if ((_actualRound == 2 && _spawnedEnemiesAmount == 1))
                {
                    newEnemy = GetAsteroidFromPool();
                }
                else
                {
                    newEnemy = GetEnemyFromPool();
                }

                newEnemy.SetManager(this);
                newEnemy.SetTarget(_target);
                newEnemy.SetNotify(true);
                var transform1 = newEnemy.transform;
                transform1.position = _spawnPositions[posToSpawn].position; 
                transform1.rotation = Quaternion.identity;

                newEnemy.Subscribe(this); //Suscribo como observer en el enemigo
                newEnemy.Subscribe(screenConfig); //Suscribo screenConfig para Score
                _spawnedEnemiesAmount++;
                yield return new WaitForSeconds(0.5f);
            }

            roundEnded = false;
            _spawnedEnemiesAmount = 0;
        }

        public Enemy GetEnemyFromPool()
        {
            return SpawnerEnemy.Instance.enemyPool.GetObject();
        }
    
        public Asteroid.Asteroid GetAsteroidFromPool()
        {
            return SpawnerEnemy.Instance.asteroidPool.GetObject();
        }
    
        public void Notify(string action)
        {
            if (action == "EnemyDead")
            {
                EnemyDead();
            }
        }
    }
}
