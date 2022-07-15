using TMPro;
using UnityEngine;
using Utils.Flyweight;

namespace Features.UI
{
    public class Config : MonoBehaviour, IObserver
    {
        public Transform mainGameXf;
        ScreenManager _mgr;
        LangManager _languageMgr;
        bool pause;
        public TextMeshProUGUI scoreText;
        [SerializeField]
        float score;
        bool endGame;

        public void Notify(string action)
        {
            switch (action)
            {
                case "AddAsteroidPoint":
                    score += FlyWeight.Asteroid.asteroidPoint;
                    break;
                case "AddAsteroidPartPoint":
                    score += FlyWeight.AsteroidPart.asteroidPartPoint;
                    break;
            }
        }

        void Start()
        {
            pause = false;
            _mgr = GetComponent<ScreenManager>();
            _languageMgr = FindObjectOfType<LangManager>();

            _mgr.Push(new ScreenGO(mainGameXf));
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && !pause)
            {
                pause = true;
                var s = Instantiate(Resources.Load<ScreenPause>("CanvasPause"));
                _languageMgr.UpdateLang();
                _mgr.Push(s);

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !endGame)
            {
                pause = false;
                _mgr.Pop();
            }
        }
        private void LateUpdate()
        {
            UpdateScoreText();
        }
        public void EndGame()
        {        
            Invoke("EndScreen", 0.1f);        
        }
        void EndScreen()
        {
            endGame = true;
            pause = true;
            var s = Instantiate(Resources.Load<ScreenPause>("CanvasEndScreen"));
            _languageMgr.UpdateLang();
            TextMeshProUGUI[] tmps = s.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var t in tmps)
            {
                if (t.name == "ScoreText")
                    t.text = score.ToString();
            }
            _mgr.Push(s);
        }
        private void UpdateScoreText()
        {        
            scoreText.text = score.ToString();
        }
    }
}
