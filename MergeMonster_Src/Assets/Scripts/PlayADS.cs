using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class PlayADS : MonoBehaviour
{
	private sealed class _JumpNumber_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _d___0;

		internal float _i___1;

		internal int _PC;

		internal object _current;

		internal PlayADS __f__this;

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
				this._d___0 = this.__f__this.targetCoinsNumber - this.__f__this.currentCoinsNumber;
				this.__f__this.start = this.__f__this.currentCoinsNumber;
				this.__f__this.jumpTimes = (float)(this.__f__this.targetCoinsNumber - this.__f__this.currentCoinsNumber) / this.__f__this.time;
				this._i___1 = 0f;
				break;
			case 1u:
				this._i___1 += 1f;
				break;
			case 2u:
				AccountGameOver.instance.CloseAccountGUI();
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._i___1 >= this.__f__this.jumpTimes)
			{
				this.__f__this.start = this.__f__this.targetCoinsNumber;
				this.__f__this.coinsNumber.text = this.__f__this.start.ToString();
				this.__f__this.StopCoroutine(this.__f__this.JumpNumber());
				this._current = new WaitForSeconds(1f);
				this._PC = 2;
			}
			else
			{
				this.__f__this.start++;
				this.__f__this.coinsNumber.text = this.__f__this.start.ToString();
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

	public Text coinsNumber;

	private int currentCoinsNumber;

	private int targetCoinsNumber;

	public Button closeButton;

	private float time = 3f;

	private int start;

	private float jumpTimes;

	public GameObject watchADS;

	public static PlayADS instance;

	private IEnumerator JumpNumber()
	{
		PlayADS._JumpNumber_c__Iterator2 _JumpNumber_c__Iterator = new PlayADS._JumpNumber_c__Iterator2();
		_JumpNumber_c__Iterator.__f__this = this;
		return _JumpNumber_c__Iterator;
	}

	private void Awake()
	{
		PlayADS.instance = this;
	}

	public void ShowRewardedAd()
	{
		AccountGameOver.instance.hasWatchingAds = true;
		//if (Advertisement.IsReady("rewardedVideo"))
		//{
		//	ShowOptions options = new ShowOptions
		//	{
		//		resultCallback = new Action<ShowResult>(this.HandleShowResult)
		//	};
		//	Advertisement.Show("rewardedVideo", options);
		//}
	}

	public void ShowAd()
	{
		//if (Advertisement.IsReady())
		//{
		//	Advertisement.Show();
		//}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Failed:
			UnityEngine.Debug.LogError("The ad failed to be shown.");
			break;
		case ShowResult.Skipped:
			UnityEngine.Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Finished:
			UnityEngine.Debug.Log("The ad was successfully shown.");
			this.currentCoinsNumber = int.Parse(this.coinsNumber.text);
			if (AccountGameOver.instance.hasWatchingAds)
			{
				this.targetCoinsNumber = this.currentCoinsNumber * 2;
			}
			else
			{
				this.targetCoinsNumber = this.currentCoinsNumber;
			}
			base.StartCoroutine(this.JumpNumber());
			break;
		}
	}
}
