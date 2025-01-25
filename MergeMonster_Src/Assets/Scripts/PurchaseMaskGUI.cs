using System;
using UnityEngine;

public class PurchaseMaskGUI : MonoBehaviour
{
	public static PurchaseMaskGUI instacne;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		base.transform.Find("Mask").GetChild(0).GetComponent<Animation>().Play("Purchase");
	}

	public void OpenPurchseMask()
	{
		base.gameObject.SetActive(true);
	}

	public void ClosePurchseMask()
	{
		base.gameObject.SetActive(false);
	}
}
