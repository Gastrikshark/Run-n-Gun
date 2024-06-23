using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

        CreateShopItem(Items.ItemType.secondGun, Items.GetCost(Items.ItemType.secondGun), Items.GetSprite(Items.ItemType.secondGun));
        CreateShopItem(Items.ItemType.HealingPotion, Items.GetCost(Items.ItemType.HealingPotion), Items.GetSprite(Items.ItemType.HealingPotion));
    }

    private void CreateShopItem(Items.ItemType itemType, int cost, Sprite itemSprite)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);

        shopItemTransform.Find("itemName").GetComponent<Text>().text = itemType.ToString();
        shopItemTransform.Find("costText").GetComponent<Text>().text = cost.ToString();
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemType));
    }

    private void TryBuyItem(Items.ItemType itemType)
    {
        if (Player.instance.TryBuyItem(itemType))
        {
            Debug.Log("Item purchased: " + itemType);
        }
        else
        {
            Debug.Log("Not enough coins for: " + itemType);
        }
    }
}

