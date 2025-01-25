using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGUI : MonoBehaviour
{
	private string tutorialShownPlayerPrefsString = "TUTORIALSHOWN";

	public GameObject pager1;

	public GameObject pager2;

	public GameObject pager3;

	public GameObject pager4;

	public Text img1Text1;

	public Text img1Text2;

	public Text img1Text3;

	public Text img2text;

	public Text img3text;

	public static TutorialGUI instance;

	public GameObject mask;

	public Text shovelTutoriaText1;

	public Text shovelTutoriaText2;

	private void Awake()
	{
		TutorialGUI.instance = this;
	}

	private void Start()
	{
		this.img1Text1.text = LanguageHelper.GetString("tl1_1", string.Empty) + "\n" + LanguageHelper.GetString("tl1_2", string.Empty);
		this.img1Text2.text = LanguageHelper.GetString("tl2_1", string.Empty) + "\n" + LanguageHelper.GetString("tl2_2", string.Empty);
		this.img1Text3.text = LanguageHelper.GetString("tl3_1", string.Empty) + "\n" + LanguageHelper.GetString("tl3_2", string.Empty);
	}

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			this.Deactivate();
		}
	}

	public void OnCloseButtonClick()
	{
		SoundsManager.instance.PlayAudioSource(SoundsManager.instance.UIButton);
		this.Deactivate();
	}

	public void Activate()
	{
		PlayerPrefs.SetInt(this.tutorialShownPlayerPrefsString, 1);
		base.gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		GUIManager.instance.CloseMask();
		if (this.mask.activeInHierarchy)
		{
			this.mask.SetActive(false);
		}
		base.gameObject.SetActive(false);
	}

	public bool tutorialShown()
	{
		return PlayerPrefs.GetInt(this.tutorialShownPlayerPrefsString, 0) != 0;
	}

	public void ShowPager2()
	{
		base.gameObject.SetActive(true);
		this.pager1.SetActive(false);
		this.pager2.SetActive(true);
		this.pager3.SetActive(false);
		this.pager4.SetActive(false);
		this.img2text.text = LanguageHelper.GetString("tl4_1", string.Empty) + "\n" + LanguageHelper.GetString("tl4_2", string.Empty);
	}

	public void ShowPager3()
	{
		base.gameObject.SetActive(true);
		this.pager3.SetActive(true);
		this.img3text.text = LanguageHelper.GetString("tl5_1", string.Empty) + "\n" + LanguageHelper.GetString("tl5_2", string.Empty);
		this.pager2.SetActive(false);
		this.pager1.SetActive(false);
		this.pager4.SetActive(false);
	}

	public void ShowPager4()
	{
		base.gameObject.SetActive(true);
		this.pager4.SetActive(true);
		this.shovelTutoriaText1.text = LanguageHelper.GetString("shovelText1", string.Empty);
		this.shovelTutoriaText2.text = LanguageHelper.GetString("shovelText2", string.Empty);
		this.pager1.SetActive(false);
		this.pager2.SetActive(false);
		this.pager3.SetActive(false);
	}
}
