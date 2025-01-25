using System;
using UnityEngine;
using UnityEngine.UI;

public class ShareGUI : MonoBehaviour
{
	public string facebookLink = "https://www.facebook.com/GameinLife-1327324820659542/";

	public Text ScoreText;

	public Text coinsText;

	public Text mergedText;

	public Text score;

	public Text merged;

	public Image levelCircleFilled;

	public Button restart;

	public Button home;

	public GameObject colorRush;

	public Button store;

	public Button[] shareButtons;

	public GameObject UIMask;

	public Text HighScore;

	public Sprite moreSprite;

	private void Start()
	{
		this.score.text = LanguageHelper.GetString("score", "分数");
		this.merged.text = LanguageHelper.GetString("merged", "合成");
	}

	private void Update()
	{
		this.ScoreText.text = string.Empty + ScoreHandler.instance.score;
		this.mergedText.text = string.Empty + ScoreHandler.instance.secondaryScore;
		this.coinsText.text = string.Empty + ScoreHandler.instance.specialPoints;
		this.HighScore.text = string.Empty + ScoreHandler.instance.highScore;
	}

	private void OnEnable()
	{
	}

	public void OnClickHomeButton()
	{
		this.Deactivate();
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GUIManager.instance.ShowMainMenuGUI(0);
	}

	public void OnCilckRestartButton()
	{
		GameManager.instance.RestartGame(0);
		this.Deactivate();
		GUIManager.instance.ShowInGameGUI();
	}

	public void Activete()
	{
		base.gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		base.gameObject.SetActive(false);
	}

	public void OnShareFacebookClick()
	{
		UnityEngine.Debug.LogError("dadada");
	}

	public void OpenStore()
	{
		Store.instance.OpenStore();
	}

	public void OnFacebookComment()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		Application.OpenURL(this.facebookLink);
	}
}
