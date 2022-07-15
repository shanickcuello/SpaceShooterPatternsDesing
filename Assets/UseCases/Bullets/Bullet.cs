using System;
using UnityEngine;
using UseCases.Enemys;
using UseCases.Services.PoolService;
using UseCases.Ships;

namespace UseCases.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable, IBullet
    {
        private float _speed;
        private float _timeToDie;
        private IFormulaMovement _formulaMovement;
        public event Action<Bullet> OnHit;
        
        public float Speed
        {
            get => _speed;
            private set => _speed = value;
        }

        protected virtual void Update()
        {
            Move();
            TimeToDie();
        }

        private void Move()
        {
            transform.position += _formulaMovement.Get(transform, _speed);
        }

        public void SetMovement(IFormulaMovement formulaMovement)
        {
            _formulaMovement = formulaMovement;
        }


        public Bullet SetTimeToDie(float timeToDie)
        {
            _timeToDie = timeToDie;
            return this;
        }

        public Bullet SetSpeed(float speed)
        {
            Speed = speed;
            return this;
        }

        void TimeToDie()
        {
            _timeToDie -= Time.deltaTime;

            if (_timeToDie <= 0)
            {
                gameObject.Release();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy en = collision.GetComponent<Enemy>();

            if (en)
            {
                OnHit?.Invoke(this);

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
