using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.UI
{
    public class ButtonController : MonoBehaviour
    {
        private LangManager LanguageManager;
        private void Awake()
        {
            LanguageManager = FindObjectOfType<LangManager>();
        }
        public void BtnPlay()
        {
            SceneManager.LoadScene("Game");
        }
        public void BtnCredits()
        {
            SceneManager.LoadScene("Credits");
        }

        public void BtnRestart()
        {
            BtnPlay();
        }
        public void BtnLanguage()
        {
            if (LanguageManager.selectedLanguage == Language.eng)
                LanguageManager.selectedLanguage = Language.spa;
            else
                LanguageManager.selectedLanguage = Language.eng;

            LanguageManager.UpdateLang();
        }
        public void BtnQuit()
        {
            Application.Quit();
        }

        public void BtnBack()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
