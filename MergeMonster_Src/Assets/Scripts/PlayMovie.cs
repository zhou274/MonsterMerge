using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMovie : MonoBehaviour
{
	//private sealed class _GotoStartLevel_c__Iterator1F : IEnumerator, IDisposable, IEnumerator<object>
	//{
	//	internal int _PC;

	//	internal object _current;

	//	internal PlayMovie __f__this;

	//	object IEnumerator<object>.Current
	//	{
	//		get
	//		{
	//			return this._current;
	//		}
	//	}

	//	object IEnumerator.Current
	//	{
	//		get
	//		{
	//			return this._current;
	//		}
	//	}

	//	public bool MoveNext()
	//	{
	//		uint num = (uint)this._PC;
	//		this._PC = -1;
	//		switch (num)
	//		{
	//		case 0u:
	//			Handheld.PlayFullScreenMovie(this.__f__this.movie, Color.white, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
	//			this._current = new WaitForEndOfFrame();
	//			this._PC = 1;
	//			return true;
	//		case 1u:
	//			this._PC = -1;
	//			break;
	//		}
	//		return false;
	//	}

	//	public void Dispose()
	//	{
	//		this._PC = -1;
	//	}

	//	public void Reset()
	//	{
	//		throw new NotSupportedException();
	//	}
	//}

	//private string movie = "Logo.mp4";

	//private void Start()
	//{
	//	base.StartCoroutine(this.GotoStartLevel());
	//	SceneManager.LoadScene("Loading");
	//}

	//private IEnumerator GotoStartLevel()
	//{
	//	PlayMovie._GotoStartLevel_c__Iterator1F _GotoStartLevel_c__Iterator1F = new PlayMovie._GotoStartLevel_c__Iterator1F();
	//	_GotoStartLevel_c__Iterator1F.__f__this = this;
	//	return _GotoStartLevel_c__Iterator1F;
	//}
}
