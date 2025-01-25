using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsGUI : MonoBehaviour
{
	public string InstagramLink = "https://www.instagram.com/gameinlifes/";

	public string facebookLink = "https://www.facebook.com/GameinLife-1327324820659542/";

	public string GameLinkOfAndroid = "https://play.google.com/store/apps/details?id=com.gameinlife.merged";

	public string GameLinkOfIOS = "https://itunes.apple.com/app/id1203669310?mt=8";

	public static SettingsGUI instance;

	public Image audioButtonImage;

	public Text settingText;

	public Text contactText;

	public Sprite audioButtonImageEnabled;

	public Sprite audioButtonImageDisabled;

	public Button closeButton;

	public Image maskUI;

	public Text restoreText;

	private void Awake()
	{
		SettingsGUI.instance = this;
	}

	private void Start()
	{
		this.restoreText.gameObject.SetActive(false);
		this.settingText.text = LanguageHelper.GetString("settings", string.Empty);
		this.contactText.text = LanguageHelper.GetString("contact", string.Empty);
		ScoreHandler.instance.LoadVoiceState();
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		if (ScoreHandler.instance.voiceState == "on")
		{
			this.audioButtonImage.sprite = this.audioButtonImageEnabled;
		}
		if (ScoreHandler.instance.voiceState == "off")
		{
			this.audioButtonImage.sprite = this.audioButtonImageDisabled;
		}
	}

	public void CloseSettingGUI()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GUIManager.instance.CloseMask();
		base.GetComponent<Animation>().Play("S2");
	}

	public void onRestoreIAPButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
	}

	public void onContactUsButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		Application.OpenURL(SocialNetworksManager.instance.contactUsURL);
	}

	public void onSoundButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		if (AudioListener.volume == 0f)
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

	public void OnClickLeaderBoard()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		RateManager.instance.rateGame();
	}

	public void onFacebookButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		Application.OpenURL(this.facebookLink);
	}

	public void OnInstagramClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		Application.OpenURL(this.InstagramLink);
	}

	public void OpenSetting()
	{
		GUIManager.instance.OpenMask();
		base.GetComponent<Animation>().Play();
	}

	public void OpenGamePager()
	{
		Application.OpenURL(this.GameLinkOfAndroid);
	}
}
