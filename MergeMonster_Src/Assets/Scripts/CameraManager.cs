using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;

	public Vector3 originalPosition;

	private void Awake()
	{
		this.originalPosition = base.transform.position;
		CameraManager.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
