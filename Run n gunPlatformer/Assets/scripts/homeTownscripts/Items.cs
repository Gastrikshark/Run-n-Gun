using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        secondGun,
        HealingPotion,
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.secondGun: return 15;
            case ItemType.HealingPotion: return 5;
        }
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.secondGun: return Resources.Load<Sprite>("Sprites/item_gun");
            case ItemType.HealingPotion: return Resources.Load<Sprite>("Sprites/Potion");
        }
    }
}
