using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UseCases.Bullets;
using UseCases.Enemys;
using UseCases.Services.PoolService;

namespace UseCases.Ships
{
    public class Ship : MonoBehaviour
    {
        public float speed;
        public float shootCooldown;
        public GameObject bulletPrefab;
        public Transform pointToSpawn;
        public Image cooldownBar;

        Camera _myCamera;
        bool _canShoot;
        Coroutine _shootCDCor;
        private Bullet _bullet;
         
        
        // Start is called before the first frame update
        void Start()
        {
            _myCamera = Camera.main;
            _canShoot = true;
            CompletedFireCooldown();
        }

        // Update is called once per frame
        void Update()
        {
            //Movimiento
            Vector3 lookAtPos = _myCamera.ScreenToWorldPoint(Input.mousePosition);
            lookAtPos.z = transform.position.z;
            transform.right = lookAtPos - transform.position;

            transform.position += (_myCamera.transform.right * Input.GetAxisRaw("Horizontal") + _myCamera.transform.up * Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;

            //Disparo
            if (Input.GetMouseButtonDown(0))
            {
                if (_canShoot) Shoot();
            }
        }

        void Shoot()
        {
            var bullet = bulletPrefab.Reuse<Bullet>(pointToSpawn.position, transform.rotation);
            //subscribe to bullet hit event
            bullet.OnHit += OnTargetHit;
            
            bullet.timeToDie = shootCooldown;  //Le paso el cooldown como tiempo de vida

            _shootCDCor = StartCoroutine(ShootCooldown());  //Corrutina del cooldown para volver a disparar
        }
        
        //Funcion para cuando la bala toca un enemigo
        public void OnTargetHit(Bullet bullet)
        {
            bullet.OnHit -= OnTargetHit;
            if (_shootCDCor != null)
            {
                StopCoroutine(_shootCDCor);
            }

            _canShoot = true;
            CompletedFireCooldown();
        }

        //Setea cambios de la barra de CD del UI
        void CompletedFireCooldown()
        {
            cooldownBar.color = Color.green;
            cooldownBar.fillAmount = 1;
        }

        IEnumerator ShootCooldown()
        {
            _canShoot = false;

            float ticks = 0;

            cooldownBar.color = Color.red;
            cooldownBar.fillAmount = 0;

            while (ticks < shootCooldown)
            {
                ticks += Time.deltaTime;
                cooldownBar.fillAmount = ticks;
                yield return null;
            }

            CompletedFireCooldown();
            _canShoot = true;
        }
    

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Enemy>())
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
    }
}
