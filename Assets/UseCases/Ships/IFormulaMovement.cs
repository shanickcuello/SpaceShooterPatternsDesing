using UnityEngine;

namespace UseCases.Ships
{
    public interface IFormulaMovement
    {
        Vector3 Get(Transform transform, float speed);
    }
}