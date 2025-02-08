using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationData))]
public class LocalizationDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LocalizationData localization = (LocalizationData)target;

        if (localization.Languages.Count == 0 || localization.Entries.Count == 0)
        {
            EditorGUILayout.HelpBox("Нет данных. Сначала загрузите Google Таблицу!", MessageType.Warning);
            return;
        }

        // Заголовок
        EditorGUILayout.LabelField("Таблица локализации", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Отображаем заголовки (Ключ + языки)
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ключ", EditorStyles.boldLabel, GUILayout.Width(150));

        foreach (var lang in localization.Languages)
        {
            EditorGUILayout.LabelField(lang, EditorStyles.boldLabel, GUILayout.Width(100));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(2);

        // Отображаем переводы построчно
        foreach (var entry in localization.Entries)
        {
            EditorGUILayout.BeginHorizontal();

            // Показываем ключ
            EditorGUILayout.LabelField(entry.Key, GUILayout.Width(150));

            // Показываем переводы
            for (int i = 0; i < localization.Languages.Count; i++)
            {
                if (i < entry.Translations.Count)
                {
                    entry.Translations[i] = EditorGUILayout.TextField(entry.Translations[i], GUILayout.Width(100));
                }
                else
                {
                    EditorGUILayout.LabelField("-", GUILayout.Width(100));
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(10);
        if (GUILayout.Button("Сохранить изменения"))
        {
            EditorUtility.SetDirty(localization);
        }
    }
}
