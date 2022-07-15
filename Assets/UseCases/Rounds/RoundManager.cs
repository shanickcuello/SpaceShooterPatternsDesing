using System.Collections;
using UnityEngine;
using UseCases.Enemys;
using UseCases.Services.PoolService;
using UseCases.Ships;

namespace UseCases.Rounds
{
    public class RoundManager : MonoBehaviour
    {
        public GameObject enemyPrefab;

        public Transform spawnPoints;

        Transform[] _spawnPositions;

        Transform _target;

        int _totalEnemies;

        int _actualRound;

        void Start()
        {
            _target = FindObjectOfType<Ship>().transform; //target que le voy a asignar al enemigo

            _spawnPositions = spawnPoints.GetComponentsInChildren<Transform>(); //Los puntos de spawn para los enemigos

            StartCoroutine(SpawnEnemies());
        }

        public void EnemyDead(Enemy enemy)
        {
            enemy.onDie -= EnemyDead;
            _totalEnemies--; //Murio un enemy

            if (_totalEnemies <= 0) //Si no hay mas enemies
            {
                StartCoroutine(SpawnEnemies()); //Nueva wave
            }
        }

        //Devuelve cuantos enemigos crear por ronda
        int CalculateEnemiesToSpawn(int round)
        {
            return round * 2;
        }

        IEnumerator SpawnEnemies()
        {
            _actualRound++; //Nueva ronda

            _totalEnemies = CalculateEnemiesToSpawn(_actualRound); //Total de enemigos a spawnear

            int enemiesToSpawn = _totalEnemies;

            int enemiesCont = 0;

            while (enemiesCont < enemiesToSpawn)
            {
                int posToSpawn = Random.Range(0, _spawnPositions.Length); //Posicion en la que va a spawnear

                var enemy = enemyPrefab.Reuse<Enemy>(_spawnPositions[posToSpawn].position, transform.rotation);
                enemy.onDie += EnemyDead;
                enemy.target = _target; //Le paso el target

                enemiesCont++;

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}