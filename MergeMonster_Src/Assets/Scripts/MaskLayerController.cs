using System;
using UnityEngine;
using UnityEngine.UI;

public class MaskLayerController : MonoBehaviour
{
	public static MaskLayerController instance;

	public Image mask;

	private void Awake()
	{
		MaskLayerController.instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Activite()
	{
		base.gameObject.SetActive(true);
	}

	public void DeadActivite()
	{
		base.gameObject.SetActive(false);
	}
}
