using UnityEngine;
using Utils.Factory;
using Utils.Flyweight;

namespace Features.Asteroid
{
    public class Asteroid : Enemy, IPrototype
    {
        protected override float GetSpeedToMove()
        {
            return FlyWeight.Asteroid.asteroidSpeed;
        }
        public override void NotifyDeath()
        {
            NotifyToObservers("AddAsteroidPoint");
            NotifyToObservers("EnemyDead");
            SpawnerEnemy.Instance.ReturnAsteroid(this);
            Clone();
            Destroy(this,0.1f);
        }
        public override void GetShot()
        {
            NotifyDeath();
        }
        public Enemy Clone()
        {
            Enemy enemyFromPool = manager.GetEnemyFromPool();
            var position = transform.position;
            enemyFromPool.SetManager(manager).SetTarget(_target).SetPosition(position).SetRotation(Quaternion.identity).SetNotify(false);

            Enemy b = manager.GetEnemyFromPool();
            Vector3 offset = new Vector3(1.5f, 0, 0);
            b.SetManager(manager).SetTarget(_target).SetPosition(position + offset).SetRotation(Quaternion.identity).SetNotify(false);

            return enemyFromPool;
        }
    }
}
