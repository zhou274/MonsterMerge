using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
//using UnityEngine.Experimental.Networking;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private sealed class _GetDataWithoutGoogle_c__Iterator12 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal UnityWebRequest _www___0;

		internal int _price___1;

		internal int _admob___2;

		internal int _spider___3;

		internal int _compound___4;

		internal int _PC;

		internal object _current;

		internal GameManager __f__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._www___0 = UnityWebRequest.Get(this.__f__this.url);
				this._current = this._www___0.Send();
				this._PC = 1;
				return true;
			case 1u:
				if (this._www___0.isNetworkError)
				{
					UnityEngine.Debug.LogError(this._www___0.error);
				}
				else
				{
					//this.__f__this.ParseResult(this._www___0.downloadHandler.text);
					//this._price___1 = int.Parse(this.__f__this.GetString("unity_trash_price", string.Empty));
                    /*
					if (this._price___1 != 0)
					{
						this.__f__this.trashPriceAdd = this._price___1;
					}
					this._admob___2 = int.Parse(this.__f__this.GetString("unity_admob_divisor", string.Empty));
					if (this._admob___2 != 0)
					{
						this.__f__this.divisor = this._admob___2;
					}
					this._spider___3 = int.Parse(this.__f__this.GetString("unity_spider_probability", string.Empty));
					if (this._spider___3 != 0)
					{
						this.__f__this.spiderProbability = this._spider___3;
					}
					this._compound___4 = int.Parse(this.__f__this.GetString("unity_spawner_compound_percentage", string.Empty));
					if (this._compound___4 != 0)
					{
						this.__f__this.spawnerCompoundPercentage = this._compound___4;
					}
					*/
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private sealed class _GameOver_c__Iterator13 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				ScoreHandler.instance.LoadDoubleCoinsState();
				SoundsManager.instance.PlayAudioSource(SoundsManager.instance.gameover);
				if (ScoreHandler.instance.doublCoinsState == "true")
				{
					if (AccountGameOver.instance.hasWatchingAds)
					{
						ScoreHandler.instance.increaseSpecialPoints(ScoreHandler.instance.score);
					}
					else
					{
						ScoreHandler.instance.increaseSpecialPoints(Mathf.RoundToInt((float)(ScoreHandler.instance.score / 4)) * 2);
					}
				}
				else if (AccountGameOver.instance.hasWatchingAds)
				{
					ScoreHandler.instance.increaseSpecialPoints(Mathf.RoundToInt((float)(ScoreHandler.instance.score / 4)) * 2);
				}
				else
				{
					ScoreHandler.instance.increaseSpecialPoints(Mathf.RoundToInt((float)(ScoreHandler.instance.score / 4)));
				}
				if (ScoreHandler.instance.score > InGameGUI.instance.lastHighScore)
				{
					ScoreHandler.instance.highScore = ScoreHandler.instance.score;
					if (RankManager.instance != null)
					{
						RankManager.instance.reportScore((long)ScoreHandler.instance.highScore);
					}
					
				}
				InGameGUI.instance.Deactivate();
				ScoreHandler.instance.SaveNumberOfTime();
				GUIManager.instance.ShowGameOverGUI();
				this._current = null;
				this._PC = 1;
				return true;
			case 1u:
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public const int SINGLE_DICE_TYPE = 0;

	public const int DOUBLE_DICE_TYPE = 1;

	public const int RANDOM_DICE_TYPE = 2;

	public static GameManager instance;

	public GameObject TopBackground;

	public GameObject tip;

	public int commentCoins;

	public int HammerPrice;

	public bool isDragging;

	public float gridTileDistanceOffset = 0.9f;

	public float diceMinDropDistance = 0.7f;

	public Button openStore;

	private int specialPoint;

	public int divisor = 5;

	public int trashPrice;

	public int trashPriceAdd = 25;

	public int skillCount;

	public GameObject skill;

	public string url = "http://media.gpowers.net/products/monstercamp/unity_remote_config.dat.txt";

	public GridSpawner gridSpawner;

	public GridRowsContainer gridRowsContainer;

	public GridTile firstGridTile;

	public Color[] diceColorList;

	public int openStoreType;

	public bool canDragSkill;

	public Material particleMaterial;

	public bool gamePaused;

	public bool voiceState = true;

	public int degreeOfDiffculty = 1;

	public int spiderProbability = 80;

	public int spawnerCompoundPercentage = 67;

	private Dictionary<string, string> dic = new Dictionary<string, string>();

	private void Awake()
	{
		GameManager.instance = this;
        Application.targetFrameRate = 60;
		bool flag = true;
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			flag = false;
		}
		if (!flag)
		{
			this.divisor = 3;
			this.spawnerCompoundPercentage = 67;
			this.spiderProbability = 80;
			this.trashPriceAdd = 30;
		}
		base.StartCoroutine(this.GetDataWithoutGoogle());
		Integrations.Instance().RequestInterstitial();
		this.InitMultiLanguage();
	}

	private void InitMultiLanguage()
	{
		string language = Application.systemLanguage.ToString();
		LanguageHelper.SetLanguage(language);
	}

	private void Start()
	{
		this.SpawnGrid();
		this.InitLeaderBoard();
		//FacebookManager.instance.CallFBInit();
		this.specialPoint = ScoreHandler.instance.specialPoints;
		this.TopBackground.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "tip", true);
		DiceCompoundSpawner.instance.SpawnDices(0);
		Application.targetFrameRate = 60;
		GUIManager.instance.ShowMainMenuGUI(0);
	}

	public IEnumerator GetDataWithoutGoogle()
	{
		GameManager._GetDataWithoutGoogle_c__Iterator12 _GetDataWithoutGoogle_c__Iterator = new GameManager._GetDataWithoutGoogle_c__Iterator12();
		_GetDataWithoutGoogle_c__Iterator.__f__this = this;
		return _GetDataWithoutGoogle_c__Iterator;
	}
    /*
	public void ParseResult(string text)
	{
		string text2 = text.Replace("\r", string.Empty);
		string[] array = text2.Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				'='
			});
			this.dic[array2[0]] = array2[1];
		}
	}
    */
	private string GetString(string name, string defaultValue = "")
	{
		if (this.dic.ContainsKey(name))
		{
			return this.dic[name];
		}
		return defaultValue;
	}

	private void InitLeaderBoard()
	{
		Social.localUser.Authenticate(new Action<bool>(GameManager.ProcessAuthentication));
	}

	private static void ProcessAuthentication(bool success)
	{
		if (success)
		{
			UnityEngine.Debug.Log("Authenticated, checking achievements");
		}
		else
		{
			UnityEngine.Debug.Log("Failed to authenticate");
		}
	}

	private void SpawnGrid()
	{
		this.gridSpawner.SpawnGridByDuplicatingFirstTile(this.firstGridTile, this.gridTileDistanceOffset);
	}

	private void Update()
	{
		if (this.skillCount > 0)
		{
			this.skill.SetActive(true);
		}
		else
		{
			this.skill.SetActive(false);
		}
	}

	private void DebugCommands()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.P))
		{
			this.RestartGame(0);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.R))
		{
			this.RespawnDices();
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.C))
		{
			this.ClearGrid();
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.D))
		{
			PlayerPrefs.DeleteAll();
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.O))
		{
			this.EntryAccountGUI();
		}
	}

	public void RespawnDices()
	{
		DiceCompound.instance.RespawnDiceCompound(2);
	}

	public void ClearGrid()
	{
		GridSpawner.instance.ClearGrid();
	}

	public void RestartGame(int flag)
	{
		if (flag == 0)
		{
			SoundsManager.instance.PlayAudioSource(SoundsManager.instance.gamestart);
			this.skillCount = 0;
			this.trashPrice = 20;
			this.HammerPrice = 20;
			AccountGameOver.instance.hasWatchingAds = false;
			Camera.main.transform.position = CameraManager.instance.originalPosition;
			ScoreHandler.instance.reset();
			GridSpawner.instance.ClearGrid();
			DiceCompound.instance.RespawnDiceCompound(2);
		}
		else
		{
			SoundsManager.instance.PlayAudioSource(SoundsManager.instance.gamestart);
			this.skillCount = 0;
			this.trashPrice = 20;
			this.HammerPrice = 20;
			AccountGameOver.instance.hasWatchingAds = false;
			Camera.main.transform.position = CameraManager.instance.originalPosition;
			ScoreHandler.instance.reset();
			GridSpawner.instance.ClearGrid();
			DiceCompound.instance.RespawnDiceCompound(2);
			Pause.instance.UnPause();
		}
	}

	public void StartGame()
	{
		this.RestartGame(0);
		MainMenuGUI.instance.Deactivate();
		if (!GUIManager.instance.tutorialGUI.tutorialShown())
		{
			ScoreHandler.instance.increaseSpecialPoints(20);
			GUIManager.instance.ShowTutorialGUI();
			GUIManager.instance.OpenMask();
		}
		ScoreHandler.instance.LoadFirstUseDustbin();
		if (ScoreHandler.instance.fristUseDustbin != "true")
		{
			this.tip.SetActive(true);
		}
	}

	public IEnumerator GameOver()
	{
		return new GameManager._GameOver_c__Iterator13();
	}

	public void EntryAccountGUI()
	{
		AccountGameOver.instance.EntryAccount();
	}

	public void DeleteData()
	{
		PlayerPrefs.DeleteAll();
	}

	public void TestMode()
	{
		UnityEngine.Debug.LogError("dasdas");
		this.EntryAccountGUI();
	}

	public void TestDiffculty()
	{
		Util.showToast("当前语言" + Application.systemLanguage.ToString());
	}
}
