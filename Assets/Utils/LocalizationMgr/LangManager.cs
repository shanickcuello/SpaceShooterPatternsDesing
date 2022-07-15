using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Utils.LocalizationMgr;

public enum Language
{
    eng,
    spa
}

public class LangManager : MonoBehaviour
{
    static LangManager _instance;
    public static LangManager Instance
    {
        get { return _instance; }
    }

    int indexScene;

    public Language selectedLanguage;

    public Dictionary<Language, Dictionary<string, string>> LanguageManager;

    public string externalURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRg8vouux1g50PsA0oj3qRF4s6KgQDv5jmWtrxCRUqLwnRBuCXT3MG44V_3rP2U0xzFUZSBzQum9HdX/pub?output=csv";

    public event Action OnUpdate = delegate { };

    void Awake()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(DownloadCSV(externalURL));        
        if(_instance == null)
        {
            GameObject.DontDestroyOnLoad(this);
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if(indexScene != SceneManager.GetActiveScene().buildIndex)
        {
            indexScene = SceneManager.GetActiveScene().buildIndex;
            OnUpdate();
        }
    }

    public void UpdateLang()
    {
        OnUpdate();
    }
    
    public string GetTranslate(string _id)
    {
        if (!LanguageManager[selectedLanguage].ContainsKey(_id))
            return "Key not found";
        else
            return LanguageManager[selectedLanguage][_id];
    }

    private IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();
        LanguageManager = CsvParser.loadCodexFromString("www", www.downloadHandler.text);
        OnUpdate();
    }



}
