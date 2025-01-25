using System;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
	public static SoundsManager instance;

	public AudioSource button;

	public AudioSource diceSwap;

	public AudioSource drop;

	public AudioSource explosion;

	public AudioSource gameover;

	public AudioSource gamestart;

	public AudioSource merge;

	public AudioSource mSpawn;

	public AudioSource wrongDrop;

	public AudioSource delete;

	public AudioSource UIButton;

	public AudioSource born;

	public AudioSource rotataion;

	public AudioSource hammer;

	private void Awake()
	{
		SoundsManager.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void PlayAudioSource(AudioSource audioSource)
	{
		audioSource.Play();
	}
}
