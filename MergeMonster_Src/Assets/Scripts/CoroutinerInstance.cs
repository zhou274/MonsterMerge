using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoroutinerInstance : MonoBehaviour
{
	private sealed class _DestroyWhenComplete_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal IEnumerator iterationResult;

		internal int _PC;

		internal object _current;

		internal IEnumerator ___iterationResult;

		internal CoroutinerInstance __f__this;

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
				this._current = this.__f__this.StartCoroutine(this.iterationResult);
				this._PC = 1;
				return true;
			case 1u:
				UnityEngine.Object.Destroy(this.__f__this.gameObject);
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

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	public Coroutine ProcessWork(IEnumerator iterationResult)
	{
		return base.StartCoroutine(this.DestroyWhenComplete(iterationResult));
	}

	public IEnumerator DestroyWhenComplete(IEnumerator iterationResult)
	{
		CoroutinerInstance._DestroyWhenComplete_c__Iterator0 _DestroyWhenComplete_c__Iterator = new CoroutinerInstance._DestroyWhenComplete_c__Iterator0();
		_DestroyWhenComplete_c__Iterator.iterationResult = iterationResult;
		_DestroyWhenComplete_c__Iterator.___iterationResult = iterationResult;
		_DestroyWhenComplete_c__Iterator.__f__this = this;
		return _DestroyWhenComplete_c__Iterator;
	}
}
