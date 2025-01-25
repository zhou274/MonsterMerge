//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RankManager : MonoBehaviour
{
	public static RankManager instance;

	public string googlePlayLeaderboardID;

	public string gameCenterLeaderboardID;

	private string leaderboardIdToUse;

	protected bool isLogged;

	private static Action<bool> __f__am_cache5;

	private static Action<bool> __f__am_cache6;

	private void Awake()
	{
		RankManager.instance = this;
	}

	private void Start()
	{
        /*
		PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(configuration);
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate(delegate(bool success)
		{
		});
		this.initialize();
		this.signIn();
		*/      
	}

	private void Update()
	{
	}

	public void reportScore(long score)
	{
        /*
		Social.ReportScore(score, this.leaderboardIdToUse, delegate(bool success)
		{
		});
		*/      
	}

	private void signIn()
	{
        /*
		Social.localUser.Authenticate(delegate(bool success)
		{
			if (success)
			{
				this.isLogged = true;
			}
			else
			{
				this.isLogged = false;
			}
		});
		*/
	}

	public void ShowScoreBoard()
	{
        /*
		if (Social.localUser.authenticated)
		{
			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(this.leaderboardIdToUse);
		}
		*/
	}

	public void showLeaderboard()
	{
        /*
		if (Social.localUser.authenticated)
		{
			((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkI87eI7r8MEAIQAQ");
		}
		*/
	}

	private void initialize()
	{
        /*
		PlayGamesPlatform.DebugLogEnabled = true;
		this.leaderboardIdToUse = this.googlePlayLeaderboardID;
		*/      
	}
}
