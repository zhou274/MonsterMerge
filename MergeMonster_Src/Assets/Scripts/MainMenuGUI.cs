using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour
{
	public static MainMenuGUI instance;

	public GameObject settingsGUI;

	public GameObject friendRankGUI;

	public Image levelLogo;

//	public Button faceBookButton;

	public Button playButton;

	public Button shoppingButton;

	public Button LeaderBoardButton;

	public Button colorRushButton;

	public Button settingButton;

	public bool isSetting;

	public Text scoreText;

	public Text coinsText;

	public Text levelText;

	//public Text loginFacebookText;

	public bool RankIsOpen;

	public Image levelCircleFilled;

	public GameObject UIMask;

	public string colorRushLinkInAndroid;

	public string colorRushLinkInIOS;

	public Sprite[] levelSprite;

	private int currentHighScore;

	public Transform chinesePanel;

	public bool skip;

	public SkeletonGraphic screenFly;

	public GameObject screenFlyGUI;

	private void Awake()
	{
		MainMenuGUI.instance = this;
	}

	private void Start()
	{
	//this.loginFacebookText.text = LanguageHelper.GetString("login with Facebook", string.Empty);
	}

	private void Update()
	{
		this.coinsText.text = string.Empty + ScoreHandler.instance.specialPoints;
		this.scoreText.text = string.Empty + ScoreHandler.instance.highScore;
		this.levelText.text = string.Empty + LevelHandler.GetLevelByExp(ScoreHandler.instance.lifetimeScore);
		this.levelCircleFilled.fillAmount = LevelHandler.fillValueByExp(ScoreHandler.instance.lifetimeScore);
	}

	private void OnEnable()
	{
		this.setLevelLogo();
		this.screenFly.gameObject.SetActive(false);
		if (this.RankIsOpen)
		{
		//this.faceBookButton.gameObject.SetActive(false);
			this.friendRankGUI.gameObject.SetActive(true);
			
		}
		else
		{
		//this.faceBookButton.gameObject.SetActive(true);
			this.friendRankGUI.gameObject.SetActive(false);
		}
	}

	public void RefreshHighScore()
	{
		this.scoreText.text = string.Empty + ScoreHandler.instance.highScore;
	}

	public void OpenSFGUI()
	{
	}

	public void CloseSFGUI()
	{
		GUIManager.instance.CloseMask();
		this.screenFlyGUI.SetActive(false);
	}

	public void RefreshCoins()
	{
		this.coinsText.text = string.Empty + ScoreHandler.instance.specialPoints;
	}

	public void OnSettingsButtonClick()
	{
		this.isSetting = true;
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GUIManager.instance.ShowSettingGUI();
	}

	public void OnPlayButtonClick()
	{
		this.friendRankGUI.gameObject.SetActive(false);
		MainMenuGUI.instance.Deactivate();
		GUIManager.instance.ShowInGameGUI();
		GameManager.instance.StartGame();
	}

	public void LinkColorRushInIOS()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		Application.OpenURL(this.colorRushLinkInAndroid);
	}

	public void onLeaderboardButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
	}

	public void OnFacebookButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		//FacebookManager.instance.CallFBLogin();
		this.friendRankGUI.SetActive(true);
	//this.faceBookButton.gameObject.SetActive(false);
	}

	public void Deactivate()
	{
		base.gameObject.SetActive(false);
	}

	public void Activate()
	{
		base.gameObject.SetActive(true);
	}

	private void OpenUIMask()
	{
		this.playButton.interactable = false;
	//	this.faceBookButton.interactable = false;
		this.LeaderBoardButton.interactable = false;
		this.settingButton.interactable = false;
		this.shoppingButton.interactable = false;
		this.colorRushButton.interactable = false;
	}

	public void CloseUIMask()
	{
		this.UIMask.SetActive(false);
		this.playButton.interactable = true;
	//	this.faceBookButton.interactable = true;
		this.LeaderBoardButton.interactable = true;
		this.settingButton.interactable = true;
		this.shoppingButton.interactable = true;
		this.colorRushButton.interactable = true;
	}

	public void SaveScore()
	{
	//FacebookManager.instance.SaveScores(0);
	}

	public void setLevelLogo()
	{
		int num = int.Parse(this.levelText.text);
		if (num < 10)
		{
			this.levelLogo.sprite = this.levelSprite[0];
			this.levelLogo.transform.localScale = Vector3.one * 0.5f;
		}
		else if (num < 20)
		{
			this.levelLogo.sprite = this.levelSprite[1];
			this.levelLogo.transform.localScale = Vector3.one * 0.6f;
		}
		else if (num < 30)
		{
			this.levelLogo.sprite = this.levelSprite[2];
			this.levelLogo.transform.localScale = Vector3.one * 0.7f;
		}
		else if (num < 40)
		{
			this.levelLogo.sprite = this.levelSprite[3];
			this.levelLogo.transform.localScale = Vector3.one * 0.8f;
		}
		else if (num < 50)
		{
			this.levelLogo.sprite = this.levelSprite[4];
			this.levelLogo.transform.localScale = Vector3.one * 0.85f;
		}
		else if (num < 60)
		{
			this.levelLogo.sprite = this.levelSprite[5];
			this.levelLogo.transform.localScale = Vector3.one * 0.9f;
		}
		else
		{
			this.levelLogo.sprite = this.levelSprite[6];
			this.levelLogo.transform.localScale = Vector3.one;
		}
	}
}
