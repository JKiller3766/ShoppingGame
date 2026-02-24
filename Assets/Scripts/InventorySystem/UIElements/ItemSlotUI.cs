using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // NOTE: Inventory UI slots support drag&drop,
    // implementing the Unity provided interfaces by events system

    public Image Image;
    public Image BackgroundImage;
    public TextMeshProUGUI AmountText;

    public static ItemSlotUI Selected;

    private Canvas canvas;
    private Transform parent;
    private ItemBase item;
    private InventoryUI inventoryUI;

    public static event Action OnSelect;
    public static event Action OnDeselect;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();

        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = (slot.Amount > 1);

        item = slot.Item;
        this.inventoryUI = inventory;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeSelected();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canvas) canvas = GetComponentInParent<Canvas>();

        parent = transform.parent;

        transform.SetParent(canvas.transform, true);
        
        transform.SetAsLastSibling();
        Selected = this;
        BackgroundImage.fillAmount = 0;
        OnSelect?.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Find scene objects colliding with mouse point on end dragging
        RaycastHit2D hitData = Physics2D.GetRayIntersection(
            Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData)
        {
            var hit = hitData.collider.gameObject.GetComponent<InventoryUI>();

            if ((hit != null) && (hit.Inventory.InventoryType == "PlayerInventory") && (this.inventoryUI.Inventory.InventoryType == "ShopInventory"))
            {
                ShoppingButtons.Buy();
            }

            if ((hit != null) && (hit.Inventory.InventoryType == "ShopInventory") && (this.inventoryUI.Inventory.InventoryType == "PlayerInventory"))
            {
                ShoppingButtons.Sell();
            }
        }

        transform.SetParent(parent.transform);

        transform.localPosition = Vector3.zero;

        BackgroundImage.fillAmount = 0;
        Selected = null;
        OnDeselect?.Invoke();
    }

    private void ChangeSelected()
    {
        if (Selected == null)
        {
            ChangeSelection();
        }

        else
        {
            if (Selected == this)
            {
                Selected = null;
                ChangeSelection();
            }

            else
            {
                Selected.ChangeSelection();
                ChangeSelection();
            }
                
        }
    }

    private void ChangeSelection()
    {
        if (BackgroundImage.fillAmount == 0)
        {
            BackgroundImage.fillAmount = 1;
            Selected = this;
            OnSelect?.Invoke();
        }
        else
        {
            BackgroundImage.fillAmount = 0;
            Selected = null;
            OnDeselect?.Invoke();
        }
    }

    public string InventoryType()
    {
        return Selected.inventoryUI.Inventory.InventoryType;
    }

    public int GetItemPrice()
    {
        return item.Cost;
    }

    public ItemBase GetItem()
    {
        return item;
    }

    public bool IsFood()
    {
        return this.item is ItemFood;
    }

    public bool IsPotion()
    {
        return this.item is ItemPotion;
    }
}
