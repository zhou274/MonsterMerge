using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeSkill : MonoBehaviour
{
	private sealed class _DropDices_c__IteratorD : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___0;

		internal Dice[] dices;

		internal DistancedTile[] tileDrop;

		internal Vector3 _dropPosition___1;

		internal int _i___2;

		internal int _PC;

		internal object _current;

		internal Dice[] ___dices;

		internal DistancedTile[] ___tileDrop;

		internal ExplodeSkill __f__this;

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
				this.__f__this.isDropping = true;
				this._i___0 = 0;
				while (this._i___0 < this.dices.Length)
				{
					this._dropPosition___1 = this.tileDrop[this._i___0].tile.transform.position;
					this._dropPosition___1.z = this.__f__this.dropZoffset;
					this.dices[this._i___0].transform.position = this._dropPosition___1;
					this.dices[this._i___0].transform.parent = null;
					this.tileDrop[this._i___0].tile.tileValue = this.dices[this._i___0].diceValue;
					this.tileDrop[this._i___0].tile.placedDice = this.dices[this._i___0];
					this._i___0++;
				}
				this._i___2 = 0;
				while (this._i___2 < this.dices.Length)
				{
					this.dices[this._i___2].transform.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
					this._i___2++;
				}
				this._current = this.__f__this.StartCoroutine(Merger.instance.CheckIfMergeable(this.tileDrop));
				this._PC = 1;
				return true;
			case 1u:
				this.__f__this.isDropping = false;
				this._current = null;
				this._PC = 2;
				return true;
			case 2u:
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

	public GameObject skillPerfab;

	public static ExplodeSkill instance;

	private bool canRelease;

	public GameObject maskLayer;

	public GameObject explodeAnimation;

	public Sprite spriteBackground;

	public Sprite originSpriteBackground;

	public GameObject tip;

	public SkeletonGraphic trashImage;

	public SkeletonGraphic hammerImage;

	public Image cup;

	public Image coins;

	public SkeletonGraphic topBackground;

	public Image logo;

	public Button pauseButton;

	public Image trashImageCoins;

	public Image hammerImageCoins;

	public Text cupText;

	public Text coinText;

	public Text scoreText;

	public Color color1;

	public Color color2;

	public Text trashText;

	public Text HammerText;

	public Text skillCountText;

	private Vector3 skillButtonStartPosition;

	private float dragOffset;

	private float dragUpperOffsetPercentage = 10f;

	private float dragUpperOffset;

	private float grabMinDistance = 0.8f;

	private float dragAnimationTime = 0.08f;

	private bool isDropping;

	private Vector3 rotationVector;

	private Vector3 rotationVectorGraphic;

	private Vector3 targetRotation;

	private Vector3 targetRotationGraphic;

	private float rotationEffectTime = 0.35f;

	private float rotationLerper;

	private List<GridTile> AllRowTile;

	private bool Moved;

	private bool canDrag;

	private float dropZoffset = -0.01f;

	private Vector3 grabStartingMousePosition;

	private void Awake()
	{
		this.skillButtonStartPosition = Camera.main.WorldToScreenPoint(base.transform.position);
		ExplodeSkill.instance = this;
	}

	private void Start()
	{
		ScoreHandler.instance.LoadFristUseExplodeSkil();
		float num = (float)Screen.height;
		this.dragUpperOffset = num / 100f * this.dragUpperOffsetPercentage;
		this.rotationVector = base.transform.localEulerAngles;
		this.targetRotation = this.rotationVector;
		if (GameManager.instance.skillCount > 0)
		{
			if (ScoreHandler.instance.fristUseExplodeSkill == "true")
			{
				this.tip.SetActive(false);
			}
			else
			{
				this.tip.SetActive(true);
			}
		}
	}

	private bool MouseNearTheCompound()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		a.z = 0f;
		Vector3 b = Camera.main.ScreenToWorldPoint(this.skillButtonStartPosition);
		b.z = 0f;
		float num = Vector3.Distance(a, b);
		return num <= 1f;
	}

	private void Update()
	{
		if (Camera.main.transform.position != CameraManager.instance.originalPosition)
		{
			return;
		}
		if (GameManager.instance.gamePaused)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (this.MouseNearTheCompound())
			{
				this.canDrag = true;
			}
			else
			{
				this.canDrag = false;
			}
			this.grabStartingMousePosition = UnityEngine.Input.mousePosition;
		}
		if (!this.canDrag)
		{
			return;
		}
		if (!GameManager.instance.canDragSkill)
		{
			return;
		}
		if (Input.GetMouseButton(0))
		{
			if (this.MouseNearTheCompound() || this.Moved)
			{
				if (GameManager.instance.skillCount > 0)
				{
					this.AddSkillAnimation(true);
					this.DragCompound();
				}
			}
			else
			{
				UnityEngine.Debug.LogError("dasasdada");
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.canRelease)
			{
				this.ReleaseSkill();
			}
			else
			{
				this.ResetCompoundPosition();
			}
		}
	}

	public void ResetCompoundPosition()
	{
		this.ResetAllRowLayer();
		Hashtable hashtable = new Hashtable();
		hashtable.Add("position", Camera.main.ScreenToWorldPoint(this.skillButtonStartPosition));
		hashtable.Add("easeType", iTween.EaseType.linear);
		hashtable.Add("speed", 40);
		this.hideAllUI(false);
		iTween.MoveTo(base.gameObject, hashtable);
	}

	public void ResetPosition()
	{
		this.Moved = false;
		this.dragOffset = 0f;
		base.transform.position = Camera.main.ScreenToWorldPoint(this.skillButtonStartPosition);
	}

	private IEnumerator DropDices(Dice[] dices, DistancedTile[] tileDrop)
	{
		ExplodeSkill._DropDices_c__IteratorD _DropDices_c__IteratorD = new ExplodeSkill._DropDices_c__IteratorD();
		_DropDices_c__IteratorD.dices = dices;
		_DropDices_c__IteratorD.tileDrop = tileDrop;
		_DropDices_c__IteratorD.___dices = dices;
		_DropDices_c__IteratorD.___tileDrop = tileDrop;
		_DropDices_c__IteratorD.__f__this = this;
		return _DropDices_c__IteratorD;
	}

	private void DragCompound()
	{
		this.hideAllUI(true);
		this.hammerImage.CrossFadeColor(this.color1, 0.1f, false, true);
		this.hammerImageCoins.CrossFadeAlpha(0f, 0.2f, false);
		this.HammerText.gameObject.SetActive(false);
		ScoreHandler.instance.LoadFristUseExplodeSkil();
		if (ScoreHandler.instance.fristUseExplodeSkill != "true")
		{
			GUIManager.instance.OpenMask();
			GUIManager.instance.ShowExplodeSkill();
			this.tip.SetActive(false);
		}
		else
		{
			Merger.instance.f8Perfabs.GetComponent<SkeletonAnimation>().state.SetAnimation(0, "click", false);
			this.AttentionAllRow();
			this.PromoteAllRowLayer();
			this.Moved = true;
			Vector3 position = this.skillButtonStartPosition + UnityEngine.Input.mousePosition - this.grabStartingMousePosition + new Vector3(0f, this.dragOffset, 0f);
			position.z = this.skillButtonStartPosition.z;
			if (Vector3.Distance(Camera.main.ScreenToWorldPoint(position), Camera.main.ScreenToWorldPoint(this.skillButtonStartPosition)) > this.grabMinDistance)
			{
				this.dragOffset = Mathf.MoveTowards(this.dragOffset, this.dragUpperOffset + (this.grabStartingMousePosition.y - this.skillButtonStartPosition.y), Time.deltaTime * this.dragUpperOffset / this.dragAnimationTime);
			}
			base.transform.position = Camera.main.ScreenToWorldPoint(position);
		}
	}

	private void AttentionAllRow()
	{
		this.AllRowTile = new List<GridTile>();
		DistancedTile distancedTile = new DistancedTile();
		distancedTile = FindNearestTile.find(base.transform.position);
		if (base.transform.position.x > -3f && base.transform.position.x < 3f && base.transform.position.y > -2f && base.transform.position.y < 4f)
		{
			this.canRelease = true;
			for (int i = 0; i < GridMap.instance.tiles.Length; i++)
			{
				GridTile gridTile = GridMap.instance.tiles[i];
				if (gridTile.rowIndex == distancedTile.tile.rowIndex)
				{
					this.AllRowTile.Add(gridTile);
					gridTile.gameObject.transform.GetComponent<Transform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
					gridTile.gameObject.transform.GetComponent<SpriteRenderer>().sortingOrder = 3;
				}
				else
				{
					if (gridTile.placedDice != null)
					{
						gridTile.placedDice.GetComponent<SpriteRenderer>().sortingOrder = 1;
					}
					gridTile.gameObject.transform.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
					gridTile.gameObject.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
				}
			}
		}
		else
		{
			this.canRelease = false;
			for (int j = 0; j < GridMap.instance.tiles.Length; j++)
			{
				GridMap.instance.tiles[j].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
			}
			this.AddSkillAnimation(false);
		}
	}

	private void AttentionDice()
	{
		foreach (GridTile current in this.AllRowTile)
		{
			if (current.placedDice == null)
			{
				current.GetComponent<SpriteRenderer>().sortingOrder = 3;
			}
			else
			{
				current.placedDice.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
			}
		}
	}

	private void ReleaseSkill()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.explosion);
		this.AddSkillAnimation(false);
		this.ResetAllRowLayer();
		this.ResetSkillPosition();
		this.hammerImage.CrossFadeColor(this.color2, 0.5f, false, true);
		this.hammerImageCoins.CrossFadeAlpha(1f, 1f, false);
		this.HammerText.gameObject.SetActive(true);
		foreach (GridTile current in this.AllRowTile)
		{
			if (current.placedDice != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(this.skillPerfab, current.gameObject.transform.position, Quaternion.identity) as GameObject;
				gameObject.GetComponent<SkeletonAnimation>().state.SetAnimation(0, "explode", false);
				UnityEngine.Object.Destroy(gameObject, 2f);
			}
			ScoreHandler.instance.increaseScore(current.tileValue);
			current.Reset();
			current.gameObject.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
		}
		this.hideAllUI(false);
		GameManager.instance.skillCount--;
		InGameGUI.instance.RefreshSkillCount();
	}

	private void ResetSkillPosition()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(this.skillButtonStartPosition);
		base.transform.position = position;
	}

	private void PromoteAllRowLayer()
	{
		foreach (GridTile current in this.AllRowTile)
		{
			current.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
			if (current.placedDice == null)
			{
				current.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
			}
			else
			{
				current.placedDice.GetComponent<SpriteRenderer>().sortingOrder = 3;
				current.placedDice.diceItem.gameObject.GetComponent<MeshRenderer>().sortingOrder = 3;
			}
		}
	}

	private void ResetAllRowLayer()
	{
		GridTile[] tiles = GridMap.instance.tiles;
		for (int i = 0; i < tiles.Length; i++)
		{
			GridTile gridTile = tiles[i];
			gridTile.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
			if (gridTile.placedDice != null)
			{
				gridTile.placedDice.GetComponent<SpriteRenderer>().sortingOrder = 1;
				gridTile.placedDice.diceItem.gameObject.GetComponent<MeshRenderer>().sortingOrder = 1;
			}
		}
	}

	public void AddSkillAnimation(bool state)
	{
		this.maskLayer.SetActive(state);
	}

	private bool draggedOut()
	{
		return this.dragOffset > 0f;
	}

	public void hideAllUI(bool state)
	{
		GameManager.instance.isDragging = !state;
		if (state)
		{
			this.cup.CrossFadeAlpha(0f, 0.2f, false);
			this.coins.CrossFadeAlpha(0f, 0.2f, false);
			this.logo.CrossFadeAlpha(0f, 0.2f, false);
			this.trashImageCoins.CrossFadeAlpha(0f, 0.2f, false);
			this.pauseButton.GetComponent<Image>().CrossFadeAlpha(0f, 0.2f, false);
			this.topBackground.CrossFadeColor(this.color1, 0.1f, false, true);
			this.trashImage.CrossFadeColor(this.color1, 0.1f, false, true);
			this.coinText.gameObject.SetActive(!state);
			this.cupText.gameObject.SetActive(!state);
			this.skillCountText.gameObject.SetActive(!state);
			this.scoreText.gameObject.SetActive(!state);
			this.trashText.gameObject.SetActive(!state);
		}
		else
		{
			this.cup.CrossFadeAlpha(1f, 1f, false);
			this.coins.CrossFadeAlpha(1f, 1f, false);
			this.logo.CrossFadeAlpha(1f, 2f, false);
			this.topBackground.CrossFadeColor(this.color2, 0.5f, false, true);
			this.trashImage.CrossFadeColor(this.color2, 0.5f, false, true);
			this.pauseButton.GetComponent<Image>().CrossFadeAlpha(1f, 0.2f, false);
			this.trashImageCoins.CrossFadeAlpha(1f, 1f, false);
			this.coinText.gameObject.SetActive(!state);
			this.skillCountText.gameObject.SetActive(!state);
			this.trashText.gameObject.SetActive(!state);
			this.scoreText.gameObject.SetActive(!state);
			this.cupText.gameObject.SetActive(!state);
		}
	}
}
