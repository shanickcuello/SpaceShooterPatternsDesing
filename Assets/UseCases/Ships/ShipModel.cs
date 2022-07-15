using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UseCases.Bullets;
using UseCases.Enemys;
using UseCases.Services;
using UseCases.Services.PoolService;

namespace UseCases.Ships
{
    public class ShipModel : MonoBehaviour, IObserver
    {
        [SerializeField] private float shootSpeed;
        public float speed;
        public float shootCooldown;
        public GameObject bulletPrefab;
        public Transform pointToSpawn;
        public IController myController;

        private Camera _myCamera;

        bool _canShoot;
        Coroutine _shootCDCor;
        private Bullet _bullet;
        private IFormulaMovement _linealFormula;
        private IFormulaMovement _sinusoidalFormula;
        private ShipView _view;
        private float _shootTimer;

        private void Awake()
        {
            _myCamera = Camera.main;
            _view = GetComponent<ShipView>();
            myController = new ShipController(_myCamera, this, _view);
        }

        void Start()
        {
            _linealFormula = new LinealFormula();
            _sinusoidalFormula = new SinusoidalFormula();
            _canShoot = true;
            _view.CompletedFireCooldown();
        }

        private void Update()
        {
            myController.OnUpdate();
            if(!_canShoot)
            {
                _shootTimer += Time.deltaTime;
                _view.FillBar(_shootTimer);
                if(_shootTimer >= shootCooldown)
                {
                    _view.CompletedFireCooldown();
                    _shootTimer = 0;
                    _canShoot = true;
                }
            }
        }

        public void Move(float horizontal, float vertical)
        {
            if (horizontal != 0 || vertical != 0)
                _view.SoundMovePlay();
            else
                _view.SoundMoveStop();

            transform.position += (_myCamera.transform.right * horizontal + _myCamera.transform.up * vertical).normalized * speed * Time.deltaTime;
        }
        
        public void Shoot(IFormulaMovement shootingType)
        {
            if(!_canShoot) return;
            var bulletGameobject = bulletPrefab.Reuse(pointToSpawn.position, transform.rotation);
            var bullet = bulletGameobject.GetComponent<Bullet>();
            bullet.SetTimeToDie(shootCooldown).SetSpeed(shootSpeed).SetMovement(shootingType);
            bullet.Subscribe(this);
            _canShoot = false;
        }

        //Funcion para cuando la bala toca un enemigo
        public void OnTargetHit()
        {
            if (_shootCDCor != null)
            {
                StopCoroutine(_shootCDCor);
            }

            _canShoot = true;
            _view.CompletedFireCooldown();
        }
        
        public void LookAt(Vector3 position)
        {
            position.z = transform.position.z;
            transform.right = position - transform.position;
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
}