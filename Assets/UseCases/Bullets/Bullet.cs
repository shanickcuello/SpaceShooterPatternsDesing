using System;
using System.Collections.Generic;
using UnityEngine;
using UseCases.Enemys;
using UseCases.Services;
using UseCases.Services.PoolService;
using UseCases.Ships;

namespace UseCases.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable, IBullet, IObservable
    {
        private float _speed;
        private float _timeToDie;
        private IFormulaMovement _formulaMovement;
        List<IObserver> _observers = new List<IObserver>();

        public float Speed
        {
            get => _speed;
            private set => _speed = value;
        }

        protected virtual void LateUpdate()
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
                NotifyToObservers(EActions.TargetHit);
                en.GetShot(); //Le hago damage al enemigo
                gameObject.Release();
            }
        }

        public void OnReuse(){}

        public void OnRelease(){}

        public void Subscribe(IObserver obs)
        {
            if (!_observers.Contains(obs))
                _observers.Add(obs);
        }

        public void Unsubscribe(IObserver obs)
        {
            if (_observers.Contains(obs))
            {
                _observers.Remove(obs);
            }
        }

        public void NotifyToObservers(EActions action)
        {
            foreach (var observer in _observers)
            {
                observer.Notify(action);
            }
        }
    }
}