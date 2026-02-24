using UnityEditor;
using UnityEngine;

public class ShoppingButtons : MonoBehaviour
{
    private static Inventory playerInventory; 
    private static Inventory shopInventory;

    private void Awake()
    {
        playerInventory = GameObject.Find("InventoryPlayer").GetComponent<InventoryUI>().Inventory;
        shopInventory = GameObject.Find("InventoryShop").GetComponent<InventoryUI>().Inventory;
    }
    public static void Buy()
    {
        if (ItemSlotUI.Selected != null)
        {
            if (ItemSlotUI.Selected.InventoryType() == "ShopInventory")
            {
                int itemCost = ItemSlotUI.Selected.GetItemPrice();
                if (Player.Money >= itemCost)
                {
                    Player.ModifyMoney(itemCost, false);
                    shopInventory.RemoveItem(ItemSlotUI.Selected.GetItem());
                    playerInventory.AddItem(ItemSlotUI.Selected.GetItem());
                }
            }
        }
    }

    public static void Sell()
    {
        if (ItemSlotUI.Selected != null)
        {
            if (ItemSlotUI.Selected.InventoryType() == "PlayerInventory")
            {
                int itemCost = ItemSlotUI.Selected.GetItemPrice();
                if (Shop.Money >= itemCost)
                {
                    Player.ModifyMoney(itemCost, true);
                    shopInventory.AddItem(ItemSlotUI.Selected.GetItem());
                    playerInventory.RemoveItem(ItemSlotUI.Selected.GetItem());
                }
            }
        }
    }

    public static void Use()
    {

    }

    private void LazyInicializacion()
    {
        
    }
}