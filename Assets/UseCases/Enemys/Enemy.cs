﻿using UnityEngine;
using UseCases.Rounds;

namespace UseCases.Enemys
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public Transform target;

        public RoundManager manager;


        // Update is called once per frame
        void Update()
        {
            if (!target) return; //Si no hay target no hago nada

            //Movimiento
            Vector3 dir = target.position - transform.position;
            dir.z = target.position.z;
            dir.Normalize();
            transform.position += dir * speed * Time.deltaTime;
        }

        public void GetShot()
        {
            manager.EnemyDead(); //Le digo al manager que mori
            Destroy(gameObject); //Me destruyo
        }
    }
}