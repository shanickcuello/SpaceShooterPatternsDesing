using System;
using UseCases.Ships;

namespace UseCases.Bullets
{
    public interface IBullet
    {
        event Action<Bullet> OnHit;
        Bullet SetTimeToDie(float timeToDie);
        void SetMovement(IFormulaMovement formulaMovement);
    }
}