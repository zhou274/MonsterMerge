using System;
using UnityEngine;

public class Loading : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
		LoadingSceneManager.LoadScene("LoadScene");
	}

	private void Update()
	{
	}
}
