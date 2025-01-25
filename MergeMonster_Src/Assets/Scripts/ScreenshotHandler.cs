using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
	private sealed class _grabScreenshot_c__Iterator20 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _width___0;

		internal int _height___1;

		internal float _rate___2;

		internal Texture2D _tex___3;

		internal int _PC;

		internal object _current;

		internal ScreenshotHandler __f__this;

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
				this._current = new WaitForEndOfFrame();
				this._PC = 1;
				return true;
			case 1u:
				this._width___0 = Screen.width;
				this._height___1 = Screen.height;
				this._rate___2 = (float)this._width___0 / (float)this._height___1;
				this._tex___3 = new Texture2D(this._width___0, this._height___1, TextureFormat.RGB24, true);
				this._tex___3.ReadPixels(new Rect(32f, 165f, 285f, 285f), 0, 0);
				this._tex___3.Apply();
				this.__f__this.screenshot = this._tex___3;
				this._PC = -1;
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

	[HideInInspector]
	public Texture2D screenshot;

	private int width;

	private int height;

	public static ScreenshotHandler instance;

	private void Awake()
	{
		ScreenshotHandler.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public IEnumerator grabScreenshot()
	{
		ScreenshotHandler._grabScreenshot_c__Iterator20 _grabScreenshot_c__Iterator = new ScreenshotHandler._grabScreenshot_c__Iterator20();
		_grabScreenshot_c__Iterator.__f__this = this;
		return _grabScreenshot_c__Iterator;
	}
}
