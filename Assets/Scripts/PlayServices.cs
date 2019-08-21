using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class PlayServices : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(succes => {});

    }

    public static void PosScore(long score, string leaderBoard){

        Social.ReportScore(score, leaderBoard, (succes => {}) );
    }

    public static void ShowLeaderBoard(string leaderBoard){
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderBoard);
    }
    public static long GetPlayerScore(string leaderBoard){
        long score = 0;
        PlayGamesPlatform.Instance.LoadScores(leaderBoard, LeaderboardStart.PlayerCentered, 1, LeaderboardCollection.Public, 
                                              LeaderboardTimeSpan.AllTime, 
                                              (LeaderboardScoreData data) => {score = data.PlayerScore.value;});
        return score;
    }
}
