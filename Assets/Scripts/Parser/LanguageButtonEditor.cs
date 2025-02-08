using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(LanguageButton))]
public class LanguageButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LanguageButton langButton = (LanguageButton)target;
        serializedObject.Update();

        EditorGUILayout.LabelField("��������� ������ ������������ �����", EditorStyles.boldLabel);

        // �������� ����� LocalizationManager � �����
        LocalizationManager manager = FindObjectOfType<LocalizationManager>();
        if (manager != null && manager.LocalizationData != null)
        {
            List<string> languages = manager.LocalizationData.Languages;
            // ������� ������ ���������� �����
            int currentIndex = languages.IndexOf(langButton.Language);
            if (currentIndex < 0) currentIndex = 0;
            // ���������� ���������� ������
            int selectedIndex = EditorGUILayout.Popup("����", currentIndex, languages.ToArray());
            langButton.Language = languages[selectedIndex];
        }
        else
        {
            EditorGUILayout.HelpBox("LocalizationManager ��� LocalizationData �� ������� � �����!", MessageType.Warning);
            // ���� ��� ������, ��������� ����������� ����� �������
            langButton.Language = EditorGUILayout.TextField("����", langButton.Language);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
