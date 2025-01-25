using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LanguageHelper
{
	private static Dictionary<string, string> _dic = new Dictionary<string, string>();

	public static void SetLanguage(string lanStr)
	{
		LanguageHelper._dic.Clear();
		TextAsset textAsset = Resources.Load(Path.Combine("Lang", lanStr)) as TextAsset;
		if (textAsset == null)
		{
			textAsset = (Resources.Load(Path.Combine("Lang", "English")) as TextAsset);
		}
		string text = textAsset.text.Replace("\r", string.Empty);
		string[] array = text.Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				'='
			});
			LanguageHelper._dic[array2[0]] = array2[1];
		}
	}

	public static string GetString(string name, string defaultValue = "")
	{
		if (LanguageHelper._dic.ContainsKey(name))
		{
			return LanguageHelper._dic[name];
		}
		return defaultValue;
	}
}
