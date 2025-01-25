//using Facebook.Unity;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ShareManager : MonoBehaviour
{
	public static string instagramLink;

	public static string colorRushAppUri;

	public static string title = "Play Monster Camp with me";

	private string shareMessage;

	public static ShareManager instance;

	public Texture2D img;

	private string imagePath;

	public bool chineseShare;

	private void Awake()
	{
		ShareManager.instance = this;
		this.Copy2SDpath();
	}

	private void Copy2SDpath()
	{
		this.imagePath = Application.persistentDataPath + "/share.png";
		if (!File.Exists(this.imagePath))
		{
			Texture2D texture2D = Resources.Load("xuanchuantu3") as Texture2D;
			if (texture2D != null)
			{
				File.WriteAllBytes(this.imagePath, texture2D.EncodeToPNG());
			}
		}
	}

	public void SetChineseState()
	{
		this.chineseShare = true;
	}

	public void ShareInsgram()
	{
		//FB.FeedShare(string.Empty, new Uri(ShareManager.instagramLink), "标题", "正文", "描述", null, string.Empty, null);
	}

	

	public void SendEmail()
	{
		Application.OpenURL("mailto:feedback@gpower.co?subject=Monster camp feedback_Feedback&body=from Android");
	}

	public void ShareNative()
	{
		UnityEngine.Debug.LogError("本地分享");
		string @string = LanguageHelper.GetString("share_title", string.Empty);
		//NativeShare.instane.ShareScreenshotWithText(@string);
	}

	public void Share(string url)
	{
        /*
		FB.FeedShare(string.Empty, null, LanguageHelper.GetString("share_title", "Play Monster Camp with me"), string.Concat(new object[]
		{
			LanguageHelper.GetString("share_message1", string.Empty),
			"  ",
			ScoreHandler.instance.score,
			"  ",
			LanguageHelper.GetString("share_message2", string.Empty),
			"  ",
			ScoreHandler.instance.secondaryScore,
			"  ",
			LanguageHelper.GetString("share_message3", string.Empty)
		}), "描述", new Uri(url), string.Empty, null);
		*/
	}
}
