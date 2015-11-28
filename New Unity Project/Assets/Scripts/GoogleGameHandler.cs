using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine.SocialPlatforms.GameCenter;

public class GoogleGameHandler
{
    const string GOOGLE_LEADERBOARD_KEY = "CgkI2auPjP8aEAIQBg";
    const string GAME_CENTER_LEADERBOARD_KEY = "grp.run_left_leaderboard";
    private string LEADERBOARD_KEY = GOOGLE_LEADERBOARD_KEY;

    const string GOOGLE_ACHIEVEMENT_BRONZE = "CgkI2auPjP8aEAIQBw";
    const string GOOGLE_ACHIEVEMENT_SILVER = "CgkI2auPjP8aEAIQCA";
    const string GOOGLE_ACHIEVEMENT_GOLD = "CgkI2auPjP8aEAIQCQ";
    const string GOOGLE_ACHIEVEMENT_1_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCg";
    const string GOOGLE_ACHIEVEMENT_2_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCw";
    const string GOOGLE_ACHIEVEMENT_3_COMPLETED_RUNS = "CgkI2auPjP8aEAIQDA";

    const string IOS_ACHIEVEMENT_BRONZE = "CgkI2auPjP8aEAIQBw";
    const string IOS_ACHIEVEMENT_SILVER = "CgkI2auPjP8aEAIQCA";
    const string IOS_ACHIEVEMENT_GOLD = "CgkI2auPjP8aEAIQCQ";
    const string IOS_ACHIEVEMENT_1_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCg";
    const string IOS_ACHIEVEMENT_2_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCw";
    const string IOS_ACHIEVEMENT_3_COMPLETED_RUNS = "CgkI2auPjP8aEAIQDA";


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
        //     PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //    PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        //    PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
#if UNITY_IOS
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
        LEADERBOARD_KEY = GAME_CENTER_LEADERBOARD_KEY;

#elif UNITY_ANDROID
        LEADERBOARD_KEY = GOOGLE_LEADERBOARD_KEY;
        PlayGamesPlatform.Activate();
#else
        LEADERBOARD_KEY = GOOGLE_LEADERBOARD_KEY;
#endif

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
#if UNITY_IOS
            GameCenterPlatform.ShowLeaderboardUI(LEADERBOARD_KEY, TimeScope.Week);
#elif UNITY_ANDROID
            PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD_KEY);
#else
#endif


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

        IncrementAchievement(GOOGLE_ACHIEVEMENT_1_COMPLETED_RUNS);
        IncrementAchievement(GOOGLE_ACHIEVEMENT_2_COMPLETED_RUNS);
        IncrementAchievement(GOOGLE_ACHIEVEMENT_3_COMPLETED_RUNS);

    }
    public void AchievementBronze()
    {
#if UNITY_IOS
        UnlockAchievement(IOS_ACHIEVEMENT_BRONZE);
#else
        UnlockAchievement(GOOGLE_ACHIEVEMENT_BRONZE);
#endif

    }
    public void AchievementSilver()
    {
#if UNITY_IOS
        UnlockAchievement(IOS_ACHIEVEMENT_SILVER);
#else
        UnlockAchievement(GOOGLE_ACHIEVEMENT_SILVER);
#endif

    }
    public void AchievementGold()
    {
#if UNITY_IOS
        UnlockAchievement(IOS_ACHIEVEMENT_GOLD);
#else
        UnlockAchievement(GOOGLE_ACHIEVEMENT_GOLD);
#endif


    }

    private void UnlockAchievement(string name)
    {
#if UNITY_IOS

#else
 if (!Social.localUser.authenticated) return;
        Social.ReportProgress(name, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
#endif


    }
    private void IncrementAchievement(string name)
    {
#if UNITY_IOS
#else
if (!Social.localUser.authenticated) return;
        PlayGamesPlatform.Instance.IncrementAchievement(name, 1, (bool success) =>
        {
            // handle success or failure
        });
#endif

    }
}
