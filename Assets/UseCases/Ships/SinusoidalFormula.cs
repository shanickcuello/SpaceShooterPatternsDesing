using UnityEngine;

namespace UseCases.Ships
{
    public struct SinusoidalFormula : IFormulaMovement
    {
        public Vector3 Get(Transform transform, float speed)
        {
            Vector3 myUp = transform.up * Mathf.Sin(Time.time * speed * 5) * Time.deltaTime * 2;

            Vector3 myRight = transform.right * speed * Time.deltaTime;

            return myUp + myRight;
        }
    }
}