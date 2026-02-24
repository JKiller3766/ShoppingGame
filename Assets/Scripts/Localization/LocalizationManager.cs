using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language
    {
        English,  // 0
        Spanish,  // 1
        Catalan   // 2
    }

    public Language currentLanguage = Language.Spanish;

    private Dictionary<string, string[]> translations = new Dictionary<string, string[]>();

    private const string LanguagePrefKey = "SelectedLanguage";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
            LoadCSV();
        }
        else
        {
            Destroy(gameObject);
        }
    
    }

    void LoadCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("Files/languages");
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;
            string[] parts = line.Split(';');
            
            if (parts.Length >= 4)
            {
                string key = parts[0].Trim();
                string[] values = new string[3];
                values[0] = parts[1].Trim(); // English
                values[1] = parts[2].Trim(); // Spanish
                values[2] = parts[3].Trim(); // Catalan
                
                translations[key] = values;
            }
        }
    }

    public void SetLanguage(Language newLanguage)
    {
        currentLanguage = newLanguage;
        PlayerPrefs.SetInt(LanguagePrefKey, (int)newLanguage);
        UpdateAllTexts();
    }

    public string GetText(string key)
    {
        if (translations.ContainsKey(key))
        {
            return translations[key][(int)currentLanguage];
        }
        return key;
    }

    void UpdateAllTexts()
    {
        LocalizedText[] allTexts = FindObjectsOfType<LocalizedText>();
        foreach (LocalizedText text in allTexts)
        {
            text.UpdateText();
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
