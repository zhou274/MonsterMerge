using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DiceCompound : MonoBehaviour
{
	private sealed class _DropCompound_c__IteratorB : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal DistancedTile[] _nearTiles___0;

		internal int _i___1;

		internal int _PC;

		internal object _current;

		internal DiceCompound __f__this;

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
				this._nearTiles___0 = new DistancedTile[this.__f__this.dices.Length];
				this._i___1 = 0;
				while (this._i___1 < this.__f__this.dices.Length)
				{
					this._nearTiles___0[this._i___1] = FindNearestTile.find(this.__f__this.dices[this._i___1].transform.position);
					this._i___1++;
				}
				if (this.__f__this.dices.Length == 1)
				{
					if (this._nearTiles___0[0].tile.isEmpty() && this._nearTiles___0[0].distance <= this.__f__this.minDropDistance)
					{
						SoundsManager.instance.PlayAudioSource(SoundsManager.instance.born);
						this._current = this.__f__this.StartCoroutine(this.__f__this.DropDices(this.__f__this.dices, this._nearTiles___0));
						this._PC = 1;
						return true;
					}
				}
				else if (this._nearTiles___0[0].tile != this._nearTiles___0[1].tile && this._nearTiles___0[0].tile.isEmpty() && this._nearTiles___0[1].tile.isEmpty() && this._nearTiles___0[0].distance <= this.__f__this.minDropDistance)
				{
					SoundsManager.instance.PlayAudioSource(SoundsManager.instance.born);
					this._current = this.__f__this.StartCoroutine(this.__f__this.DropDices(this.__f__this.dices, this._nearTiles___0));
					this._PC = 2;
					return true;
				}
				if (this.__f__this.draggedOut())
				{
					SoundsManager.instance.PlayAudioSource(SoundsManager.instance.wrongDrop);
					this.__f__this.ResetCompoundPosition();
				}
				this._current = null;
				this._PC = 3;
				return true;
			case 1u:
				this.__f__this.ResetRotationTarget();
				if (this.__f__this.isDroped)
				{
					DiceCompoundSpawner.instance.SpawnDices(1);
				}
				break;
			case 2u:
				this.__f__this.ResetRotationTarget();
				if (this.__f__this.isDroped)
				{
					DiceCompoundSpawner.instance.SpawnDices(1);
				}
				this.__f__this.ResetPosition();
				break;
			case 3u:
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

	private sealed class _DropDices_c__IteratorC : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___0;

		internal Dice[] dices;

		internal DistancedTile[] tileDrop;

		internal Vector3 _dropPosition___1;

		internal int _PC;

		internal object _current;

		internal Dice[] ___dices;

		internal DistancedTile[] ___tileDrop;

		internal DiceCompound __f__this;

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
				if (this.__f__this.canDrop)
				{
					this._i___0 = 0;
					while (this._i___0 < this.dices.Length)
					{
						this._dropPosition___1 = this.tileDrop[this._i___0].tile.transform.position;
						this._dropPosition___1.z = this.__f__this.dropZoffset;
						this.dices[this._i___0].transform.position = this._dropPosition___1;
						this.dices[this._i___0].transform.parent = null;
						this.dices[this._i___0].tag = "Dice";
						this.tileDrop[this._i___0].tile.tileValue = this.dices[this._i___0].diceValue;
						this.tileDrop[this._i___0].tile.placedDice = this.dices[this._i___0];
						this._i___0++;
					}
					this.__f__this.isDroped = true;
					this._current = this.__f__this.StartCoroutine(Merger.instance.CheckIfMergeable(this.tileDrop));
					this._PC = 1;
					return true;
				}
				this.__f__this.isDroped = false;
				this.__f__this.ResetCompoundPosition();
				break;
			case 1u:
				this.__f__this.isDropping = false;
				this._current = null;
				this._PC = 2;
				return true;
			case 2u:
				break;
			default:
				return false;
			}
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

	public ParticleSystem explodeEffect;

	public GameObject dustbin;

	public Dice[] dices;

	public static DiceCompound instance;

	public Transform topPosition;

	public Button trashButton;

	private int compoundType;

	private bool isCompleteAnnimation;

	private Vector3 compoundStartPosition;

	private float dragOffset;

	private float dragUpperOffsetPercentage = 10f;

	private float dragUpperOffset;

	private float grabMinDistance = 0.8f;

	private float dragAnimationTime = 0.08f;

	private bool canDrop;

	private bool isDroped;

	private Vector3 rotationVector;

	private Vector3 rotationVectorGraphic;

	private Vector3 targetRotation;

	private Vector3 targetRotationGraphic;

	private float rotationEffectTime = 0.3f;

	private float rotationLerper;

	public Transform[] path;

	public bool debugClear;

	private bool Moved;

	private bool isDropping;

	private bool canDrag;

	private float dropZoffset = -0.01f;

	private float minDropDistance = 0.45f;

	private Vector3 grabStartingMousePosition;

	private void Awake()
	{
		DiceCompound.instance = this;
	}

	private void Start()
	{
		float num = (float)Screen.height;
		this.dragUpperOffset = num / 100f * this.dragUpperOffsetPercentage;
		this.compoundStartPosition = Camera.main.WorldToScreenPoint(base.transform.position);
		this.rotationVector = base.transform.localEulerAngles;
		this.targetRotation = this.rotationVector;
	}

	private void LateUpdate()
	{
		if (this.isDoubleDice())
		{
			if (this.draggedOut())
			{
				this.EnableRotationEffect(false);
			}
		}
		else
		{
			this.EnableRotationEffect(false);
		}
	}

	private bool MouseNearTheCompound()
	{
		Vector3 a = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
		a.z = 0f;
		Vector3 b = Camera.main.ScreenToWorldPoint(this.compoundStartPosition);
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
		if (this.rotationVector != this.targetRotation)
		{
			this.RotateCompound();
		}
		if (this.rotationVector == this.targetRotation)
		{
			this.canDrop = true;
		}
		if (this.isDropping)
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
		if (Input.GetMouseButton(0))
		{
			if (!this.MouseNearTheCompound() && !this.Moved)
			{
				return;
			}
			this.DragCompound();
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (!this.draggedOut() && this.isDoubleDice())
			{
				this.TriggerRotationEffect();
				return;
			}
			base.StartCoroutine(this.DropCompound());
		}
	}

	private void TriggerRotationEffect()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.rotataion);
		this.targetRotation.z = this.targetRotation.z + 90f;
		this.targetRotationGraphic.z = this.targetRotationGraphic.z - 90f;
		this.rotationLerper = 0f;
	}

	private void ResetRotationTarget()
	{
		base.transform.rotation = Quaternion.identity;
		this.rotationVector = Vector3.zero;
		this.rotationVectorGraphic = Vector3.zero;
		this.targetRotation = Vector3.zero;
		this.targetRotationGraphic = Vector3.zero;
	}

	public void RotateCompound()
	{
		this.canDrop = false;
		this.rotationVector = Vector3.Lerp(this.rotationVector, this.targetRotation, this.rotationLerper);
		this.rotationVectorGraphic = Vector3.Lerp(this.rotationVectorGraphic, this.targetRotationGraphic, this.rotationLerper);
		this.rotationLerper += Time.deltaTime / this.rotationEffectTime;
		base.transform.eulerAngles = this.rotationVector;
		Dice[] array = this.dices;
		for (int i = 0; i < array.Length; i++)
		{
			Dice dice = array[i];
			dice.transform.localEulerAngles = this.rotationVectorGraphic;
		}
	}

	public void FindDicesInCompound()
	{
		this.dices = new Dice[0];
		this.dices = base.GetComponentsInChildren<Dice>();
	}

	private IEnumerator DropCompound()
	{
		DiceCompound._DropCompound_c__IteratorB _DropCompound_c__IteratorB = new DiceCompound._DropCompound_c__IteratorB();
		_DropCompound_c__IteratorB.__f__this = this;
		return _DropCompound_c__IteratorB;
	}

	private void FlyComplete()
	{
		this.ResetCompoundPosition();
		this.ResetRotationTarget();
		this.trashButton.interactable = true;
		GameManager.instance.gamePaused = false;
		Dice[] array = this.dices;
		for (int i = 0; i < array.Length; i++)
		{
			Dice dice = array[i];
			dice.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(dice);
			UnityEngine.Object.Destroy(dice.gameObject);
		}
		this.dices = new Dice[0];
		DiceCompoundSpawner.instance.SpawnDices(this.compoundType);
	}

	public void RespawnDiceCompound(int type)
	{
		this.compoundType = type;
		if (type == 0)
		{
			Dice[] array = this.dices;
			for (int i = 0; i < array.Length; i++)
			{
				Dice dice = array[i];
				dice.transform.gameObject.GetComponent<SpriteRenderer>().sprite = null;
				GameObject obj = UnityEngine.Object.Instantiate(this.explodeEffect.gameObject, dice.transform.position, dice.transform.localRotation) as GameObject;
				UnityEngine.Object.Destroy(obj, 1f);
			}
			this.dustbin.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "eat", false);
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			hashtable2.Add("rotation", new Vector3(0f, 0f, 90f));
			hashtable2.Add("time", 0.5f);
			hashtable.Add("path", this.path);
			hashtable.Add("position", Camera.main.ScreenToWorldPoint(this.topPosition.position));
			hashtable.Add("easeType", iTween.EaseType.linear);
			hashtable.Add("speed", 15);
			hashtable.Add("oncomplete", "FlyComplete");
			iTween.MoveTo(base.gameObject, hashtable);
			iTween.RotateTo(base.gameObject, hashtable2);
		}
		else
		{
			this.ResetCompoundPosition();
			this.ResetRotationTarget();
			Dice[] array2 = this.dices;
			for (int j = 0; j < array2.Length; j++)
			{
				Dice dice2 = array2[j];
				dice2.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(dice2);
				UnityEngine.Object.Destroy(dice2.gameObject);
			}
			this.dices = new Dice[0];
			DiceCompoundSpawner.instance.SpawnDices(type);
		}
	}

	public void RespawnDice(int TYPE)
	{
		for (int i = 0; i < this.dices.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(this.dices[i].gameObject);
		}
		DiceCompoundSpawner.instance.SpawnDices(TYPE);
		this.trashButton.interactable = true;
	}

	public void ResetCompoundPosition()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("position", Camera.main.ScreenToWorldPoint(this.compoundStartPosition));
		hashtable.Add("easeType", iTween.EaseType.linear);
		hashtable.Add("speed", 40);
		hashtable.Add("oncomplete", "CleanData");
		iTween.MoveTo(base.gameObject, hashtable);
	}

	public void ResetPosition()
	{
		this.Moved = false;
		this.dragOffset = 0f;
		base.transform.position = Camera.main.ScreenToWorldPoint(this.compoundStartPosition);
	}

	private void CleanData()
	{
		this.Moved = false;
		this.dragOffset = 0f;
		this.EnableRotationEffect(true);
	}

	private IEnumerator DropDices(Dice[] dices, DistancedTile[] tileDrop)
	{
		DiceCompound._DropDices_c__IteratorC _DropDices_c__IteratorC = new DiceCompound._DropDices_c__IteratorC();
		_DropDices_c__IteratorC.dices = dices;
		_DropDices_c__IteratorC.tileDrop = tileDrop;
		_DropDices_c__IteratorC.___dices = dices;
		_DropDices_c__IteratorC.___tileDrop = tileDrop;
		_DropDices_c__IteratorC.__f__this = this;
		return _DropDices_c__IteratorC;
	}

	private void DragCompound()
	{
		this.Moved = true;
		Vector3 position = this.compoundStartPosition + UnityEngine.Input.mousePosition - this.grabStartingMousePosition + new Vector3(0f, this.dragOffset, 0f);
		position.z = this.compoundStartPosition.z;
		if (Vector3.Distance(Camera.main.ScreenToWorldPoint(position), Camera.main.ScreenToWorldPoint(this.compoundStartPosition)) > this.grabMinDistance)
		{
			this.dragOffset = Mathf.MoveTowards(this.dragOffset, this.dragUpperOffset + (this.grabStartingMousePosition.y - this.compoundStartPosition.y), Time.deltaTime * this.dragUpperOffset / this.dragAnimationTime);
		}
		base.transform.position = Camera.main.ScreenToWorldPoint(position);
	}

	private bool isDoubleDice()
	{
		return this.dices.Length > 1;
	}

	private void EnableRotationEffect(bool value)
	{
		DiceCompoundSpawner.instance.SetRotatingSwitch(value);
	}

	private bool draggedOut()
	{
		return this.dragOffset > 0f;
	}
}
