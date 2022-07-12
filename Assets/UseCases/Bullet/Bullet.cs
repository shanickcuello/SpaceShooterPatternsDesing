using UnityEngine;
using UseCases.Enemys;

namespace UseCases.Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public float timeToDie;
        public Ship.Ship owner;

        // Update is called once per frame
        void Update()
        {
            //Movimiento
            transform.position += transform.right * speed * Time.deltaTime;

            //Lifetime
            timeToDie -= Time.deltaTime;

            if (timeToDie<= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy en = collision.GetComponent<Enemy>();

            if (en)
            {
                owner.TargetHit(); //Le digo al player que le pegue

                en.GetShot(); //Le hago damage al enemigo

                Destroy(gameObject); //Me destruyo
            }
        }
    }
}
