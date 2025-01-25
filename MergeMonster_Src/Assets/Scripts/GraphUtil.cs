using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GraphUtil : ScriptableObject
{
	private sealed class _LoadImgEnumerator_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal string imgURL;

		internal WWW _www___0;

		internal Action<Texture2D> callback;

		internal int _PC;

		internal object _current;

		internal string ___imgURL;

		internal Action<Texture2D> ___callback;

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
				this._www___0 = new WWW(this.imgURL);
				this._current = this._www___0;
				this._PC = 1;
				return true;
			case 1u:
				if (this._www___0.error != null)
				{
					UnityEngine.Debug.LogError(this._www___0.error);
				}
				else
				{
					this.callback(this._www___0.texture);
					this._PC = -1;
				}
				break;
			}
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

	public static string GetPictureQuery(string facebookID, int? width = null, int? height = null, string type = null, bool onlyURL = false)
	{
		string text = string.Format("/{0}/picture", facebookID);
		string text2 = (!width.HasValue) ? string.Empty : ("&width=" + width.ToString());
		text2 += ((!height.HasValue) ? string.Empty : ("&height=" + height.ToString()));
		text2 += ((type == null) ? string.Empty : ("&type=" + type));
		if (onlyURL)
		{
			text2 += "&redirect=false";
		}
		if (text2 != string.Empty)
		{
			text = text + "?g" + text2;
		}
		return text;
	}

	public static void LoadImgFromURL(string imgURL, Action<Texture2D> callback)
	{
		Coroutiner.StartCoroutine(GraphUtil.LoadImgEnumerator(imgURL, callback));
	}

	public static IEnumerator LoadImgEnumerator(string imgURL, Action<Texture2D> callback)
	{
		GraphUtil._LoadImgEnumerator_c__Iterator1 _LoadImgEnumerator_c__Iterator = new GraphUtil._LoadImgEnumerator_c__Iterator1();
		_LoadImgEnumerator_c__Iterator.imgURL = imgURL;
		_LoadImgEnumerator_c__Iterator.callback = callback;
		_LoadImgEnumerator_c__Iterator.___imgURL = imgURL;
		_LoadImgEnumerator_c__Iterator.___callback = callback;
		return _LoadImgEnumerator_c__Iterator;
	}

	public static string DeserializePictureURL(object userObject)
	{
		Dictionary<string, object> dictionary = userObject as Dictionary<string, object>;
		object obj;
		if (dictionary.TryGetValue("picture", out obj))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)((Dictionary<string, object>)obj)["data"];
			return (string)dictionary2["url"];
		}
		return null;
	}

	public static int GetScoreFromEntry(object obj)
	{
		Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
		return Convert.ToInt32(dictionary["score"]);
	}
}
