using Features.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Player
{
    public class PlayerView : MonoBehaviour
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
            AudioManager.Instance.Play("Player_Shoot");
        }
        public void SoundMovePlay()
        {
            AudioManager.Instance.Play("Player_Move");
        }
        public void SoundMoveStop()
        {
            try { AudioManager.Instance.Stop("Player_Move"); }
            catch { Debug.Log("Audio missing"); }
        
        }
        public void SoundDestroy()
        {
            AudioManager.Instance.Play("Player_Death");
        }
    }
}
