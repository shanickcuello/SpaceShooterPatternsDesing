using UnityEngine;

namespace Features.Player
{
    public class PlayerController : IController
    {
        private Camera _camera;
        private PlayerModel _model;
        private PlayerView _view;

        public PlayerController(Camera cam, PlayerModel mod, PlayerView view)
        {
            _camera = cam;
            _model = mod;
            _view = view;
        }

        public void OnUpdate()
        {
            Vector3 lookAtPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _model.LookAt(lookAtPos);

            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            _model.Move(x, y);

            if (!_model.CanShoot) return;
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                _model.ShootLineal();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Shoot();
                _model.ShootSinuous();
            }
        }

        void Shoot()
        {
            _view.SoundShoot();
            _model.ShootCooldown();
        }
    }
}