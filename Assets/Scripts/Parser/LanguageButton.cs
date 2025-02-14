using UnityEngine;
using UnityEngine.UI;
using YG;

public class LanguageButton : MonoBehaviour
{
    // ����, ���������� ��������� ����.
    [SerializeField] private string language;

    // �������� ��� ������� �� ������ �������� ��� ���������.
    public string Language { get => language; set => language = value; }

    // ���� ���������, ����� ��������� ����� ��� ������� OnClick ������
    public void OnButtonClick()
    {
        if (LocalizationManager.Instance != null)
        {
            //LocalizationManager.Instance.ChangeLanguage(language);
            YandexGame.SwitchLanguage(char.ToLower(language[0]) + language.Substring(1));
            Debug.Log("����������� ���� ��: " + language);
        }
        else
        {
            Debug.LogWarning("LocalizationManager �� ������ � �����!");
        }
    }
}
