﻿using UnityEngine;
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

	const string GOOGLE_ACHIEVEMENT_BRONZE = "CgkI2auPjP8aEAIQBw";
	const string GOOGLE_ACHIEVEMENT_SILVER = "CgkI2auPjP8aEAIQCA";
	const string GOOGLE_ACHIEVEMENT_GOLD = "CgkI2auPjP8aEAIQCQ";
	const string GOOGLE_ACHIEVEMENT_1_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCg";
	const string GOOGLE_ACHIEVEMENT_2_COMPLETED_RUNS = "CgkI2auPjP8aEAIQCw";
	const string GOOGLE_ACHIEVEMENT_3_COMPLETED_RUNS = "CgkI2auPjP8aEAIQDA";

	const string IOS_ACHIEVEMENT_BRONZE = "grp.bronzeaward";
	const string IOS_ACHIEVEMENT_SILVER = "grp.silveraward";
	const string IOS_ACHIEVEMENT_GOLD = "grp.goldaward";
	const string IOS_ACHIEVEMENT_1_COMPLETED_RUNS = "grp.runleftgettingstarted";
	const string IOS_ACHIEVEMENT_2_COMPLETED_RUNS = "grp.runleftgettingold";
	const string IOS_ACHIEVEMENT_3_COMPLETED_RUNS = "grp.runleftlegend";


	private static GoogleGameHandler _instance;

	public static GoogleGameHandler Instance {
		get {
			if (_instance == null) {
				_instance = new GoogleGameHandler ();
			}
			return _instance;
		}
	}

	public void Instantiate ()
	{
		_instance = new GoogleGameHandler ();
#if UNITY_IOS
		GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);


#elif UNITY_ANDROID
        PlayGamesPlatform.Activate();
#endif

		LogIn ();
	}

	private static void LogIn ()
	{
		if (Social.localUser.authenticated)
			return;
		Social.localUser.Authenticate ((bool success) => {
			if (success) {
				//	mStatusText = "Welcome " + Social.localUser.userName;
				Debug.Log("Logged in!");
			} else {
				//	mStatusText = "Authentication failed.";
				Debug.Log("Failed to log in!");
			}
		});
	}

	public void ShowAchievements ()
	{
		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI ();
		} else {
			LogIn ();
		}
	}

	public void ShowLeaderboard ()
	{
		if (Social.localUser.authenticated) {
			long unpublishedScore = HighscoreHandler.Instance.GetUnpublishedScore ();
			if (unpublishedScore > 1) {
				SendToHighscore (unpublishedScore);
			}
#if UNITY_IOS
		GameCenterPlatform.ShowLeaderboardUI (GAME_CENTER_LEADERBOARD_KEY, TimeScope.Week);
#elif UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI(GOOGLE_LEADERBOARD_KEY);
#else
#endif


		} else {
			LogIn ();
		}

	}

	public void SendToHighscore (long score)
	{
		
		if (!Social.localUser.authenticated) {
			HighscoreHandler.Instance.SaveUnpublishedScore (score);
			return;
		}
		#if UNITY_IOS
		
		score /=10;
		Debug.Log ("Sending " + score + " to leaderboard " + GAME_CENTER_LEADERBOARD_KEY);
		Social.ReportScore (score, GAME_CENTER_LEADERBOARD_KEY, (bool success) => {
			// handle success or failure
			if (!success) {
				HighscoreHandler.Instance.SaveUnpublishedScore (score);
			}
		});
		#else
		Social.ReportScore (score, GOOGLE_LEADERBOARD_KEY, (bool success) => {
		// handle success or failure
		if (!success) {
		HighscoreHandler.Instance.SaveUnpublishedScore (score);
		}
		});
		#endif
	}

	public void IncreaseCompletedRuns ()
	{
		Debug.Log ("INCREASE");
		#if UNITY_IOS
		IncrementAchievement (IOS_ACHIEVEMENT_1_COMPLETED_RUNS, 10);
		IncrementAchievement (IOS_ACHIEVEMENT_2_COMPLETED_RUNS, 50);
		IncrementAchievement (IOS_ACHIEVEMENT_3_COMPLETED_RUNS, 100);
		#else
		IncrementAchievement (GOOGLE_ACHIEVEMENT_1_COMPLETED_RUNS, 10);
		IncrementAchievement (GOOGLE_ACHIEVEMENT_2_COMPLETED_RUNS, 50);
		IncrementAchievement (GOOGLE_ACHIEVEMENT_3_COMPLETED_RUNS, 100);
		#endif





	}

	public void AchievementBronze ()
	{
#if UNITY_IOS
		UnlockAchievement (IOS_ACHIEVEMENT_BRONZE);
#else
        UnlockAchievement(GOOGLE_ACHIEVEMENT_BRONZE);
#endif

	}

	public void AchievementSilver ()
	{
#if UNITY_IOS
		UnlockAchievement (IOS_ACHIEVEMENT_SILVER);
#else
        UnlockAchievement(GOOGLE_ACHIEVEMENT_SILVER);
#endif

	}

	public void AchievementGold ()
	{
#if UNITY_IOS
		UnlockAchievement (IOS_ACHIEVEMENT_GOLD);
#else
        UnlockAchievement(GOOGLE_ACHIEVEMENT_GOLD);
#endif


	}

	private void UnlockAchievement (string name)
	{
#if UNITY_IOS
		if (!Social.localUser.authenticated)
			return;
		Social.ReportProgress (name, 100.0f, (bool success) => {
			// handle success or failure
		});
#else
 if (!Social.localUser.authenticated) return;
        Social.ReportProgress(name, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
#endif


	}

	private void IncrementAchievement (string name, double stepsToFull)
	{
	
		double progress = 100 / stepsToFull * RunLeftPlayerPrefs.GetCompletedRuns();
		Debug.Log ("100 / "+stepsToFull+" * " +RunLeftPlayerPrefs.GetCompletedRuns());
		Debug.Log ("Progress of " + name + " " + progress);
		if (!Social.localUser.authenticated)
			return;
#if UNITY_IOS

		Social.ReportProgress (name, progress, (bool success) => {
			// handle success or failure
			if (success) {
				Debug.Log (name + "successfully increamented with " + progress);

			} else {
				Debug.Log ("Failed to increase " + name);
			}
		});
#else

        PlayGamesPlatform.Instance.IncrementAchievement(name, 1, (bool success) =>
        {
            // handle success or failure
        });
#endif

	}
}
