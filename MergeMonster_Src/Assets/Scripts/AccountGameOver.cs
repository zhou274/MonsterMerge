using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AccountGameOver : MonoBehaviour
{
	private sealed class _JumpNumber_c__Iterator11 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _time___0;

		internal int _start___1;

		internal int _target___2;

		internal float _jumpTimes___3;

		internal float _i___4;

		internal int _PC;

		internal object _current;

		internal AccountGameOver __f__this;

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
				this._time___0 = 3f;
				this._start___1 = Mathf.RoundToInt((float)(ScoreHandler.instance.score / 4));
				this._target___2 = Mathf.RoundToInt((float)(ScoreHandler.instance.score / 2));
				this._jumpTimes___3 = (float)(this._target___2 - this._start___1) / this._time___0;
				this._i___4 = 0f;
				break;
			case 1u:
				this._i___4 += 1f;
				break;
			case 2u:
				this.__f__this.CloseAccountGUI();
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._i___4 >= this._jumpTimes___3)
			{
				this._start___1 = this._target___2;
				this.__f__this.scoreText.text = this._start___1.ToString();
				this.__f__this.StopCoroutine(this.__f__this.JumpNumber());
				this._current = new WaitForSeconds(1f);
				this._PC = 2;
			}
			else
			{
				this._start___1++;
				this.__f__this.scoreText.text = this._start___1.ToString();
				this._current = new WaitForEndOfFrame();
				this._PC = 1;
			}
			return true;
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

	public static AccountGameOver instance;

	public Text scoreText;

	public bool hasWatchingAds;

	public GameObject mask;

	public GameObject comment;

	public string commentUrlOfAndroid;

	public string commentUrlOfIOS;

	public Text titleText;

	public GameObject watchAds;

	public GameObject doubleSign;

	public Text watchAdsText;

	private void Awake()
	{
		AccountGameOver.instance = this;
	}

	private void Start()
	{
		this.titleText.text = LanguageHelper.GetString("get_coins", string.Empty);
		this.watchAdsText.text = LanguageHelper.GetString("watch_ads", "watch ads");
	}

	private void Update()
	{
	}

	public void EntryAccount()
	{
		GUIManager.instance.OpenMask();
		this.scoreText.text = Mathf.RoundToInt((float)(ScoreHandler.instance.score / 4)).ToString();
		ScoreHandler.instance.LoadDoubleCoinsState();
		if (ScoreHandler.instance.doublCoinsState == "true")
		{
			base.GetComponent<Animation>().Play("A1");
		}
		else
		{
			base.GetComponent<Animation>().Play("A10");
		}
	}

	public void CloseAccountGUI()
	{
		base.GetComponent<Animation>().Play("A2");
	}

	public void OpenTheGameOverGUI()
	{
		this.watchAds.gameObject.SetActive(true);
		GUIManager.instance.CloseMask();
		GUIManager.instance.CloseInGameGUI();
		base.StartCoroutine(GameManager.instance.GameOver());
	}

	public void HideComment()
	{
		this.comment.SetActive(false);
	}

	public void WriteComment()
	{
		Application.OpenURL(this.commentUrlOfAndroid);
	}

	public void DisplayComment()
	{
		this.comment.SetActive(true);
	}

	public void WatchAds()
	{
		PlayADS.instance.ShowRewardedAd();
		this.watchAds.gameObject.SetActive(false);
	}

	public void PlayAni()
	{
		base.StartCoroutine(this.JumpNumber());
	}

	public IEnumerator JumpNumber()
	{
		AccountGameOver._JumpNumber_c__Iterator11 _JumpNumber_c__Iterator = new AccountGameOver._JumpNumber_c__Iterator11();
		_JumpNumber_c__Iterator.__f__this = this;
		return _JumpNumber_c__Iterator;
	}
}
