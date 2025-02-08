using UnityEngine;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField] private LocalizationData localizationData;
    [SerializeField] private string currentLanguage;

    public LocalizationData LocalizationData => localizationData;  // Публичное свойство для доступа
    public string CurrentLanguage => currentLanguage;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeLanguage(string newLanguage)
    {
        if (localizationData != null && localizationData.Languages.Contains(newLanguage))
        {
            currentLanguage = newLanguage;
            foreach (LocalizedText text in FindObjectsOfType<LocalizedText>())
            {
                text.SetLanguage(newLanguage);
            }
        }
    }

    private void OnValidate()
    {
        if (localizationData != null && !localizationData.Languages.Contains(currentLanguage))
        {
            currentLanguage = localizationData.Languages.Count > 0 ? localizationData.Languages[0] : "";
        }
    }
}
