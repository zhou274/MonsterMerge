using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IOSNativeShare : MonoBehaviour
{
	private sealed class _delayedShare_c__Iterator14 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string screenShotPath;

		internal string text;

		internal int _PC;

		internal object _current;

		internal string ___screenShotPath;

		internal string ___text;

		internal IOSNativeShare __f__this;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (!File.Exists(this.screenShotPath))
			{
				this._current = new WaitForSeconds(0.05f);
				this._PC = 1;
				return true;
			}
			this.__f__this.Share(this.text, this.screenShotPath, string.Empty, string.Empty);
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public string ScreenshotName = "screenshot.png";

	public void ShareScreenshotWithText(string text)
	{
		string text2 = Application.persistentDataPath + "/" + this.ScreenshotName;
		if (File.Exists(text2))
		{
			File.Delete(text2);
		}
		UnityEngine.ScreenCapture.CaptureScreenshot(this.ScreenshotName);
		base.StartCoroutine(this.delayedShare(text2, text));
	}

	private IEnumerator delayedShare(string screenShotPath, string text)
	{
		IOSNativeShare._delayedShare_c__Iterator14 _delayedShare_c__Iterator = new IOSNativeShare._delayedShare_c__Iterator14();
		_delayedShare_c__Iterator.screenShotPath = screenShotPath;
		_delayedShare_c__Iterator.text = text;
		_delayedShare_c__Iterator.___screenShotPath = screenShotPath;
		_delayedShare_c__Iterator.___text = text;
		_delayedShare_c__Iterator.__f__this = this;
		return _delayedShare_c__Iterator;
	}

	public void Share(string shareText, string imagePath, string url, string subject = "")
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]);
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
		{
			androidJavaClass.GetStatic<string>("ACTION_SEND")
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.net.Uri");
		AndroidJavaObject androidJavaObject2 = androidJavaClass2.CallStatic<AndroidJavaObject>("parse", new object[]
		{
			"file://" + imagePath
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
		{
			androidJavaClass.GetStatic<string>("EXTRA_STREAM"),
			androidJavaObject2
		});
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
		{
			"image/png"
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			shareText
		});
		AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass3.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject3 = androidJavaClass.CallStatic<AndroidJavaObject>("createChooser", new object[]
		{
			androidJavaObject,
			subject
		});
		@static.Call("startActivity", new object[]
		{
			androidJavaObject3
		});
	}
}
