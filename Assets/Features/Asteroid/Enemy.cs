using System.Collections.Generic;
using Features.Rounds;
using UnityEngine;
using Utils.Factory;
using Utils.Flyweight;

namespace Features.Asteroid
{
    public class Enemy : MonoBehaviour, IObservable
    {
        public Transform _target;
        public RoundManager manager;
        public bool notify;
        public List<IObserver> _allObserver = new List<IObserver>();

        public Enemy SetNotify(bool count)
        {
            notify = count;
            return this;
        }
        public Enemy SetTarget(Transform target)
        {
            _target = target;
            return this;
        }
        public Enemy SetManager(RoundManager manager)
        {
            this.manager = manager;
            return this;
        }
        public Enemy SetPosition(Vector3 position)
        {
            transform.position = position;
            return this;
        }
        public Enemy SetRotation(Quaternion rot)
        {
            transform.rotation = rot;
            return this;
        }
        void Update()
        {
            if (!_target) return;

            Movement();
        }

        private void Movement()
        {        
            transform.position += GetDirToMove() * GetSpeedToMove() * Time.deltaTime;
        }

        protected virtual float GetSpeedToMove()
        {
            return FlyWeight.AsteroidPart.asteroidPartSpeed;
        }
        Vector3 GetDirToMove()
        {
            Vector3 dir = _target.position - transform.position;
            dir.z = _target.position.z;
            dir.Normalize();

            return dir;
        }
        public virtual void GetShot()
        {        
            NotifyDeath();
            SpawnerEnemy.Instance.ReturnEnemy(this);
        }
        public virtual void NotifyDeath()
        {
            if(notify)
                NotifyToObservers("EnemyDead");

            NotifyToObservers("AddAsteroidPartPoint");
        }
        public static void TurnOn(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
        }
        public static void TurnOff(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }
        public void Subscribe(IObserver obs)
        {
            if (!_allObserver.Contains(obs))
            {
                _allObserver.Add(obs);
            }
        }
        public void Unsubscribe(IObserver obs)
        {
            if (_allObserver.Contains(obs))
            {
                _allObserver.Remove(obs);
            }
        }
        public void NotifyToObservers(string action)
        {
            for (int i = _allObserver.Count - 1; i >= 0; i--)
            {
                _allObserver[i].Notify(action);
            }
        }
    }
}
