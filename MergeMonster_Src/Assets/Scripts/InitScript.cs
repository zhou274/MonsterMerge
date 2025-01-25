using System;
using UnityEngine;
using UnityEngine.UI;

public class InitScript : MonoBehaviour
{
	public static InitScript Instance;

	public static string Username;

	public static Sprite profilePic;

	public static int maxLevel;

	public static string user_id;

	private int k;

	public GameObject elementPrefab;

	public GameObject parent;

	public Sprite[] sprite;

	private void Awake()
	{
		InitScript.Instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void CallUIRedraw()
	{
	}

	public void DrawBadge(GameObject p)
	{
		if (this.parent.transform.GetChild(0) != null)
		{
			p.transform.GetChild(0).Find("Badge").GetComponent<Image>().sprite = this.sprite[0];
		}
		if (this.parent.transform.GetChild(1) != null)
		{
			p.transform.GetChild(1).Find("Badge").GetComponent<Image>().sprite = this.sprite[1];
		}
		if (this.parent.transform.GetChild(2) != null)
		{
			p.transform.GetChild(2).Find("Badge").GetComponent<Image>().sprite = this.sprite[2];
		}
		else
		{
			p.transform.GetChild(2).Find("Badge").GetComponent<Image>().sprite = null;
		}
	}

	public GameObject AddPhotoFrame()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.elementPrefab);
		gameObject.transform.SetParent(this.parent.transform);
		gameObject.transform.localScale = Vector3.one;
		return gameObject;
	}
}
