using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGUI : MonoBehaviour
{
	public static PauseGUI instance;

	public Image audioButtonImage;

	public Sprite audioButtonImageEnabled;

	public Sprite audioButtonImageDisabled;

	private void Awake()
	{
		PauseGUI.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
		ScoreHandler.instance.LoadVoiceState();
		if (ScoreHandler.instance.voiceState == "on")
		{
			this.audioButtonImage.sprite = this.audioButtonImageEnabled;
		}
		if (ScoreHandler.instance.voiceState == "off")
		{
			this.audioButtonImage.sprite = this.audioButtonImageDisabled;
		}
	}

	public void OnCloseButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		Pause.instance.UnPause();
	}

	public void OnRefreshButtonClick()
	{
		GameManager.instance.RestartGame(1);
		Pause.instance.UnPause();
	}

	public void OnHomeButtonClick()
	{
		Pause.instance.UnPause();
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GUIManager.instance.ShowMainMenuGUI(1);
		InGameGUI.instance.Deactivate();
		Pause.instance.UnPause();
	}

	public void Deactivate()
	{
		GameManager.instance.gamePaused = false;
		base.gameObject.SetActive(false);
	}

	public void Activate()
	{
		base.gameObject.SetActive(true);
	}
}
