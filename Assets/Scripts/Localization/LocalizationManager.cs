using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField] private Languages.DefaultLanguages _defaultLanguage = Languages.DefaultLanguages.en;

    private Dictionary<string, string> texts = new Dictionary<string, string>();

    public static UnityEvent OnLanguageChange = new UnityEvent();

    public string CurrentLanguage { get; private set; }
    /*
        [DllImport("__Internal")]
        private static extern void GetCurrentLanguageExtern();*/

    public void GetCurrentLanguageExternHandler(string language)
    {
        if (language == null)
            CurrentLanguage = _defaultLanguage.ToString();
        else
            CurrentLanguage = language;
    }

    public void SetRuLanguage()
    {
        Languages.DefaultLanguages newLanguage = Languages.DefaultLanguages.ru;

        SetLanguage(newLanguage);
    }

    public void SetEnLanguage()
    {
        Languages.DefaultLanguages newLanguage = Languages.DefaultLanguages.en;

        SetLanguage(newLanguage);
    }

    public void SetLanguage(Languages.DefaultLanguages newLanguage)
    {
        if (newLanguage.ToString() != CurrentLanguage)
        {
            CurrentLanguage = newLanguage.ToString();
            LoadLocalization();
            OnLanguageChange.Invoke();
        }
        else
        {
            throw new Exception($"Localization Error!: \"{newLanguage}\" already enabled!");
        }
    }

    public string GetText(string key)
    {
        if (!texts.ContainsKey(key))
            throw new Exception($"Localization Error!: \"{key}\" not found in keys!");

        return texts[key];
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

        CurrentLanguage = _defaultLanguage.ToString();

        LoadLocalization();
        //GetCurrentLanguageExtern();
    }

    private void LoadLocalization()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Localization/" + CurrentLanguage);

        if (textAsset == null)
            throw new Exception($"Localization Error!: \"{CurrentLanguage}.json\" not found in \"Localization/\" folder!");

        texts = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text);

        Debug.Log("Localization loaded successfully!");
    }
}