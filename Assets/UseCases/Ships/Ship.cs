using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UseCases.Bullets;
using UseCases.Enemys;
using UseCases.Services;
using UseCases.Services.PoolService;

namespace UseCases.Ships
{
    public class Ship : MonoBehaviour, IObserver
    {
        [SerializeField] private float shootSpeed;
        public float speed;
        public float shootCooldown;
        public GameObject bulletPrefab;
        public Transform pointToSpawn;
        public Image cooldownBar;

        Camera _myCamera;
        bool _canShoot;
        Coroutine _shootCDCor;
        private Bullet _bullet;
        private IFormulaMovement _linealFormula;
        private IFormulaMovement _sinusoidalFormula;


        // Start is called before the first frame update
        void Start()
        {
            _linealFormula = new LinealFormula();
            _sinusoidalFormula = new SinusoidalFormula();
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

            transform.position +=
                (_myCamera.transform.right * Input.GetAxisRaw("Horizontal") +
                 _myCamera.transform.up * Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime;

            //Disparo
            if (Input.GetMouseButtonDown(0) && _canShoot)
            {
                Shoot(_linealFormula);
            }
            else if (Input.GetMouseButtonDown(1) && _canShoot)
            {
                Shoot(_sinusoidalFormula);
            }
        }

        void Shoot(IFormulaMovement shootingType)
        {
            var bulletGameobject = bulletPrefab.Reuse(pointToSpawn.position, transform.rotation);
            var bullet = bulletGameobject.GetComponent<Bullet>();
            bullet.Subscribe(this);
            bullet.SetTimeToDie(shootCooldown).SetSpeed(shootSpeed).SetMovement(shootingType);
            _shootCDCor = StartCoroutine(ShootCooldown()); //Corrutina del cooldown para volver a disparar
        }

        //Funcion para cuando la bala toca un enemigo
        public void OnTargetHit()
        {
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

        public void Notify(EActions action)
        {
            if (action == EActions.TargetHit) OnTargetHit();
        }
    }
    
    public struct LinealFormula : IFormulaMovement
    {
        public Vector3 Get(Transform transform, float speed)
        {
            return transform.right * (speed * Time.deltaTime);
        }
    }
    
    public struct SinusoidalFormula : IFormulaMovement
    {
        public Vector3 Get(Transform transform, float speed)
        {
            Vector3 myUp = transform.up * Mathf.Sin(Time.time * speed * 5) * Time.deltaTime * 2;

            Vector3 myRight = transform.right * speed * Time.deltaTime;

            return myUp + myRight;
        }
    }

    public interface IFormulaMovement
    {
        Vector3 Get(Transform transform, float speed);
    }
}