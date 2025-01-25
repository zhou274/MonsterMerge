using System;
using UnityEngine;

public class UIEvent : MonoBehaviour
{
	public static UIEvent instance;

	private void Start()
	{
		UIEvent.instance = this;
	}

	private void Update()
	{
	}

	public void OnClickPlayGame()
	{
	}

	public void OpenStore()
	{
		Store.instance.OpenStore();
	}

	public void CloseStore()
	{
	}

	public void OpenSetting()
	{
		GUIManager.instance.ShowSettingGUI();
		SettingsGUI.instance.OpenSetting();
	}

	public void CloseSetting()
	{
		SettingsGUI.instance.CloseSettingGUI();
		GUIManager.instance.CloseSettingGUI();
	}

	public void OpenLeaderboard()
	{
		RankManager.instance.showLeaderboard();
	}

	public void LinkWeb(string url)
	{
	}

	public void LoginFacebook()
	{
	}

	public void PlayAds()
	{
	}

	public void OnPause()
	{
	}

	public void InPause()
	{
	}

	public void OpenAccountUI()
	{
	}

	public void CloseAccountUI()
	{
	}

	public void TestButton()
	{
		RankManager.instance.reportScore(100L);
	}
}
