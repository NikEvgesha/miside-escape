using UnityEngine;
using UnityEngine.UI;

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
            LocalizationManager.Instance.ChangeLanguage(language);
            Debug.Log("����������� ���� ��: " + language);
        }
        else
        {
            Debug.LogWarning("LocalizationManager �� ������ � �����!");
        }
    }
}
