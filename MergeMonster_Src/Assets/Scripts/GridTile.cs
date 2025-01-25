using System;
using UnityEngine;

public class GridTile : MonoBehaviour
{
	public enum FindDirection
	{
		left,
		right,
		top,
		bottom,
		root
	}

	public int tileIndex;

	public int rowIndex;

	public int tileValue;

	public Dice placedDice;

	public bool isAnimationFinished;

	public GridTile tileToFollow;

	public static GridTile instance;

	public GridTile.FindDirection findDirection;

	private void Awake()
	{
		GridTile.instance = this;
	}

	public void SetFindDirection(GridTile.FindDirection direction)
	{
		if (direction == GridTile.FindDirection.left)
		{
			this.findDirection = GridTile.FindDirection.right;
		}
		if (direction == GridTile.FindDirection.right)
		{
			this.findDirection = GridTile.FindDirection.left;
		}
		if (direction == GridTile.FindDirection.top)
		{
			this.findDirection = GridTile.FindDirection.bottom;
		}
		if (direction == GridTile.FindDirection.bottom)
		{
			this.findDirection = GridTile.FindDirection.top;
		}
		if (direction == GridTile.FindDirection.root)
		{
			this.findDirection = GridTile.FindDirection.root;
		}
	}

	public void SetIndexes(int t, int r)
	{
		this.tileIndex = t;
		this.rowIndex = r;
	}

	public bool isEmpty()
	{
		return this.tileValue == 0;
	}

	public int IncreaseTileValue()
	{
		this.tileValue++;
		int result = this.tileValue;
		this.tileValue = Mathf.Clamp(this.tileValue, DicePointHandler.instance.minumDiceScore, DicePointHandler.instance.GetMaxDiceScore() + 1);
		DicePointHandler.instance.CheckIfScoreCanBeIncreased(this.tileValue);
		return result;
	}

	public void Reset()
	{
		try
		{
			UnityEngine.Object.Destroy(this.placedDice.gameObject);
		}
		catch
		{
		}
		this.placedDice = null;
		this.tileValue = 0;
	}
}
