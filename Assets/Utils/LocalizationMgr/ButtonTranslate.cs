using TMPro;
using UnityEngine;

namespace Utils.LocalizationMgr
{
    public class ButtonTranslate : MonoBehaviour
    {
        public string ID;

        public LangManager manager;

        public TextMeshProUGUI myView;

        string _myText = "";

        void Awake()
        {
            manager = FindObjectOfType<LangManager>();
            if (manager == null)
                Debug.LogError("Manager not found");
            else
                manager.OnUpdate += ChangeLang;

            _myText = myView.GetComponent<TextMeshProUGUI>().text;
        }

        void ChangeLang()
        {
            myView.text = manager.GetTranslate(ID);
        }
    }
}
