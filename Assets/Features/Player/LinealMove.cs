using UnityEngine;

namespace Features.Player
{
    public class LinealMove : IMove
    {
        private float _speed;
        private Transform _owner;
        public LinealMove(Transform owner, float speed)
        {
            _owner = owner;
            _speed = speed;
        }
        public void Move()
        {
            _owner.position += _owner.right * _speed * Time.deltaTime;
        }
    }
}




