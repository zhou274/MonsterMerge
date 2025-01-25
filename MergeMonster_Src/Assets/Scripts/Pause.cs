using System;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public Text pauseText;

	public Image audioButtonImage;

	public Sprite audioButtonImageEnabled;

	public Sprite audioButtonImageDisabled;

	public static Pause instance;

	private void Awake()
	{
		Pause.instance = this;
	}

	private void Start()
	{
		this.pauseText.text = LanguageHelper.GetString("pause", "pause");
	}

	private void Update()
	{
	}

	public void InPause()
	{
		GameManager.instance.gamePaused = true;
		GUIManager.instance.OpenMask();
		if (ScoreHandler.instance.score > InGameGUI.instance.lastHighScore)
		{
			UnityEngine.Debug.LogError("记录分数");
			ScoreHandler.instance.saveHighScoreToPrefs();
			
		}
		base.GetComponent<Animation>().Play("P1");
	}

	public void UnPause()
	{
		GameManager.instance.gamePaused = false;
		GUIManager.instance.CloseMask();
		base.GetComponent<Animation>().Play("P2");
	}

	public void ReStart()
	{
		base.GetComponent<Animation>().Play("P2");
		GameManager.instance.RestartGame(1);
	}

	public void onSoundButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		if (ScoreHandler.instance.voiceState == "off")
		{
			this.audioButtonImage.sprite = this.audioButtonImageEnabled;
			AudioListener.volume = 1f;
			ScoreHandler.instance.SaveVoiceStateOn();
		}
		else
		{
			this.audioButtonImage.sprite = this.audioButtonImageDisabled;
			AudioListener.volume = 0f;
			ScoreHandler.instance.SaveVoiceStateOff();
		}
	}
}
