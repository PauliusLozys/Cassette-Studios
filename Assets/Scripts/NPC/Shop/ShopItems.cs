using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Item : MonoBehaviour
{
	public enum DefenceStat
	{
		ArmourUpgrade,
		HealthUpgrade,
		AgilityUpgrade,
		JumpingUpgrade
	}
	public enum OffenceStat
	{
		WeaponUpgrade,
		RangedUpgrade
	}
	
	public static int GetCost(DefenceStat stat)
	{
		switch (stat)
		{
			default:
			case DefenceStat.ArmourUpgrade:
				return 100;
			case DefenceStat.HealthUpgrade:
				return 150;
			case DefenceStat.AgilityUpgrade:
				return 120;
			case DefenceStat.JumpingUpgrade:
				return 1000;
		}
	}
	public static int GetCost(OffenceStat stat)
	{
		switch (stat)
		{
			default:
			case OffenceStat.RangedUpgrade:
				return 300;
			case OffenceStat.WeaponUpgrade:
				return 140;
		}
	}

	public static Sprite GetSprite(DefenceStat stat)
	{
		switch (stat)
		{
			default:
			case DefenceStat.HealthUpgrade:
				return GameAssets.i.HealthIconSprite;
			case DefenceStat.ArmourUpgrade:
				return GameAssets.i.ArmourIconSprite;
			case DefenceStat.AgilityUpgrade:
				return GameAssets.i.AgilityIconSprite;
			case DefenceStat.JumpingUpgrade:
				return GameAssets.i.JumpingIconSprite;
		}
	}
	public static Sprite GetSprite(OffenceStat stat)
	{
		switch (stat)
		{
			default:
			case OffenceStat.RangedUpgrade:
				return GameAssets.i.RangedIconSprite;
			case OffenceStat.WeaponUpgrade:
				return GameAssets.i.WeaponIconSprite;

		}
	}
}
