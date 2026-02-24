using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerInventory", menuName = "Inventory System/Player Inventory")]
public class PlayerInventory : Inventory
{
    public static readonly string TypeName = "PlayerInventory";

    public void Initialize()
    {
        inventoryType = TypeName;
    }
}
