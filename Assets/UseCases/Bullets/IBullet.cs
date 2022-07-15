using System;
using UseCases.Ships;

namespace UseCases.Bullets
{
    public interface IBullet
    {
        Bullet SetTimeToDie(float timeToDie);
        void SetMovement(IFormulaMovement formulaMovement);
    }
}