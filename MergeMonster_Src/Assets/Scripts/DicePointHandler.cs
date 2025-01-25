using System;
using UnityEngine;

public class DicePointHandler : MonoBehaviour
{
	public int minumDiceScore = 1;

	public int DiceScore;

	private int StartDiceScore = 2;

	private int MaximumDiceScore = 7;

	private int tempLastDiceScore;

	public bool debugmode = true;

	public static DicePointHandler instance;

	private void Awake()
	{
		DicePointHandler.instance = this;
		this.DiceScore = this.StartDiceScore;
		if (this.debugmode)
		{
			this.Debug();
		}
	}

	public void ResetDiceScore()
	{
		this.DiceScore = this.StartDiceScore;
		this.tempLastDiceScore = 0;
	}

	private void Debug()
	{
		this.minumDiceScore = 6;
		this.DiceScore = this.MaximumDiceScore;
	}

	public void CheckIfScoreCanBeIncreased(int value)
	{
		if (value > this.DiceScore)
		{
			this.IncreaseMaximumDiceScore();
		}
	}

	public void IncreaseMaximumDiceScore()
	{
		this.DiceScore++;
		this.DiceScore = Mathf.Clamp(this.DiceScore, this.minumDiceScore, this.MaximumDiceScore);
	}

	public int GetMaxDiceScore()
	{
		return this.MaximumDiceScore;
	}

	public int GetTempLastDiceScore()
	{
		return this.tempLastDiceScore;
	}

	public void SetTempLastDiceScore(int value)
	{
		this.tempLastDiceScore = value;
	}
}
