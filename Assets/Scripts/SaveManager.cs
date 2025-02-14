using System.Collections.Generic;
using System.Linq;
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
    private List<long> _scores = new List<long>();
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
            YandexGame.savesData.scores = new int[_topScoreCounts];
        }
        long l_score = (long)score * 1000;
        _scores.Add(l_score);
        _scores.Sort();
        if (_scores.Count > _topScoreCounts)
            _scores.RemoveAt(_scores.Count - 1);
        int i = 0;
        foreach (int el in _scores) {
            YandexGame.savesData.scores[i] = el;
            i++;
        }
        if (score == _scores[0]) {
            YandexGame.NewLeaderboardScores("allTime", l_score);
            YandexGame.NewLeaderboardScores("monthTime", l_score);
        }
        
        YandexGame.SaveProgress();
    }

    public void LoadScore() {
        if (YandexGame.savesData.scores != null)
        {
            foreach (int el in YandexGame.savesData.scores)
            {
                if (el != 0)
                {
                    _scores.Add(el);
                }
            }
        }

            
    }

    public List<long> GetPlayerScores()
    {
        return _scores;
    }
}
