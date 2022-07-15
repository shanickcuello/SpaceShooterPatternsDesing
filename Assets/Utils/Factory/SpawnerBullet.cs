using Features.Bullet;
using UnityEngine;

namespace Utils.Factory
{
    public class SpawnerBullet : MonoBehaviour
    {
        private static SpawnerBullet _instance;

        public static SpawnerBullet Instance { get { return _instance; } }

        public Bullet bulletPrefab;

        public int bulletInitialStock = 1;

        public ObjectPool<Bullet> pool;

        private void Start()
        {
            if (_instance)
            {
                Destroy(_instance);
            }

            _instance = this;

            pool = new ObjectPool<Bullet>(BulletFactory, Bullet.TurnOn, Bullet.TurnOff, bulletInitialStock, true);
        }
        public Bullet GetBulletFromPool(Transform transform, float lifeTime)
        {
            Bullet bullet = pool.GetObject();
            var transform1 = bullet.transform;
            transform1.position = transform.position;
            transform1.rotation = transform.rotation;
            bullet.timeToDie = lifeTime;

            return bullet;
        }
        public Bullet BulletFactory()
        {
            Bullet b = Instantiate(bulletPrefab);
            b.transform.parent = GameObject.Find("Game").transform;
            return b;
        }
        public void ReturnBullet(Bullet bullet)
        {
            pool.ReturnObject(bullet);
        }
    }
}
