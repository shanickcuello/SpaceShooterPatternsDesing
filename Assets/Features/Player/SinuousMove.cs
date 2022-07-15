using UnityEngine;

namespace Features.Player
{
    public class SinuousMove : IMove
    {
        float _speed;
        Transform _owner;

        public SinuousMove(Transform owner, float speed)
        {
            _owner = owner;
            _speed = speed;
        }

        public void Move()
        {
            Vector3 up = _owner.up * Mathf.Sin(Time.time * _speed * 2) * Time.deltaTime * 2;
            Vector3 right = _owner.right * _speed * Time.deltaTime;
            _owner.position += up + right;
        }
    }
}
