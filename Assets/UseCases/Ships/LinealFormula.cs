using UnityEngine;

namespace UseCases.Ships
{
    public struct LinealFormula : IFormulaMovement
    {
        public Vector3 Get(Transform transform, float speed)
        {
            return transform.right * (speed * Time.deltaTime);
        }
    }
}