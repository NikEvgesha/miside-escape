using UnityEngine;
using System.Collections.Generic;
using YG;
using static Unity.VisualScripting.Member;

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

    private void OnEnable()
    {
        YandexGame.SwitchLangEvent += OnSwitchLanguage;
    }

    private void OnDisable()
    {
        YandexGame.SwitchLangEvent -= OnSwitchLanguage;
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

    private void OnSwitchLanguage(string langCode)
    {
        ChangeLanguage(char.ToUpper(langCode[0]) + langCode.Substring(1));
    }
}
