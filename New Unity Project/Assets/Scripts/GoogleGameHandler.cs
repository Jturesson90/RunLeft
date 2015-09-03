using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System;

public class GoogleGameHandler
{
    const string LEADERBOARD_KEY = "CgkIjOWS5KsQEAIQAQ";
    private static GoogleGameHandler _instance;

    public static GoogleGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {

                _instance = new GoogleGameHandler();
                GooglePlayGames.PlayGamesPlatform.Activate();
                LogIn();
            }
            return _instance;
        }
    }

    private static void LogIn()
    {

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {


            }
            else
            {
                //	mStatusText = "Authentication failed.";

            }
        });
    }

    public void ShowAchievements()
    {

    }
    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            long unpublishedScore = HighscoreHandler.Instance.GetUnpublishedScore();
            if (unpublishedScore > 1)
            {
                SendToHighscore(unpublishedScore);
            }
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD_KEY);
        }

    }

    public void SendToHighscore(long score)
    {
        if (!Social.localUser.authenticated)
        {
            HighscoreHandler.Instance.SaveUnpublishedScore(score);
            return;
        }
        Social.ReportScore(score, LEADERBOARD_KEY, (bool success) =>
        {
            // handle success or failure
            if (!success)
            {
                HighscoreHandler.Instance.SaveUnpublishedScore(score);
            }
        });
    }


}
