using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UseCases.Ships
{
    public class ShipView : MonoBehaviour
    {
        public Image cooldownBar;

        public void CompletedFireCooldown()
        {
            cooldownBar.color = Color.green;
            cooldownBar.fillAmount = 1;
        }
        public void FillBar(float amount)
        {
            cooldownBar.color = Color.red;
            cooldownBar.fillAmount = amount;
        }
        
        public void SoundShoot()
        {
            
        }
        public void SoundMovePlay()
        {
        }
        public void SoundMoveStop()
        {
        }
        public void SoundDestroy()
        {
        }
    }
}