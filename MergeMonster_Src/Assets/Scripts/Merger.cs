using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Merger : MonoBehaviour
{
	private sealed class _CheckIfMergeable_c__Iterator19 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal DistancedTile[] tilesToCheck;

		internal DistancedTile[] __s_124___0;

		internal int __s_125___1;

		internal DistancedTile _tile___2;

		internal Vector3 _f8Position___3;

		internal Vector3[] _Paths___4;

		internal Hashtable _args___5;

		internal int _PC;

		internal object _current;

		internal DistancedTile[] ___tilesToCheck;

		internal Merger __f__this;

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
				this.__f__this.tileToCheckList = new List<GridTile>(0);
				this.__s_124___0 = this.tilesToCheck;
				this.__s_125___1 = 0;
				while (this.__s_125___1 < this.__s_124___0.Length)
				{
					this._tile___2 = this.__s_124___0[this.__s_125___1];
					this.__f__this.tileToCheckList.Add(this._tile___2.tile);
					this.__s_125___1++;
				}
				this.__f__this.tileToCheckList = this.__f__this.ReOrderTiletocheckList(this.__f__this.tileToCheckList);
				goto IL_3E9;
			case 1u:
				if (this.__f__this.mergedParticleList.mergedParticlesList.Count > 0)
				{
					this.__f__this.StartParticleEffectColor();
				}
				break;
			case 2u:
				break;
			case 3u:
				goto IL_19E;
			default:
				return false;
			}
			if (this.__f__this.mergedParticleList.mergedParticlesList.Count != 0)
			{
				this._current = new WaitForEndOfFrame();
				this._PC = 2;
				return true;
			}
			if (!(this.__f__this.lastCheckedTile != null))
			{
				goto IL_1B8;
			}
			IL_19E:
			if (this.__f__this.lastCheckedTile.placedDice.isEnlarging)
			{
				this._current = new WaitForEndOfFrame();
				this._PC = 3;
				return true;
			}
			IL_1B8:
			if (this.__f__this.lastIncreaseValue == DicePointHandler.instance.GetMaxDiceScore() + 1)
			{
				GameManager.instance.canDragSkill = false;
				if (this.__f__this.f8Perfabs != null)
				{
					UnityEngine.Object.Destroy(this.__f__this.f8Perfabs);
				}
				GameManager.instance.skillCount++;
				InGameGUI.instance.RefreshSkillCount();
				this.__f__this.f8Perfabs = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("f8"), this.__f__this.tileToCheckList[0].transform.position, Quaternion.identity);
				this._f8Position___3 = this.__f__this.f8Perfabs.transform.position;
				this._Paths___4 = new Vector3[]
				{
					this._f8Position___3 + Vector3.up,
					this.__f__this.path[1].position
				};
				this.__f__this.f8Perfabs.transform.localScale = Vector3.one * 0.75f;
				this.__f__this.f8Perfabs.transform.parent = this.__f__this.skillPosition;
				this.__f__this.f8Perfabs.gameObject.transform.GetComponent<MeshRenderer>().sortingOrder = 5;
				this._args___5 = new Hashtable();
				this._args___5.Add("path", this._Paths___4);
				this._args___5.Add("easeType", iTween.EaseType.easeOutExpo);
				this._args___5.Add("time", 1.5f);
				this._args___5.Add("oncomplete", "OnComplete");
				iTween.MoveTo(this.__f__this.f8Perfabs, this._args___5);
				this.__f__this.ClearTile(this.__f__this.tileToCheckList[0]);
				this.__f__this.StartCoroutine(this.__f__this.WaitAnimationComplete());
				SoundsManager.instance.PlayAudioSource(SoundsManager.instance.explosion);
			}
			IL_3E9:
			if (this.__f__this.tileToCheckList.Count > 0)
			{
				this.__f__this.mergedParticleList = new MergedParticleList();
				this._current = this.__f__this.StartCoroutine(this.__f__this.TryMergeNew(this.__f__this.tileToCheckList[0]));
				this._PC = 1;
				return true;
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

	private sealed class _WaitAnimationComplete_c__Iterator1A : IEnumerator, IDisposable, IEnumerator<object>
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
				this._current = new WaitForSeconds(1.5f);
				this._PC = 1;
				return true;
			case 1u:
				GameManager.instance.canDragSkill = true;
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

	private sealed class _mergeEffect_c__Iterator1B : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GridTile tile;

		internal List<GridTile> matchingTiles;

		internal string animationType;

		internal GameObject _particleInstance___0;

		internal int _PC;

		internal object _current;

		internal GridTile ___tile;

		internal List<GridTile> ___matchingTiles;

		internal string ___animationType;

		internal Merger __f__this;

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
				if (this.tile.tileToFollow == null)
				{
					this.tile.isAnimationFinished = true;
					return false;
				}
				this._current = new WaitUntil(() => !this.__f__this.IsTileInFollowList(this.tile, this.matchingTiles));
				this._PC = 1;
				return true;
			case 1u:
				this.tile.placedDice.diceItem.GetComponent<SkeletonAnimation>().state.SetAnimation(0, this.animationType, false);
				this.tile.placedDice.GetComponent<SpriteRenderer>().enabled = false;
				this._particleInstance___0 = UnityEngine.Object.Instantiate<GameObject>(this.__f__this.particle);
				this._particleInstance___0.transform.position = this.tile.placedDice.transform.position;
				UnityEngine.Object.Destroy(this._particleInstance___0, 1f);
				break;
			case 2u:
				break;
			case 3u:
				this._PC = -1;
				return false;
			default:
				return false;
			}
			if (!(this.tile.placedDice.transform.position != this.tile.tileToFollow.transform.position - Vector3.forward * 1f))
			{
				this.tile.tileToFollow = null;
				this.tile.isAnimationFinished = true;
				this.__f__this.ClearTile(this.tile);
				this._current = null;
				this._PC = 3;
				return true;
			}
			this.tile.placedDice.transform.position = Vector3.MoveTowards(this.tile.placedDice.transform.position, this.tile.tileToFollow.transform.position - Vector3.forward * 1f, 10f * Time.deltaTime);
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

		internal bool __m__123()
		{
			return !this.__f__this.IsTileInFollowList(this.tile, this.matchingTiles);
		}
	}

	private sealed class _TryMergeNew_c__Iterator1C : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GridTile tileToCheck;

		internal List<GridTile> _matchingTiles___0;

		internal List<GridTile>.Enumerator __s_128___1;

		internal GridTile _tile___2;

		internal List<GridTile>.Enumerator __s_129___3;

		internal GridTile _tile___4;

		internal List<GridTile> _tilesToFollowList___5;

		internal GridTile _target___6;

		internal List<GridTile>.Enumerator __s_130___7;

		internal GridTile _tile___8;

		internal string _animationType___9;

		internal List<GridTile>.Enumerator __s_131___10;

		internal GridTile _tile___11;

		internal int _PC;

		internal object _current;

		internal GridTile ___tileToCheck;

		internal Merger __f__this;

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
				this._matchingTiles___0 = TileChecker.GetMatchingTileList(this.tileToCheck);
				if (this._matchingTiles___0.Count >= 3)
				{
					SoundsManager.instance.merge.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
					SoundsManager.instance.PlayAudioSource(SoundsManager.instance.merge);
					this.__s_128___1 = this._matchingTiles___0.GetEnumerator();
					try
					{
						while (this.__s_128___1.MoveNext())
						{
							this._tile___2 = this.__s_128___1.Current;
							ScoreHandler.instance.increaseScore(this._tile___2.tileValue);
						}
					}
					finally
					{
						((IDisposable)this.__s_128___1).Dispose();
					}
					ScoreHandler.instance.increaseSecondaryScore(1);
					this.tileToCheck.SetFindDirection(GridTile.FindDirection.root);
					this.__s_129___3 = this._matchingTiles___0.GetEnumerator();
					try
					{
						while (this.__s_129___3.MoveNext())
						{
							this._tile___4 = this.__s_129___3.Current;
							this._tile___4.tileToFollow = null;
							this._tilesToFollowList___5 = new List<GridTile>(0);
							this._target___6 = this._tile___4;
							while (this._target___6.findDirection != GridTile.FindDirection.root)
							{
								this._target___6 = this.__f__this.findRootByTarget(this._target___6);
								this._tilesToFollowList___5.Add(this._target___6);
							}
							if (this._tilesToFollowList___5.Count > 0)
							{
								this._tile___4.tileToFollow = this._tilesToFollowList___5[0];
							}
						}
					}
					finally
					{
						((IDisposable)this.__s_129___3).Dispose();
					}
					this.__s_130___7 = this._matchingTiles___0.GetEnumerator();
					try
					{
						while (this.__s_130___7.MoveNext())
						{
							this._tile___8 = this.__s_130___7.Current;
							this._tile___8.isAnimationFinished = false;
							this._animationType___9 = null;
							if (this._tile___8.rowIndex == this.tileToCheck.rowIndex)
							{
								this._animationType___9 = "H";
							}
							else
							{
								this._animationType___9 = "V";
							}
							this.__f__this.StartCoroutine(this.__f__this.mergeEffect(this._tile___8, this._matchingTiles___0, this._animationType___9));
						}
					}
					finally
					{
						((IDisposable)this.__s_130___7).Dispose();
					}
					this._current = new WaitUntil(() => this.__f__this.IsAllAnimationFinished(this._matchingTiles___0));
					this._PC = 1;
					return true;
				}
				this.__f__this.lastIncreaseValue = 0;
				this.__f__this.tileToCheckList.RemoveAt(0);
				this.__f__this.lastCheckedTile = null;
				this._current = null;
				this._PC = 3;
				return true;
			case 1u:
				this.__s_131___10 = this._matchingTiles___0.GetEnumerator();
				try
				{
					while (this.__s_131___10.MoveNext())
					{
						this._tile___11 = this.__s_131___10.Current;
						if (this._tile___11 != this.tileToCheck)
						{
							this.__f__this.ClearTile(this._tile___11);
						}
					}
				}
				finally
				{
					((IDisposable)this.__s_131___10).Dispose();
				}
				this.__f__this.lastIncreaseValue = this.tileToCheck.IncreaseTileValue();
				this.tileToCheck.placedDice.diceItem.GetComponent<SkeletonAnimation>().state.SetAnimation(0, "HC", true);
				this.tileToCheck.placedDice.SetDiceValue(this.tileToCheck.tileValue);
				this.__f__this.lastCheckedTile = this.tileToCheck;
				this._current = null;
				this._PC = 2;
				return true;
			case 2u:
				break;
			case 3u:
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

		internal bool __m__124()
		{
			return this.__f__this.IsAllAnimationFinished(this._matchingTiles___0);
		}
	}

	private sealed class _DestoryDice_c__Iterator1D : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal List<GridTile> matchTiles;

		internal List<GridTile>.Enumerator __s_132___0;

		internal GridTile _tile___1;

		internal GridTile tileToCheck;

		internal int _PC;

		internal object _current;

		internal List<GridTile> ___matchTiles;

		internal GridTile ___tileToCheck;

		internal Merger __f__this;

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
				this.__s_132___0 = this.matchTiles.GetEnumerator();
				try
				{
					while (this.__s_132___0.MoveNext())
					{
						this._tile___1 = this.__s_132___0.Current;
						if (this._tile___1 != this.tileToCheck)
						{
							this.__f__this.ClearTile(this._tile___1);
						}
					}
				}
				finally
				{
					((IDisposable)this.__s_132___0).Dispose();
				}
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

	public GameObject f8Perfabs;

	public static Merger instance;

	private List<GridTile> tileToCheckList;

	public Transform skillPosition;

	public MergedParticleList mergedParticleList;

	public GameObject particle;

	private GridTile lastCheckedTile;

	private int lastIncreaseValue;

	private GameObject[] lastSkill;

	private float flyTime = 1f;

	public Transform[] path;

	public Vector3 EndPointOnScreen;

	private void Awake()
	{
		Merger.instance = this;
	}

	private List<GridTile> ReOrderTiletocheckList(List<GridTile> tilesList)
	{
		List<GridTile> list = new List<GridTile>();
		int num = 999;
		foreach (GridTile current in tilesList)
		{
			if (current.tileValue <= num)
			{
				list.Insert(0, current);
				num = current.tileValue;
			}
			else
			{
				list.Add(current);
			}
		}
		return list;
	}

	public IEnumerator CheckIfMergeable(DistancedTile[] tilesToCheck)
	{
		Merger._CheckIfMergeable_c__Iterator19 _CheckIfMergeable_c__Iterator = new Merger._CheckIfMergeable_c__Iterator19();
		_CheckIfMergeable_c__Iterator.tilesToCheck = tilesToCheck;
		_CheckIfMergeable_c__Iterator.___tilesToCheck = tilesToCheck;
		_CheckIfMergeable_c__Iterator.__f__this = this;
		return _CheckIfMergeable_c__Iterator;
	}

	private IEnumerator WaitAnimationComplete()
	{
		return new Merger._WaitAnimationComplete_c__Iterator1A();
	}

	private void StartParticleEffectColor()
	{
		this.mergedParticleList.SetParticlesColor(this.lastIncreaseValue);
		base.StartCoroutine(this.mergedParticleList.LerpParticlesColor());
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

	private IEnumerator mergeEffect(GridTile tile, List<GridTile> matchingTiles, string animationType)
	{
		Merger._mergeEffect_c__Iterator1B _mergeEffect_c__Iterator1B = new Merger._mergeEffect_c__Iterator1B();
		_mergeEffect_c__Iterator1B.tile = tile;
		_mergeEffect_c__Iterator1B.matchingTiles = matchingTiles;
		_mergeEffect_c__Iterator1B.animationType = animationType;
		_mergeEffect_c__Iterator1B.___tile = tile;
		_mergeEffect_c__Iterator1B.___matchingTiles = matchingTiles;
		_mergeEffect_c__Iterator1B.___animationType = animationType;
		_mergeEffect_c__Iterator1B.__f__this = this;
		return _mergeEffect_c__Iterator1B;
	}

	private bool IsAllAnimationFinished(List<GridTile> matchingTiles)
	{
		foreach (GridTile current in matchingTiles)
		{
			if (!current.isAnimationFinished)
			{
				return false;
			}
		}
		return true;
	}

	private bool IsTileInFollowList(GridTile tile, List<GridTile> matchingTiles)
	{
		foreach (GridTile current in matchingTiles)
		{
			if (!(tile == current))
			{
				if (current.tileToFollow == tile)
				{
					return true;
				}
			}
		}
		return false;
	}

	public IEnumerator TryMergeNew(GridTile tileToCheck)
	{
		Merger._TryMergeNew_c__Iterator1C _TryMergeNew_c__Iterator1C = new Merger._TryMergeNew_c__Iterator1C();
		_TryMergeNew_c__Iterator1C.tileToCheck = tileToCheck;
		_TryMergeNew_c__Iterator1C.___tileToCheck = tileToCheck;
		_TryMergeNew_c__Iterator1C.__f__this = this;
		return _TryMergeNew_c__Iterator1C;
	}

	private IEnumerator DestoryDice(List<GridTile> matchTiles, GridTile tileToCheck)
	{
		Merger._DestoryDice_c__Iterator1D _DestoryDice_c__Iterator1D = new Merger._DestoryDice_c__Iterator1D();
		_DestoryDice_c__Iterator1D.matchTiles = matchTiles;
		_DestoryDice_c__Iterator1D.tileToCheck = tileToCheck;
		_DestoryDice_c__Iterator1D.___matchTiles = matchTiles;
		_DestoryDice_c__Iterator1D.___tileToCheck = tileToCheck;
		_DestoryDice_c__Iterator1D.__f__this = this;
		return _DestoryDice_c__Iterator1D;
	}

	private void ClearTile(GridTile tile)
	{
		this.tileToCheckList.Remove(tile);
		tile.Reset();
	}
}
