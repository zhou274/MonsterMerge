using System;
using UnityEngine;

public class SocialNetworksManager : MonoBehaviour
{
	public static SocialNetworksManager instance;

	public string TwitterUrl;

	public string FacebookUrl;

	public string contactUsURL;

	public void Awake()
	{
		SocialNetworksManager.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
