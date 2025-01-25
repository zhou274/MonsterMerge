using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Util
{
	private sealed class _showToast_c__AnonStoreyAF
	{
		internal string content;

		internal void __m__128()
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.widget.Toast");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.lang.String", new object[]
			{
				this.content
			});
			AndroidJavaObject androidJavaObject2 = androidJavaClass.CallStatic<AndroidJavaObject>("makeText", new object[]
			{
				Util.context,
				androidJavaObject,
				androidJavaClass.GetStatic<int>("LENGTH_SHORT")
			});
			androidJavaObject2.Call("show", new object[0]);
		}
	}

	private static AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

	private static AndroidJavaObject currentActivity = Util.UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

	private static AndroidJavaObject context = Util.currentActivity.Call<AndroidJavaObject>("getApplicationContext", new object[0]);

	public static void showToast(string content)
	{
		Util.currentActivity.Call("runOnUiThread", new object[]
		{
			new AndroidJavaRunnable(delegate
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.widget.Toast");
				AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.lang.String", new object[]
				{
					content
				});
				AndroidJavaObject androidJavaObject2 = androidJavaClass.CallStatic<AndroidJavaObject>("makeText", new object[]
				{
					Util.context,
					androidJavaObject,
					androidJavaClass.GetStatic<int>("LENGTH_SHORT")
				});
				androidJavaObject2.Call("show", new object[0]);
			})
		});
	}
}
