using System.Collections.Generic;
using Features.Asteroid;
using Features.Player;
using UnityEngine;
using Utils.Factory;

namespace Features.Bullet
{
    public class Bullet : MonoBehaviour, IObservable
    {
        public float speed;

        public float timeToDie;

        List<IObserver> _observers = new List<IObserver>();
        IMove myCurrentMove;

        public Bullet SetMovement(IMove movement = null)
        {
            myCurrentMove = movement;
            return this;
        }

        void Update()
        {
            if (myCurrentMove != null)
                myCurrentMove.Move();


            timeToDie -= Time.deltaTime;

            if (timeToDie <= 0)
            {
                SpawnerBullet.Instance.ReturnBullet(this); 
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy en = collision.GetComponent<Enemy>();

            if (en)
            {
                NotifyToObservers("TargetHit");

                en.GetShot(); 

                SpawnerBullet.Instance.ReturnBullet(this); 
            }
        }

        private void Reset()
        {
            myCurrentMove = null;
            timeToDie = 0;
        }

        public static void TurnOn(Bullet bullet)
        {
            bullet.Reset();
            bullet.gameObject.SetActive(true);
        }

        public static void TurnOff(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

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

        public void NotifyToObservers(string action)
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].Notify(action);
            }
        }
    }
}