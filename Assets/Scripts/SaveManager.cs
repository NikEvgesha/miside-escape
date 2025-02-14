using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using YG;

public class SaveManager : MonoBehaviour
{
    /*
     Топ-5 личных рекордов времени
     Топ-5 по всем игрокам + место игрока
     Топ месяца
     */
    [SerializeField] private bool _removeSaveOnStart;
    private static SaveManager _instance;
    private List<float> _scores = new List<float>();
    private int _topScoreCounts = 5;

    public int TopScoreCounts { get { return _topScoreCounts; } }


    public static SaveManager Instance {  get { return _instance; } }

    private void Awake()
    {
        if (_removeSaveOnStart)
        {
            YandexGame.ResetSaveProgress();
            PlayerPrefs.DeleteAll();
        }
        _instance = this;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadScore();
        }
    }

    private void OnDisable()
    {
        
    }

    public void SaveScore(float score) {
       
        if (YandexGame.savesData.scores == null)
        {
            YandexGame.savesData.scores = new float[_topScoreCounts];
        }

        //long l_score = (long)score * 1000;
        _scores.Add(score);
        _scores.Sort();
        if (_scores.Count > _topScoreCounts)
            _scores.RemoveAt(_scores.Count - 1);
        int i = 0;
        foreach (float el in _scores) {
            YandexGame.savesData.scores[i] = el;
            Debug.Log("YG save: "+YandexGame.savesData.scores[i]);
            i++;
        }
        //Debug.Log("score: " + score);
        if (Mathf.Abs(score - _scores[0]) <= 1e-06) {
            Debug.Log("score to LB: " + score);
            YandexGame.NewLBScoreTimeConvert("allTime", score);
            YandexGame.NewLBScoreTimeConvert("monthTime", score);
        }
        
        YandexGame.SaveProgress();
    }

    public void LoadScore() {
        if (YandexGame.savesData.scores != null)
        {
            foreach (float el in YandexGame.savesData.scores)
            {
                if (el != 0)
                {
                    _scores.Add(el);
                }
            }
        }

            
    }

    public List<float> GetPlayerScores()
    {
        return _scores;
    }
}
