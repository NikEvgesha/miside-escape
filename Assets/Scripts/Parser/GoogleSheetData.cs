using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

[CreateAssetMenu(fileName = "GoogleSheetData", menuName = "Google Sheets/Data")]
public class GoogleSheetData : ScriptableObject
{
    [SerializeField] private string sheetId;
    [SerializeField] private string sheetName;
    [SerializeField] private LocalizationData localizationData; // Ссылка на объект локализации

    public void DownloadAndParseSheet()
    {
        if (string.IsNullOrEmpty(sheetId) || string.IsNullOrEmpty(sheetName))
        {
            Debug.LogError("Sheet ID or Sheet Name is empty!");
            return;
        }

        string url = $"https://docs.google.com/spreadsheets/d/{sheetId}/gviz/tq?tqx=out:csv&sheet={sheetName}";

        using (WebClient client = new WebClient())
        {
            string csvData = client.DownloadString(url);
            ParseCSV(csvData);
        }
    }

    private void ParseCSV(string csv)
    {
        List<string[]> data = new List<string[]>();
        using (StringReader reader = new StringReader(csv))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                data.Add(line.Split(',')); // Разбиваем по запятым
            }
        }

        if (localizationData != null)
        {
            localizationData.SetData(data);
            Debug.Log("Localization data updated!");
        }
    }
}
