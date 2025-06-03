using MirraGames.SDK;
using MirraGames.SDK.Common;
using System;
using UnityEngine;

public class MirraSDKLeaderboardProvider : LeaderboardProvider
{
    public override void LoadLB(LBName LBTag, int topAmount, bool includePlayer, Action<LBData> onLoad)
    {
        MirraSDK.Achievements.GetLeaderboard(
            boardId: LBTag.ToString(),
            onLeaderboard: (scoreTable) => {
                LBData data = new();
                data.LBName = LBTag;
                data.Records = new();
                //List<LBRecord> records = new();
                for (int i = 0; i < scoreTable.players.Length; i++)
                {
                    PlayerScore playerScore = scoreTable.players[i];
                    LBRecord rec = new LBRecord();
                    rec.Name = playerScore.displayName;
                    rec.Rank = playerScore.position;
                    rec.Score = playerScore.score;
                    rec.URL = playerScore.profilePictureUrl;
                    data.Records.Add(rec);
                }

                Debug.Log("onScoreTableResolve : " + scoreTable.players.Length);
                onLoad(data);
            }
        );
    }


    public override void SaveScore(string LBName, int score)
    {
        MirraSDK.Achievements.SetScore(
            boardId: LBName,
            score: score);
        Debug.Log(LBName + " set score " + score);
    }
}