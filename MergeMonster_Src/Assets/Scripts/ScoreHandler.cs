using System;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
	public int score;

	public int secondaryScore;

	public int lifetimeScore;

	public int highScore;

	public int specialPoints;

	public int numberOfGames;

	public string fristUseDustbin;

	public string fristUseExplodeSkill;

	public string _100coins;

	public string doublCoinsState;

	public string voiceState = "on";

	public int numberOfTime;

	private string voiceStatePlayerName = "voiceState";

	private string _100coinsPlayerPerfsName = "100coins";

	private string fristUseExplodeSkillPlayerPrefsName = "first_use_explode";

	private string highScorePlayerPrefsName = "HIGHSCORE";

	private string specialPointsPlayerPrefsName = "SPECIALPOINTS";

	private string numberOfGamesPlayerPrefsName = "NUMBEROFGAMES";

	private string lifeTimeScorePlayerPrefsName = "LIFETIMESCORE";

	private string adsStatePlayerPerfsName = "ADS_STATE";

	private string doubleCoinsPlayerPersName = "doubleCoinsState";

	private string firstUseDustbinPlayerPersName = "fristUseDustbin";

	private string PlayTimesPlayerName = "playTimes";

	private string FristUseShovelPrefix = "fristUseShovel";

	public string fristUseShovel;

	public static ScoreHandler instance;

	private void Awake()
	{
		ScoreHandler.instance = this;
		this.loadHighScoreFromPrefs();
		this.loadLifeTimeScoreFromPrefs();
		this.loadSpecialPointsFromPlayerPrefs();
		this.loadNumberOfGamesFromPlayerPrefs();
		this.LoadDoubleCoinsState();
		this.LoadNumberOfTime();
	}

	public void SaveFristUseShovel()
	{
		PlayerPrefs.SetString(this.FristUseShovelPrefix, "true");
	}

	public void LoadFristUseShovelState()
	{
		this.fristUseShovel = PlayerPrefs.GetString(this.FristUseShovelPrefix);
	}

	public void SaveNumberOfTime()
	{
		this.numberOfTime++;
		PlayerPrefs.SetInt(this.PlayTimesPlayerName, this.numberOfTime);
	}

	public void LoadNumberOfTime()
	{
		this.numberOfTime = PlayerPrefs.GetInt(this.PlayTimesPlayerName);
	}

	public void increaseSpecialPoints(int valueToAdd)
	{
		this.specialPoints += valueToAdd;
		this.saveSpecialPointsToPlayerPrefs();
		if (InGameGUI.instance != null)
		{
			InGameGUI.instance.RefreshCoins();
		}
	}

	public void increaseScore(int valueToAdd)
	{
		this.score += valueToAdd;
		this.lifetimeScore += valueToAdd;
		this.saveLifeTimeScoreToPrefs();
		InGameGUI.instance.RefreshScore();
		if (this.score > this.highScore)
		{
			this.highScore = this.score;
			this.saveHighScoreToPrefs();
			InGameGUI.instance.RefreshHighScore();
		}
	}

	public void SaveVoiceStateOn()
	{
		PlayerPrefs.SetString(this.voiceStatePlayerName, "on");
	}

	public void SaveVoiceStateOff()
	{
		PlayerPrefs.SetString(this.voiceStatePlayerName, "off");
	}

	public void LoadVoiceState()
	{
		this.voiceState = PlayerPrefs.GetString(this.voiceStatePlayerName);
	}

	public void increaseSecondaryScore(int valueToAdd)
	{
		this.secondaryScore += valueToAdd;
	}

	public void incrementNumberOfGames()
	{
		this.numberOfGames++;
		PlayerPrefs.SetInt(this.numberOfGamesPlayerPrefsName, this.numberOfGames);
	}

	public void loadNumberOfGamesFromPlayerPrefs()
	{
		this.numberOfGames = PlayerPrefs.GetInt(this.numberOfGamesPlayerPrefsName, 0);
	}

	public void removeSpecialPoints(int specialPointsToRemove)
	{
		this.specialPoints -= specialPointsToRemove;
		InGameGUI.instance.RefreshCoins();
	}

	public void saveSpecialPointsToPlayerPrefs()
	{
		PlayerPrefs.SetInt(this.specialPointsPlayerPrefsName, this.specialPoints);
	}

	public void loadSpecialPointsFromPlayerPrefs()
	{
		this.specialPoints = PlayerPrefs.GetInt(this.specialPointsPlayerPrefsName, 0);
	}

	public void saveHighScoreToPrefs()
	{
		PlayerPrefs.SetInt(this.highScorePlayerPrefsName, this.highScore);
	}

	private void saveLifeTimeScoreToPrefs()
	{
		PlayerPrefs.SetInt(this.lifeTimeScorePlayerPrefsName, this.lifetimeScore);
	}

	private void loadLifeTimeScoreFromPrefs()
	{
		this.lifetimeScore = PlayerPrefs.GetInt(this.lifeTimeScorePlayerPrefsName, 0);
	}

	public void loadHighScoreFromPrefs()
	{
		this.highScore = PlayerPrefs.GetInt(this.highScorePlayerPrefsName, 0);
	}

	public void LoadFirstUseDustbin()
	{
		this.fristUseDustbin = PlayerPrefs.GetString(this.firstUseDustbinPlayerPersName);
	}

	public void SaveAdsState()
	{
		PlayerPrefs.SetString(this.adsStatePlayerPerfsName, "true");
	}

	public void SaveUseDustbinState()
	{
		PlayerPrefs.SetString(this.firstUseDustbinPlayerPersName, "true");
	}

	public void SaveDoubleCoinsState()
	{
		PlayerPrefs.SetString(this.doubleCoinsPlayerPersName, "true");
	}

	public void Save100CoinsState()
	{
		PlayerPrefs.SetString(this._100coinsPlayerPerfsName, "true");
	}

	public void Load100CoinsState()
	{
		this._100coins = PlayerPrefs.GetString(this._100coinsPlayerPerfsName);
	}

	public void LoadDoubleCoinsState()
	{
		this.doublCoinsState = PlayerPrefs.GetString(this.doubleCoinsPlayerPersName);
	}

	public void reset()
	{
		this.score = 0;
		this.secondaryScore = 0;
		this._100coins = string.Empty;
	}

	public void AccountData()
	{
		this.saveSpecialPointsToPlayerPrefs();
	}

	public void LoadFristUseExplodeSkil()
	{
		this.fristUseExplodeSkill = PlayerPrefs.GetString(this.fristUseExplodeSkillPlayerPrefsName);
	}

	public void SaveFristExplodeSkillState()
	{
		PlayerPrefs.SetString(this.fristUseExplodeSkillPlayerPrefsName, "true");
	}
}
