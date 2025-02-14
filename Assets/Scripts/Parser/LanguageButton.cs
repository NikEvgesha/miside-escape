using UnityEngine;
using UnityEngine.UI;
using YG;

public class LanguageButton : MonoBehaviour
{
    // Поле, содержащее выбранный язык.
    [SerializeField] private string language;

    // Свойство для доступа из других скриптов или редактора.
    public string Language { get => language; set => language = value; }

    // Если требуется, можно привязать метод для события OnClick кнопки
    public void OnButtonClick()
    {
        if (LocalizationManager.Instance != null)
        {
            //LocalizationManager.Instance.ChangeLanguage(language);
            YandexGame.SwitchLanguage(char.ToLower(language[0]) + language.Substring(1));
            Debug.Log("Переключаем язык на: " + language);
        }
        else
        {
            Debug.LogWarning("LocalizationManager не найден в сцене!");
        }
    }
}
