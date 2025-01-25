using System;
using UnityEngine;

public class RateManager : MonoBehaviour
{
	public static RateManager instance;

	public string androidAppStoreUrl;

	public string iOSAppStoreUrl;

	private void Awake()
	{
		RateManager.instance = this;
	}

	public void rateGame()
	{
		string url = this.androidAppStoreUrl;
		url = this.iOSAppStoreUrl;
		Application.OpenURL(url);
	}
}
