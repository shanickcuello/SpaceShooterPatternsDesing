using UnityEngine;

namespace Features.UI
{
    public class ScreenMessage : MonoBehaviour, IScreen
    {
        bool _active;

        public UnityEngine.UI.Text text;

        string _result;

        #region Botones

        public void OnOk()
        {
            if (!_active) return;

            _result = "Ok";

            ScreenManager.instance.Pop();
        }

        public void OnCancel()
        {
            if (!_active) return;

            _result = "Cancel";

            ScreenManager.instance.Pop();
        }

        #endregion

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
