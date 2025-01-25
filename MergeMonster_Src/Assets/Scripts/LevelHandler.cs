using System;
using UnityEngine;

public static class LevelHandler
{
	private static int levelExpNeed = 1000;

	public static int level = 1;

	private static float barValue;

	public static int GetLevelByExp(int exp)
	{
		LevelHandler.level = 1 + Mathf.RoundToInt((float)(exp / LevelHandler.levelExpNeed));
		return LevelHandler.level;
	}

	public static float fillValueByExp(int exp)
	{
		int levelByExp = LevelHandler.GetLevelByExp(exp);
		int num = exp - (levelByExp * LevelHandler.levelExpNeed - LevelHandler.levelExpNeed);
		return (float)num / (float)LevelHandler.levelExpNeed;
	}
}
