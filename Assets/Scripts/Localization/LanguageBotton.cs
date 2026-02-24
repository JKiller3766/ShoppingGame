using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageBotton : MonoBehaviour
{
    public Button toggleButton;
    public TextMeshProUGUI buttonText;

    private LocalizationManager localizationManager;
    private LocalizationManager.Language[] languages = new LocalizationManager.Language[]
    {
        LocalizationManager.Language.Spanish,
        LocalizationManager.Language.English,
        LocalizationManager.Language.Catalan
    };
    
    private int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        localizationManager = LocalizationManager.Instance;
        if (toggleButton == null)
            toggleButton = GetComponent<Button>();
        
        if (toggleButton != null)
            toggleButton.onClick.AddListener(ChangeLanguage);

        currentIndex = System.Array.IndexOf(languages, localizationManager.currentLanguage);
        if (currentIndex < 0) currentIndex = 0;
    }

    // Update is called once per frame
    public void ChangeLanguage()
    {
        if (localizationManager == null) return;
        currentIndex = (currentIndex + 1) % languages.Length;
        LocalizationManager.Language nextLanguage = languages[currentIndex];
        localizationManager.SetLanguage(nextLanguage);
    }

    public void SetLanguage(int languageIndex)
    {
        if (languageIndex >= 0 && languageIndex < languages.Length)
        {
            currentIndex = languageIndex;
            localizationManager.SetLanguage(languages[currentIndex]);
        }
    }
}
