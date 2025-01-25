using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
	private sealed class _TestMode_c__Iterator21 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

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
				this._current = new WaitForSeconds(1f);
				this._PC = 1;
				return true;
			case 1u:
				GUIManager.instance.PurchseComplete();
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

	public Image coinsImage;

	public static Store instance;

	public Button doubleCoins;

	public Text loginOrInvite;

	public Text storeText;

	public Text doubleCoinsText;

	public Sprite areadlyPauchaseSprite;

	private bool state;

	public GameObject invite;

	private void Awake()
	{
		Store.instance = this;
	}

	private void Start()
	{
		this.storeText.text = LanguageHelper.GetString("store", string.Empty);
		this.doubleCoinsText.text = LanguageHelper.GetString("double_Coins", string.Empty);
	}

	public void OpenStore()
	{
		ScoreHandler.instance.LoadDoubleCoinsState();
		if (ScoreHandler.instance.doublCoinsState == "true")
		{
			this.doubleCoins.GetComponent<Image>().sprite = this.areadlyPauchaseSprite;
			this.doubleCoins.interactable = false;
		}
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GameManager.instance.gamePaused = true;
		if (MainMenuGUI.instance.RankIsOpen)
		{
			this.state = true;
			UnityEngine.Debug.LogError("已经登陆");
			this.invite.gameObject.SetActive(false);
			this.coinsImage.gameObject.SetActive(false);
		}
		else
		{
			this.state = false;
			this.loginOrInvite.text = LanguageHelper.GetString("login with Facebook", string.Empty);
		}
		ScoreHandler.instance.LoadDoubleCoinsState();
		base.gameObject.GetComponent<Animation>().Play();
		GUIManager.instance.OpenMask();
	}

	public void CloseStore()
	{
		GameManager.instance.gamePaused = false;
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		GUIManager.instance.CloseMask();
		base.GetComponent<Animation>().Play("O2");
	}

	public void DebugTheButton()
	{
		UnityEngine.Debug.LogError("succeed");
	}

	public void LoginOrInvite()
	{
		
	}

	public void WaitForPurchase()
	{
		this.CloseStore();
		GUIManager.instance.ShowPurchaseMask();
	}

	public IEnumerator TestMode()
	{
		return new Store._TestMode_c__Iterator21();
	}

	public void Test()
	{
		base.StartCoroutine(this.TestMode());
	}
}
