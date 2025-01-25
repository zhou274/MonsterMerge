using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;

public class Integrations
{
	private Dictionary<string, string> globalParams = new Dictionary<string, string>();

	private static Integrations _instance;

	public InterstitialAd interstitial;

	public AdRequest adrequest;

	private BannerView bannerView;

	protected bool isLeadboardLogged;

	public static Integrations Instance()
	{
		if (Integrations._instance == null)
		{
			Integrations._instance = new Integrations();
			Integrations._instance.Init();
		}
		return Integrations._instance;
	}

	public string GetGlobalParam(string key, string defaultValue = "1")
	{
		if (this.globalParams.ContainsKey(key))
		{
			return this.globalParams[key];
		}
		return defaultValue;
	}

	public void SetGlobalParam(string key, string value)
	{
		if (this.globalParams.ContainsKey(key))
		{
			this.globalParams[key] = value;
		}
		else
		{
			this.globalParams.Add(key, value);
		}
	}

	private void Init()
	{
		this.RequestInterstitial();
	}

	public void RequestBanner()
	{
		this.bannerView = new BannerView("ca-app-pub-8969722984181378/2765549449", AdSize.Banner, AdPosition.Top);
		AdRequest request = new AdRequest.Builder().Build();
		this.bannerView.LoadAd(request);
	}

	public void RequestInterstitial()
	{
		this.interstitial = new InterstitialAd("ca-app-pub-8969722984181378/4242282645");
		AdRequest request = new AdRequest.Builder().Build();
		this.interstitial.LoadAd(request);
	}

	public void ShowInterstitial()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
		}
		this.interstitial.Destroy();
		this.RequestInterstitial();
	}
}
