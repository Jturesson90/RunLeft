using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using System;

public class GoogleGameHandler
{
	const string LEADERBOARD_KEY = "CgkIjOWS5KsQEAIQAQ";
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
		//PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();
		LogIn ();
	}
	private static void LogIn ()
	{
		if (Social.localUser.authenticated)
			return;
		Social.localUser.Authenticate ((bool success) => {
			if (success) {
				//	mStatusText = "Welcome " + Social.localUser.userName;
				
			} else {
				//	mStatusText = "Authentication failed.";
				
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
			PlayGamesPlatform.Instance.ShowLeaderboardUI (LEADERBOARD_KEY);
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
		Social.ReportScore (score, LEADERBOARD_KEY, (bool success) =>
		{
			// handle success or failure
			if (!success) {
				HighscoreHandler.Instance.SaveUnpublishedScore (score);
			}
		});
	}


}
