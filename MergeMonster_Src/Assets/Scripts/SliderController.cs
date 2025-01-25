using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderController : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IEndDragHandler
{
	private ScrollRect rect;

	private List<float> pages = new List<float>();

	private List<Transform> points = new List<Transform>();

	public float smooting = 4f;

	private float targethorizontal;

	private bool isDrag;

	private static int index;

	public Action<int, int> OnPageChanged;

	private float startime;

	private float delay = 0.1f;

	private float preX;

	private float endX;

	private void Start()
	{
		this.rect = base.transform.GetComponent<ScrollRect>();
		GameObject gameObject = GameObject.Find("Point");
		this.points.Add(gameObject.transform.GetChild(0));
		this.points.Add(gameObject.transform.GetChild(1));
		this.points.Add(gameObject.transform.GetChild(2));
		for (int i = 0; i < this.points.Count; i++)
		{
			if (i == 0)
			{
				this.points[i].transform.GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.points[i].transform.GetComponent<Image>().color = Color.gray;
			}
		}
		this.startime = Time.time;
	}

	private void Update()
	{
		this.UpdatePages();
		if (!this.isDrag && this.pages.Count > 0)
		{
			this.rect.horizontalNormalizedPosition = Mathf.Lerp(this.rect.horizontalNormalizedPosition, this.targethorizontal, Time.deltaTime * this.smooting);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		this.preX = eventData.position.x;
		this.isDrag = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.endX = eventData.position.x;
		this.isDrag = false;
		float horizontalNormalizedPosition = this.rect.horizontalNormalizedPosition;
		if (horizontalNormalizedPosition > (float)(1 / (3 * (this.pages.Count - SliderController.index))) && this.endX < this.preX)
		{
			SliderController.index++;
			if (SliderController.index > this.pages.Count - 1)
			{
				SliderController.index = this.pages.Count - 1;
			}
		}
		if (horizontalNormalizedPosition > (float)(1 / (3 * (this.pages.Count - SliderController.index))) && this.endX > this.preX)
		{
			SliderController.index--;
			if (SliderController.index < 0)
			{
				SliderController.index = 0;
			}
		}
		this.targethorizontal = this.pages[SliderController.index];
		for (int i = 0; i < this.points.Count; i++)
		{
			if (i == SliderController.index)
			{
				this.points[i].GetComponent<Image>().color = Color.white;
			}
			else
			{
				this.points[i].GetComponent<Image>().color = Color.gray;
			}
		}
	}

	private void UpdatePages()
	{
		int num = this.rect.content.childCount;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			if (this.rect.content.GetChild(i).gameObject.activeSelf)
			{
				num2++;
			}
		}
		num = num2;
		if (this.pages.Count != num && num != 0)
		{
			this.pages.Clear();
			for (int j = 0; j < num; j++)
			{
				float item = 0f;
				if (num != 1)
				{
					item = (float)j / (float)(num - 1);
				}
				this.pages.Add(item);
			}
		}
	}
}
