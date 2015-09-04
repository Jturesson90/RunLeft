using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;

public class GoogleGameHandler
{
    const string LEADERBOARD_KEY = "CgkI2auPjP8aEAIQBg";

    const string ACHIEVEMENT_BRONZE = "CgkI2auPjP8aEAIQBw";
    const string ACHIEVEMENT_SILVER = "CgkI2auPjP8aEAIQCA";
    const string ACHIEVEMENT_GOLD = "CgkI2auPjP8aEAIQCQ";
    const string ACHIEVEMENT_1_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCg";
    const string ACHIEVEMENT_2_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCw";
    const string ACHIEVEMENT_3_COMPLETED_RUNS = "CgkI2auPjP8aEAIQDA";

    private static GoogleGameHandler _instance;

    public static GoogleGameHandler Instance
    {
        get
        {
            if (_instance == null)
            {

                _instance = new GoogleGameHandler();
            }
            return _instance;
        }
    }
    public void Instantiate()
    {
        _instance = new GoogleGameHandler();
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        //PlayGamesPlatform.DebugLogEnabled = true;
        LogIn();
    }
    private static void LogIn()
    {
        if (Social.localUser.authenticated)
            return;
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                //	mStatusText = "Welcome " + Social.localUser.userName;

            }
            else
            {
                //	mStatusText = "Authentication failed.";

            }
        });
    }

    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            LogIn();
        }
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
        else
        {
            LogIn();
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

    public void IncreaseCompletedRuns()
    {
        IncrementAchievement(ACHIEVEMENT_1_COMPLETED_RUNS);
        IncrementAchievement(ACHIEVEMENT_2_COMPLETED_RUNS);
        IncrementAchievement(ACHIEVEMENT_3_COMPLETED_RUNS);
    }
    public void AchievementBronze()
    {
        UnlockAchievement(ACHIEVEMENT_BRONZE);
    }
    public void AchievementSilver()
    {
        UnlockAchievement(ACHIEVEMENT_SILVER);
    }
    public void AchievementGold()
    {
        UnlockAchievement(ACHIEVEMENT_GOLD);
    }

    private void UnlockAchievement(string name)
    {
        if (!Social.localUser.authenticated) return;
        Social.ReportProgress(name, 100.0f, (bool success) =>
        {
            // handle success or failure
        });

    }
    private void IncrementAchievement(string name)
    {
        if (!Social.localUser.authenticated) return;
        PlayGamesPlatform.Instance.IncrementAchievement(name, 1, (bool success) =>
        {
            // handle success or failure
        });
    }
}
