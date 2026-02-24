using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    private Text textComponent;
    private TextMeshProUGUI tmpComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent = GetComponent<Text>();
        tmpComponent = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    // Update is called once per frame
    public void UpdateText()
    {
        if (LocalizationManager.Instance == null) return;

        string translatedText = LocalizationManager.Instance.GetText(key);
        if (textComponent != null)
            textComponent.text = translatedText;
        else if (tmpComponent != null)
            tmpComponent.text = translatedText;
    
    }
}
