using UnityEngine;

public class ShoppingButtons : MonoBehaviour
{
    public static void Buy()
    {
        if (ItemSlotUI.Selected == null) Debug.Log(ItemSlotUI.Selected.InventoryType());
        /*else
        {
            if (ItemSlotUI.Selected.InventoryType() == ShopInventory.TypeName)
            {
                Debug.Log("Se puede comprar");
            }
            else
            {
                Debug.Log("No se puede comprar");
            }
        }*/

    }

    public static void Sell()
    {
        if (ItemSlotUI.Selected != null) Debug.Log(ItemSlotUI.Selected.InventoryType());
        /*else
        {
            if (ItemSlotUI.Selected.InventoryType() == PlayerInventory.TypeName)
            {
                Debug.Log("Se puede vender");
            }
            else
            {
                Debug.Log("No se puede vender");
            }
        }*/
    }

    public static void Use()
    {

    }
}
