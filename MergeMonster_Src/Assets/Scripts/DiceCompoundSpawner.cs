using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceCompoundSpawner : MonoBehaviour
{
	public Transform leftDiceSpot;

	public Transform rightDiceSpot;

	public Transform centerDiceSpot;

	public Transform compound;

	private string dicePrefab = "Dice";

	public SpriteRenderer rotatingSwitch;

	public static DiceCompoundSpawner instance;

	private void Awake()
	{
		DiceCompoundSpawner.instance = this;
		this.SetRotatingSwitch(false);
	}

	public void SpawnDices(int Type)
	{
		if (!this.SpotsAvailable())
		{
			this.SetRotatingSwitch(false);
			ScoreHandler.instance.LoadNumberOfTime();
			if (ScoreHandler.instance.numberOfTime % GameManager.instance.divisor == 0)
			{
				Integrations.Instance().ShowInterstitial();
			}
			AccountGameOver.instance.EntryAccount();
			return;
		}
		DiceCompound.instance.ResetPosition();
		int num = UnityEngine.Random.Range(0, 101);
		if (num > GameManager.instance.spawnerCompoundPercentage)
		{
			num = 0;
		}
		else
		{
			num = 1;
		}
		if (num != 0 && !this.DoubleSpotAvailable())
		{
			num = 0;
		}
		if (Type == 0)
		{
			num = 0;
		}
		if (num == 0)
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.dicePrefab), this.centerDiceSpot.position, Quaternion.identity);
			gameObject.transform.parent = this.compound;
			gameObject.tag = "None";
		}
		else
		{
			GameObject gameObject2 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.dicePrefab), this.leftDiceSpot.position, Quaternion.identity);
			GameObject gameObject3 = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(this.dicePrefab), this.rightDiceSpot.position, Quaternion.identity);
			this.SetRotatingSwitch(true);
			gameObject2.transform.parent = this.compound;
			gameObject3.transform.parent = this.compound;
			gameObject2.tag = "None";
			gameObject3.tag = "None";
		}
		DiceCompound.instance.FindDicesInCompound();
	}

	public void SetRotatingSwitch(bool value)
	{
		this.rotatingSwitch.enabled = value;
	}

	private bool DoubleSpotAvailable()
	{
		GridTile[] tiles = GridMap.instance.tiles;
		for (int i = 0; i < tiles.Length; i++)
		{
			GridTile gridTile = tiles[i];
			if (gridTile.isEmpty())
			{
				List<GridTile> matchingTileList = TileChecker.GetMatchingTileList(gridTile);
				if (matchingTileList.Count > 1)
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool SpotsAvailable()
	{
		GridTile[] tiles = GridMap.instance.tiles;
		for (int i = 0; i < tiles.Length; i++)
		{
			GridTile gridTile = tiles[i];
			if (gridTile.isEmpty())
			{
				return true;
			}
		}
		return false;
	}
}
