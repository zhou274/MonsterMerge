using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
	private sealed class _startEffect_c__IteratorE : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal SpriteRenderer[] sprites;

		internal SpriteRenderer[] __s_116___0;

		internal int __s_117___1;

		internal SpriteRenderer _sprite___2;

		internal int _PC;

		internal object _current;

		internal SpriteRenderer[] ___sprites;

		internal ExplosionEffect __f__this;

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
				this.__f__this.StartCoroutine(this.__f__this.enlarge());
				this.__s_116___0 = this.sprites;
				this.__s_117___1 = 0;
				break;
			case 1u:
				this.__s_117___1++;
				break;
			case 2u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this.__s_117___1 >= this.__s_116___0.Length)
			{
				this._current = null;
				this._PC = 2;
			}
			else
			{
				this._sprite___2 = this.__s_116___0[this.__s_117___1];
				this.__f__this.StartCoroutine(this.__f__this.duplicate(this._sprite___2));
				this._current = new WaitForSeconds(this.__f__this.duplicateEachXSeconds);
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

	private sealed class _enlarge_c__IteratorF : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Transform _objectToEnlarge___0;

		internal Vector3 _objectTargetScale___1;

		internal float _lerper___2;

		internal int _PC;

		internal object _current;

		internal ExplosionEffect __f__this;

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
				this._objectToEnlarge___0 = this.__f__this.tileToEnlarge;
				this._objectTargetScale___1 = this._objectToEnlarge___0.localScale;
				this._objectToEnlarge___0.localScale = Vector3.zero;
				this._lerper___2 = 0f;
				break;
			case 1u:
				break;
			case 2u:
				goto IL_143;
			case 3u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._lerper___2 < 1f)
			{
				this._lerper___2 += Time.deltaTime / (this.__f__this.secondsToCompleteEnlarge / 2f);
				this._objectToEnlarge___0.localScale = Vector3.Lerp(Vector3.zero, this._objectTargetScale___1, this._lerper___2);
				this._current = new WaitForEndOfFrame();
				this._PC = 1;
				return true;
			}
			this._lerper___2 = 0f;
			IL_143:
			if (this._lerper___2 >= 1f)
			{
				UnityEngine.Object.Destroy(this.__f__this.tileToEnlarge.gameObject);
				this._current = null;
				this._PC = 3;
				return true;
			}
			this._lerper___2 += Time.deltaTime / (this.__f__this.secondsToCompleteEnlarge / 2f);
			this._objectToEnlarge___0.localScale = Vector3.Lerp(this._objectTargetScale___1, Vector3.zero, this._lerper___2);
			this._current = new WaitForEndOfFrame();
			this._PC = 2;
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

	private sealed class _duplicate_c__Iterator10 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _lerper___0;

		internal SpriteRenderer sprite;

		internal int _PC;

		internal object _current;

		internal SpriteRenderer ___sprite;

		internal ExplosionEffect __f__this;

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
				this._lerper___0 = 0f;
				break;
			case 1u:
				break;
			case 2u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this._lerper___0 > 1f)
			{
				this.sprite.enabled = false;
				this._current = null;
				this._PC = 2;
			}
			else
			{
				this._lerper___0 += Time.deltaTime / this.__f__this.secondsToCompleteScale;
				this.sprite.transform.localScale = Vector3.Lerp(Vector3.zero, this.__f__this.targetscale, this._lerper___0);
				this.sprite.color = Color.Lerp(this.sprite.color, this.__f__this.alphaColor, this._lerper___0);
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

	public int duplicateXTimes = 5;

	public float duplicateEachXSeconds = 0.2f;

	public float secondsToCompleteScale = 0.65f;

	public float secondsToCompleteEnlarge = 0.5f;

	public SpriteRenderer spriteToDuplicate;

	private SpriteRenderer[] spritesGenerated;

	public Transform tileToEnlarge;

	private Color alphaColor;

	private float destructionTime = 3f;

	public Color[] WaveColorList;

	private int colorIndex;

	private Vector3 targetscale;

	private void Start()
	{
		this.alphaColor = this.spriteToDuplicate.color;
		this.alphaColor.a = 0.5f;
		this.targetscale = this.spriteToDuplicate.transform.localScale;
		this.spritesGenerated = new SpriteRenderer[this.duplicateXTimes];
		for (int i = 0; i < this.spritesGenerated.Length; i++)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.spriteToDuplicate.gameObject, this.spriteToDuplicate.transform.position, this.spriteToDuplicate.transform.rotation);
			SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
			component.material.color = this.CycleThroughColorList();
			this.spritesGenerated[i] = component;
			gameObject.transform.parent = base.transform;
			gameObject.transform.localScale = Vector3.zero;
		}
		this.spriteToDuplicate.enabled = false;
		base.StartCoroutine(this.startEffect(this.spritesGenerated));
	}

	private Color CycleThroughColorList()
	{
		Color result = this.WaveColorList[this.colorIndex];
		this.colorIndex++;
		if (this.colorIndex >= this.WaveColorList.Length)
		{
			this.colorIndex = 0;
		}
		return result;
	}

	private void Update()
	{
		this.destructionTime -= Time.deltaTime;
		if (this.destructionTime <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private IEnumerator startEffect(SpriteRenderer[] sprites)
	{
		ExplosionEffect._startEffect_c__IteratorE _startEffect_c__IteratorE = new ExplosionEffect._startEffect_c__IteratorE();
		_startEffect_c__IteratorE.sprites = sprites;
		_startEffect_c__IteratorE.___sprites = sprites;
		_startEffect_c__IteratorE.__f__this = this;
		return _startEffect_c__IteratorE;
	}

	private IEnumerator enlarge()
	{
		ExplosionEffect._enlarge_c__IteratorF _enlarge_c__IteratorF = new ExplosionEffect._enlarge_c__IteratorF();
		_enlarge_c__IteratorF.__f__this = this;
		return _enlarge_c__IteratorF;
	}

	private IEnumerator duplicate(SpriteRenderer sprite)
	{
		ExplosionEffect._duplicate_c__Iterator10 _duplicate_c__Iterator = new ExplosionEffect._duplicate_c__Iterator10();
		_duplicate_c__Iterator.sprite = sprite;
		_duplicate_c__Iterator.___sprite = sprite;
		_duplicate_c__Iterator.__f__this = this;
		return _duplicate_c__Iterator;
	}
}
