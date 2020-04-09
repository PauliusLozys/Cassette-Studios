using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public enum ItemType
	{
		WeaponUpgrade,
		ArmourUpgrade
	}
	public enum StatType
	{
		HealthUpgrade
	}

	public static int GetCost(ItemType item)
	{
		switch (item)
		{
			default:
			case ItemType.WeaponUpgrade:
				return 100;
			case ItemType.ArmourUpgrade:
				return 150;
		}
	}
	public static int GetCost(StatType stat)
	{
		switch (stat)
		{
			default:
			case StatType.HealthUpgrade:
				return 140;
		}
	}

	public static Sprite GetSprite(ItemType item)
	{
		switch (item)
		{
			default:
			case ItemType.WeaponUpgrade:
				return GameAssets.i.WeaponIconSprite;
			case ItemType.ArmourUpgrade:
				return GameAssets.i.ArmourIconSprite;
		}
	}
	public static Sprite GetSprite(StatType stat)
	{
		switch (stat)
		{
			default:
			case StatType.HealthUpgrade:
				return GameAssets.i.HealthIconSprite;
		}
	}
}
