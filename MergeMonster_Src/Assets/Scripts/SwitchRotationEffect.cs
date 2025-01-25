using System;
using UnityEngine;

public class SwitchRotationEffect : MonoBehaviour
{
	public float SecondsTimeToCompleteA360Rotation = 5f;

	private float currentRotation;

	private void Start()
	{
	}

	private void Update()
	{
		this.currentRotation += 360f * (Time.deltaTime / this.SecondsTimeToCompleteA360Rotation);
		Vector3 eulerAngles = base.transform.eulerAngles;
		eulerAngles.z = this.currentRotation;
		base.transform.eulerAngles = eulerAngles;
	}
}
