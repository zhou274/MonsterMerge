using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MergedParticle : MonoBehaviour
{
	private sealed class _mergeEffect_c__Iterator16 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

		internal MergedParticle __f__this;

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
			//IL_D0:
				if (this.__f__this.transform.position != this.__f__this.tilesToFollowList[0].transform.position - Vector3.forward * 1f)
				{
					this.__f__this.transform.position = Vector3.MoveTowards(this.__f__this.transform.position, this.__f__this.tilesToFollowList[0].transform.position - Vector3.forward * 1f, this.__f__this.particleSpeed * Time.deltaTime);
					this._current = new WaitForEndOfFrame();
					this._PC = 1;
					return true;
				}
				this.__f__this.tilesToFollowList.RemoveAt(0);
				break;
			case 2u:
				this._current = null;
				this._PC = 3;
				return true;
			case 3u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (this.__f__this.tilesToFollowList.Count <= 0)
			{
				this._current = this.__f__this.StartCoroutine(this.__f__this.startDestruction());
				this._PC = 2;
				return true;
			}
			this.__f__this.tilesToFollowList[0].gameObject.GetComponent<Animation>().Play();
			goto IL_D0;

        IL_D0:
            if (this.__f__this.transform.position != this.__f__this.tilesToFollowList[0].transform.position - Vector3.forward * 1f)
            {
                this.__f__this.transform.position = Vector3.MoveTowards(this.__f__this.transform.position, this.__f__this.tilesToFollowList[0].transform.position - Vector3.forward * 1f, this.__f__this.particleSpeed * Time.deltaTime);
                this._current = new WaitForEndOfFrame();
                this._PC = 1;
                return true;
            }
            this.__f__this.tilesToFollowList.RemoveAt(0);
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

	private sealed class _startDestruction_c__Iterator17 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _PC;

		internal object _current;

		internal MergedParticle __f__this;

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
				this._current = new WaitForSeconds(this.__f__this.EffectDuration);
				this._PC = 1;
				return true;
			case 1u:
				this.__f__this.RemoveThisElementFromParentList();
				UnityEngine.Object.Destroy(this.__f__this.gameObject);
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

	public MergedParticleList parentCompound;

	private List<GridTile> tilesToFollowList;

	private float particleSpeed = 1f;

	private float EffectDuration = 0.5f;

	public ParticleSystem particleEffect;

	public void StartMergeEffect(GridTile origin)
	{
		this.findTilesToFollow(origin);
		base.StartCoroutine(this.mergeEffect());
	}

	private void findTilesToFollow(GridTile origin)
	{
		this.tilesToFollowList = new List<GridTile>(0);
		GridTile gridTile = origin;
		while (gridTile.findDirection != GridTile.FindDirection.root)
		{
			gridTile = this.findRootByTarget(gridTile);
			this.tilesToFollowList.Add(gridTile);
		}
	}

	private GridTile findRootByTarget(GridTile target)
	{
		GridTile result = null;
		if (target.findDirection == GridTile.FindDirection.left)
		{
			result = TileFinder.FindLeft(target.tileIndex, target.rowIndex);
		}
		if (target.findDirection == GridTile.FindDirection.right)
		{
			result = TileFinder.FindRight(target.tileIndex, target.rowIndex);
		}
		if (target.findDirection == GridTile.FindDirection.top)
		{
			result = TileFinder.FindTop(target.tileIndex, target.rowIndex);
		}
		if (target.findDirection == GridTile.FindDirection.bottom)
		{
			result = TileFinder.FindBottom(target.tileIndex, target.rowIndex);
		}
		if (target.findDirection == GridTile.FindDirection.root)
		{
			return result;
		}
		return result;
	}

	private IEnumerator mergeEffect()
	{
		MergedParticle._mergeEffect_c__Iterator16 _mergeEffect_c__Iterator = new MergedParticle._mergeEffect_c__Iterator16();
		_mergeEffect_c__Iterator.__f__this = this;
		return _mergeEffect_c__Iterator;
	}

	private IEnumerator startDestruction()
	{
		MergedParticle._startDestruction_c__Iterator17 _startDestruction_c__Iterator = new MergedParticle._startDestruction_c__Iterator17();
		_startDestruction_c__Iterator.__f__this = this;
		return _startDestruction_c__Iterator;
	}

	private void RemoveThisElementFromParentList()
	{
		this.parentCompound.Remove(this);
	}
}
