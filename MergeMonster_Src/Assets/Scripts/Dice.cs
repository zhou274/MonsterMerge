using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
	private sealed class _playIdelAni_c__Iterator6 : IEnumerator, IDisposable, IEnumerator<object>
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
				GameObject.Find("Shovel").GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", true);
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

	private sealed class _BombSpawn_c__Iterator7 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

		internal Dice __f__this;

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
				this.__f__this.DestroyPreviousGraphic();
				this._current = null;
				this._PC = 1;
				return true;
			case 1u:
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

	private sealed class _lerpToTargetColor_c__Iterator8 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int targetColorValue;

		internal Color _targetColor___0;

		internal float _lerper___1;

		internal int _PC;

		internal object _current;

		internal int ___targetColorValue;

		internal Dice __f__this;

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
				this._targetColor___0 = this.__f__this.GetColorByValue(this.targetColorValue);
				this._lerper___1 = 0f;
				break;
			case 1u:
				break;
			case 2u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._lerper___1 > 1f)
			{
				this._current = null;
				this._PC = 2;
			}
			else
			{
				this._lerper___1 += Time.deltaTime;
				this.__f__this.sprite.color = Color.Lerp(this.__f__this.currentColor, this._targetColor___0, this._lerper___1);
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

	private sealed class _IncreaseGraphicEffect_c__Iterator9 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _lerper___0;

		internal int _PC;

		internal object _current;

		internal Dice __f__this;

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
			case 2u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (Merger.instance.mergedParticleList.mergedParticlesList.Count <= 0)
			{
				this._lerper___0 = 0f;
				this.__f__this.isEnlarging = false;
				this._current = null;
				this._PC = 2;
			}
			else
			{
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

	private sealed class _spawnAnimation_c__IteratorA : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Vector3 _targetscale___0;

		internal int _PC;

		internal object _current;

		internal Dice __f__this;

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
				this._targetscale___0 = this.__f__this.transform.localScale;
				this.__f__this.transform.localScale = Vector3.zero;
				break;
			case 1u:
				break;
			case 2u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (!(this.__f__this.transform.localScale != this._targetscale___0))
			{
				this._current = null;
				this._PC = 2;
			}
			else
			{
				this.__f__this.transform.localScale = Vector3.MoveTowards(this.__f__this.transform.localScale, this._targetscale___0, this.__f__this.spawnAnimationSpeed * Time.deltaTime);
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

	public static Dice instance;

	public GameObject itemPrefab;

	public GameObject diceItem;

	private float spawnAnimationSpeed = 6f;

	public int diceValue;

	public GameObject currentBackground;

	private float dotTransitionTime = 0.3f;

	public SpriteRenderer sprite;

	private SpriteRenderer[] dots;

	private Color currentColor;

	private int currentValue;

	public bool isEnlarging;

	private Vector3 tempClickPosition;

	private RaycastHit2D tempRayCastHit2D;

	private Collider2D tempCollider2D;

	private Dice currentlyDice;

	public Button skillButton;

	public Button TrashButton;

	public GameObject shovel;

	public GameObject ps;

	private void Awake()
	{
		Dice.instance = this;
	}

	private void Start()
	{
		this.SetRandomDiceValue();
		base.StartCoroutine(this.spawnAnimation());
	}

	private void Update()
	{
		if (InGameGUI.instance == null)
		{
			return;
		}
		if (!InGameGUI.instance.useHammer)
		{
			return;
		}
		if (InGameGUI.instance.allDice.Length <= 0)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.RayCast(UnityEngine.Input.mousePosition);
		}
	}

	public void RayCast(Vector3 clickPosition)
	{
		try
		{
			this.tempClickPosition = Camera.main.ScreenToWorldPoint(clickPosition);
			this.tempRayCastHit2D = Physics2D.Raycast(this.tempClickPosition, Vector2.zero);
			this.tempCollider2D = this.tempRayCastHit2D.collider;
			if (this.tempCollider2D != null && this.tempCollider2D.GetComponent<GridTile>() != null && this.tempCollider2D.GetComponent<GridTile>().placedDice != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("effe")) as GameObject;
				gameObject.transform.localScale = Vector2.one * 3f;
				gameObject.transform.position = this.tempCollider2D.transform.position;
				UnityEngine.Object.Destroy(gameObject, 1f);
				this.tempCollider2D.GetComponent<GridTile>().Reset();
				SoundsManager.instance.PlayAudioSource(SoundsManager.instance.hammer);
				ScoreHandler.instance.removeSpecialPoints(GameManager.instance.HammerPrice);
				GameManager.instance.HammerPrice += GameManager.instance.trashPriceAdd;
				InGameGUI.instance.RefreshHanmmerPrice();
			}
		}
		catch
		{
			UnityEngine.Debug.LogError("点击区域错误");
		}
		this.resetState();
		GameObject.Find("Shovel").GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "product2", false);
		base.StartCoroutine(this.playIdelAni());
	}

	private IEnumerator playIdelAni()
	{
		return new Dice._playIdelAni_c__Iterator6();
	}

	private void resetState()
	{
		ExplodeSkill.instance.hideAllUI(false);
		if (InGameGUI.instance.UIMask != null)
		{
			InGameGUI.instance.UIMask.SetActive(false);
		}
		if (GameManager.instance.skillCount > 0)
		{
			GameObject.Find("Skill").transform.GetChild(1).gameObject.SetActive(true);
		}
		else
		{
			InGameGUI.instance.skillCount.gameObject.SetActive(false);
		}
		GameManager.instance.gamePaused = false;
		InGameGUI.instance.useHammer = false;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Dice");
		for (int i = 0; i < array.Length; i++)
		{
			array[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
			array[i].transform.GetChild(1).GetComponent<MeshRenderer>().sortingOrder = 1;
			array[i].transform.GetChild(1).GetComponent<SkeletonAnimation>().skeleton.SetToSetupPose();
			array[i].transform.GetChild(1).GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "idle", false);
		}
	}

	public void SetDiceValue(int newvalue)
	{
		if (newvalue == DicePointHandler.instance.GetMaxDiceScore() + 1)
		{
			base.StartCoroutine(this.BombSpawn(newvalue));
			return;
		}
		if (newvalue == DicePointHandler.instance.GetMaxDiceScore())
		{
			SoundsManager.instance.PlayAudioSource(SoundsManager.instance.mSpawn);
		}
		this.currentValue = newvalue;
		this.isEnlarging = true;
		this.diceValue = newvalue;
		this.DestroyPreviousGraphic();
		this.AssignFg();
		base.StartCoroutine(this.IncreaseGraphicEffect());
	}

	private IEnumerator BombSpawn(int ColorValue)
	{
		Dice._BombSpawn_c__Iterator7 _BombSpawn_c__Iterator = new Dice._BombSpawn_c__Iterator7();
		_BombSpawn_c__Iterator.__f__this = this;
		return _BombSpawn_c__Iterator;
	}

	public void SetRandomDiceValue()
	{
		this.diceValue = this.GetRandomDiceValue();
		while (this.diceValue == DicePointHandler.instance.GetTempLastDiceScore())
		{
			this.diceValue = this.GetRandomDiceValue();
		}
		DicePointHandler.instance.SetTempLastDiceScore(this.diceValue);
		this.AssignFg();
	}

	public void AssignFg()
	{
		if (this.diceValue == 1)
		{
			this.SpawnFg("q0");
		}
		if (this.diceValue == 2)
		{
			this.SpawnFg("q1");
		}
		if (this.diceValue == 3)
		{
			this.SpawnFg("q2");
		}
		if (this.diceValue == 4)
		{
			this.SpawnFg("q3");
		}
		if (this.diceValue == 5)
		{
			this.SpawnFg("q4");
		}
		if (this.diceValue == 6)
		{
			this.SpawnFg("q5");
		}
		if (this.diceValue == 7)
		{
			this.SpawnFg("q6");
		}
		if (this.diceValue == 8)
		{
			this.SpawnFg("f7");
		}
	}

	private void SpawnFg(string fgName)
	{
		this.diceItem = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(fgName), base.transform.position - Vector3.up * 0.25f, Quaternion.identity);
		if (fgName == "q1")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.55f;
		}
		if (fgName == "q2")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.55f;
		}
		if (fgName == "q3")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.5f;
		}
		if (fgName == "q4")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.53f;
		}
		if (fgName == "q5")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.5f;
		}
		if (fgName == "q6")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.48f;
		}
		if (fgName == "q0")
		{
			this.diceItem.transform.localScale = Vector3.one * 0.54f;
		}
		this.diceItem.GetComponent<SkeletonAnimation>().state.SetAnimation(0, "HC", false);
		this.diceItem.transform.parent = base.transform;
	}

	private void DestroyPreviousGraphic()
	{
		UnityEngine.Object.Destroy(this.diceItem.gameObject);
		this.currentBackground = null;
		this.diceItem = null;
	}

	private IEnumerator lerpToTargetColor(int targetColorValue)
	{
		Dice._lerpToTargetColor_c__Iterator8 _lerpToTargetColor_c__Iterator = new Dice._lerpToTargetColor_c__Iterator8();
		_lerpToTargetColor_c__Iterator.targetColorValue = targetColorValue;
		_lerpToTargetColor_c__Iterator.___targetColorValue = targetColorValue;
		_lerpToTargetColor_c__Iterator.__f__this = this;
		return _lerpToTargetColor_c__Iterator;
	}

	private Color GetColorByValue(int value)
	{
		return GameManager.instance.diceColorList[value - 1];
	}

	private IEnumerator IncreaseGraphicEffect()
	{
		Dice._IncreaseGraphicEffect_c__Iterator9 _IncreaseGraphicEffect_c__Iterator = new Dice._IncreaseGraphicEffect_c__Iterator9();
		_IncreaseGraphicEffect_c__Iterator.__f__this = this;
		return _IncreaseGraphicEffect_c__Iterator;
	}

	private int GetRandomDiceValue()
	{
		int num = UnityEngine.Random.Range(DicePointHandler.instance.minumDiceScore, DicePointHandler.instance.DiceScore + 1);
		float num2 = 80f;
		if (GameManager.instance.spiderProbability != 0)
		{
			num2 = (float)GameManager.instance.spiderProbability;
		}
		if (num == DicePointHandler.instance.GetMaxDiceScore())
		{
			int num3 = UnityEngine.Random.Range(0, 101);
			if ((float)num3 <= num2)
			{
				num = UnityEngine.Random.Range(DicePointHandler.instance.minumDiceScore, DicePointHandler.instance.DiceScore);
			}
		}
		return num;
	}

	public IEnumerator spawnAnimation()
	{
		Dice._spawnAnimation_c__IteratorA _spawnAnimation_c__IteratorA = new Dice._spawnAnimation_c__IteratorA();
		_spawnAnimation_c__IteratorA.__f__this = this;
		return _spawnAnimation_c__IteratorA;
	}
}
