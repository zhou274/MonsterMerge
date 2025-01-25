using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGUI : MonoBehaviour
{
	public Text coinsText;

	public Text levelText;

	public Text mergedText;

	public Text scoreText;

	public Image levelCircleFilled;

	public GameObject screenshotImage;

	private void Start()
	{
		if (ScreenshotHandler.instance.screenshot != null)
		{
			Sprite sprite = Sprite.Create(ScreenshotHandler.instance.screenshot, new Rect(0f, 0f, 1080f, 1920f), new Vector2(0.5f, 0.5f));
			this.screenshotImage.GetComponent<Image>().sprite = sprite;
		}
		else
		{
			UnityEngine.Debug.LogError("无图");
		}
	}

	private void Update()
	{
		this.coinsText.text = string.Empty + ScoreHandler.instance.specialPoints;
		this.levelText.text = string.Empty + LevelHandler.GetLevelByExp(ScoreHandler.instance.lifetimeScore);
		this.mergedText.text = string.Empty + ScoreHandler.instance.secondaryScore;
		this.scoreText.text = string.Empty + ScoreHandler.instance.score;
		this.levelCircleFilled.fillAmount = LevelHandler.fillValueByExp(ScoreHandler.instance.lifetimeScore);
	}

	public void OnRestartButtonClick()
	{
		GameManager.instance.RestartGame(1);
		this.Deactivate();
		GUIManager.instance.ShowInGameGUI();
	}

	public void OnHomeButtonClick()
	{
		this.Deactivate();
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GUIManager.instance.ShowMainMenuGUI(0);
	}

	public void OnRemoveAdsButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
	}

	private void OnEnable()
	{
	}

	public void Activate()
	{
		base.gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		base.gameObject.SetActive(false);
	}

	public void OnShareButtonClick()
	{
		UnityEngine.Debug.LogError("dadasd");
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
	}

	public void OnShareFacebookClick()
	{
		UnityEngine.Debug.LogError("dadada");
	}
}
