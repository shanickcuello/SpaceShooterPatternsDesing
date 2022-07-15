using UnityEngine;

namespace Features.UI
{
    public class ScreenPause : MonoBehaviour, IScreen
    {
        bool _active;

        public UnityEngine.UI.Text text;

        string _result;
        
        public void Activate()
        {
            _active = true;
        }

        public void Deactivate()
        {
            _active = false;
        }

        public string Free()
        {
            Destroy(gameObject);
            return _result;
        }
    }
}
