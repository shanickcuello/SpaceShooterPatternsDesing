using UnityEngine;
using UseCases.Enemys;
using UseCases.Services.PoolService;
using UseCases.Ships;

namespace UseCases.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        public float speed;
        public float timeToDie;
        public Ship owner;

        void Update()
        {
            transform.position += transform.right * speed * Time.deltaTime;

            timeToDie -= Time.deltaTime;

            if (timeToDie<= 0)
            {
                gameObject.Release();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy en = collision.GetComponent<Enemy>();

            if (en)
            {
                owner.TargetHit(); //Le digo al player que le pegue

                en.GetShot(); //Le hago damage al enemigo
                gameObject.Release();
            }
        }

        public void OnReuse()
        {
        }

        public void OnRelease()
        {
        }
    }
}
