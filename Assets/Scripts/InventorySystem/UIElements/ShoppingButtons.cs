using UnityEngine;

public class ShoppingButtons : MonoBehaviour
{
    public static void Buy()
    {
        if (ItemSlotUI.Selected != null)
        {
            if (ItemSlotUI.Selected.InventoryType() == "ShopInventory")
            {
                Debug.Log("Se puede comprar");
            }
            else
            {
                Debug.Log("No se puede comprar");
            }
        }
    }

    public static void Sell()
    {
        if (ItemSlotUI.Selected != null)
        {
            if (ItemSlotUI.Selected.InventoryType() == "PlayerInventory")
            {
                Debug.Log("Se puede vender");
            }
            else
            {
                Debug.Log("No se puede vender");
            }
        }
    }

    public static void Use()
    {

    }
}
