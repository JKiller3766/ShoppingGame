using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopInventory", menuName = "Inventory System/Shop Inventory")]
public class ShopInventory : Inventory
{
    public static readonly string TypeName = "ShopInventory";

    OnEnable()
    {
        inventoryType = TypeName;
    }
}
