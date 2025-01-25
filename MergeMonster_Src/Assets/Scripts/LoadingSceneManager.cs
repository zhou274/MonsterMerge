using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
	private sealed class _LoadAsynchronously_c__Iterator15 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

		internal LoadingSceneManager __f__this;

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
				this.__f__this.LoadingSetup();
				this.__f__this._asyncOperation = SceneManager.LoadSceneAsync(LoadingSceneManager._sceneToLoad);
				this.__f__this._asyncOperation.allowSceneActivation = false;
				break;
			case 1u:
				break;
			case 2u:
				goto IL_CB;
			default:
				return false;
			}
			if (this.__f__this._asyncOperation.progress < 0.9f)
			{
				this.__f__this._fillTarget = this.__f__this._asyncOperation.progress;
				this._current = null;
				this._PC = 1;
				return true;
			}
			this.__f__this._fillTarget = 1f;
			IL_CB:
			if (this.__f__this.LoadingProgressBar.GetComponent<Image>().fillAmount != this.__f__this._fillTarget)
			{
				this._current = null;
				this._PC = 2;
				return true;
			}
			this.__f__this._asyncOperation.allowSceneActivation = true;
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

	[Header("Binding")]
	public static string LoadingScreenSceneName = "LoadScene";

	[Header("GameObjects")]
	public Text LoadingText;

	public CanvasGroup LoadingProgressBar;

	public CanvasGroup LoadingAnimation;

	public CanvasGroup LoadingCompleteAnimation;

	[Header("Time")]
	public float StartFadeDuration = 0.2f;

	public float ProgressBarSpeed = 2f;

	public float ExitFadeDuration = 0.2f;

	public float LoadCompleteDelay = 0.5f;

	protected AsyncOperation _asyncOperation;

	protected static string _sceneToLoad = "Game";

	protected float _fadeDuration = 0.5f;

	protected float _fillTarget = 1f;

	protected virtual void Start()
	{
		if (LoadingSceneManager._sceneToLoad != string.Empty)
		{
			base.StartCoroutine(this.LoadAsynchronously());
		}
	}

	protected virtual void Update()
	{
		this.LoadingProgressBar.GetComponent<Image>().fillAmount = this.Approach(this.LoadingProgressBar.GetComponent<Image>().fillAmount, this._fillTarget, Time.deltaTime * this.ProgressBarSpeed);
	}

	protected virtual IEnumerator LoadAsynchronously()
	{
		LoadingSceneManager._LoadAsynchronously_c__Iterator15 _LoadAsynchronously_c__Iterator = new LoadingSceneManager._LoadAsynchronously_c__Iterator15();
		_LoadAsynchronously_c__Iterator.__f__this = this;
		return _LoadAsynchronously_c__Iterator;
	}

	protected virtual void LoadingSetup()
	{
		this.LoadingProgressBar.GetComponent<Image>().fillAmount = 0f;
	}

	public static void LoadScene(string sceneToLoad)
	{
		Application.backgroundLoadingPriority = ThreadPriority.High;
		if (LoadingSceneManager.LoadingScreenSceneName != null)
		{
			SceneManager.LoadScene(LoadingSceneManager.LoadingScreenSceneName);
		}
	}

	private float Approach(float from, float to, float amount)
	{
		if (from < to)
		{
			from += amount;
			if (from > to)
			{
				return to;
			}
		}
		else
		{
			from -= amount;
			if (from < to)
			{
				return to;
			}
		}
		return from;
	}
}
