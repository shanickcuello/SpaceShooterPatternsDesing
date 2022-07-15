using Features.Asteroid;
using UnityEngine;
using Utils.Factory;

namespace Features.Player
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerModel : MonoBehaviour, IObserver
    {
        public float speed;
        public float shootCooldown;
        public Transform pointToSpawn;

        public bool CanShoot { get; set; }

        public IController myController;
        private PlayerView myView;
        private Camera _camera;

        private float shootTimer;
        private void Awake()
        {
            GetDependencies();
        }

        private void GetDependencies()
        {
            _camera = Camera.main;
            CanShoot = true;
            myView = GetComponent<PlayerView>();
        }

        void Start()
        {
            InitializeController();
        }

        private void InitializeController()
        {
            myController = new PlayerController(_camera, this, myView);
        }

        void Update()
        {
            UpdateController();
            CalculateShoot();
        }

        private void UpdateController()
        {
            myController.OnUpdate();
        }

        private void CalculateShoot()
        {
            if (CanShoot) return;
            shootTimer += Time.deltaTime;
            myView.FillBar(shootTimer);
            if (!(shootTimer >= shootCooldown)) return;
            myView.CompletedFireCooldown();
            CanShoot = true;
        }

        public void LookAt(Vector3 position)
        {
            position.z = transform.position.z;
            transform.right = position - transform.position;
        }
        public void Move(float horizontal, float vertical)
        {
            if (horizontal != 0 || vertical != 0)
                myView.SoundMovePlay();
            else
                myView.SoundMoveStop();

            transform.position += (_camera.transform.right * horizontal + _camera.transform.up * vertical).normalized * speed * Time.deltaTime;
        }
        public void OnDisable()
        {
            myView.SoundMoveStop();
        }
        public void ShootSinuous()
        {        
            var bullet = SpawnerBullet.Instance.GetBulletFromPool(gameObject.transform, shootCooldown).SetMovement();
            IMove sinuousMove = new SinuousMove(bullet.transform, bullet.speed); 
            bullet.SetMovement(sinuousMove); 
            bullet.Subscribe(this);
        }
        public void ShootLineal()
        {
            var bullet = SpawnerBullet.Instance.GetBulletFromPool(gameObject.transform, shootCooldown).SetMovement();
            IMove linealMove = new LinealMove(bullet.transform, bullet.speed); 
            bullet.SetMovement(linealMove); 
            bullet.Subscribe(this);
        }
        public void ShootCooldown()
        {
            CanShoot = false;
            shootTimer = 0;
        }
        
        public void TargetHit()
        {
            CanShoot = true;
            myView.CompletedFireCooldown();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.GetComponent<Enemy>()) return;
            myView.SoundDestroy();
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        
        public void Notify(string action)
        {
            if (action == "TargetHit")
            {
                TargetHit();
            }
        }
    }
}
