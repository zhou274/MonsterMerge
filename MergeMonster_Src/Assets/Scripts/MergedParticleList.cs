using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MergedParticleList
{
	private sealed class _LerpParticlesColor_c__Iterator18 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal float _lerper___0;

		internal int _PC;

		internal object _current;

		internal MergedParticleList __f__this;

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
			if (this._lerper___0 == 1f)
			{
				this._current = null;
				this._PC = 2;
			}
			else
			{
				this._lerper___0 += Time.deltaTime;
				GameManager.instance.particleMaterial.color = Color.Lerp(GameManager.instance.diceColorList[this.__f__this.startingColorIndex], GameManager.instance.diceColorList[this.__f__this.startingColorIndex + 1], this._lerper___0);
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

	public List<MergedParticle> mergedParticlesList;

	private int startingColorIndex;

	public MergedParticleList()
	{
		this.New();
	}

	private void New()
	{
		this.mergedParticlesList = new List<MergedParticle>(0);
	}

	public void Add(MergedParticle particle)
	{
		this.mergedParticlesList.Add(particle);
	}

	public void Remove(MergedParticle particle)
	{
		this.mergedParticlesList.Remove(particle);
	}

	public void SetParticlesColor(int colorIndex)
	{
		this.startingColorIndex = colorIndex - 2;
		GameManager.instance.particleMaterial.color = GameManager.instance.diceColorList[this.startingColorIndex];
	}

	public IEnumerator LerpParticlesColor()
	{
		MergedParticleList._LerpParticlesColor_c__Iterator18 _LerpParticlesColor_c__Iterator = new MergedParticleList._LerpParticlesColor_c__Iterator18();
		_LerpParticlesColor_c__Iterator.__f__this = this;
		return _LerpParticlesColor_c__Iterator;
	}
}
