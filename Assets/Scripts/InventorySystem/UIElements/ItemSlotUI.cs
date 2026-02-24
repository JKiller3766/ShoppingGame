using System.Collections.Generic;
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

        BackgroundImage.fillAmount = 1;
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
            Debug.Log("Drop over object: " + hitData.collider.gameObject.name);

            var consumer = hitData.collider.gameObject.GetComponent<IConsume>();

            if ((consumer != null) && (item is ConsumableItem))
            {
                //(item as ConsumableItem).Use(consumer);
                inventoryUI.UseItem(item);
            }
        }

        // Changing parent back to slot
        transform.SetParent(parent.transform);

        // And centering item position
        transform.localPosition = Vector3.zero;

        BackgroundImage.fillAmount = 0;
        Selected = null;
    }

    private void ChangeSelected()
    {
        if (Selected == null)
        {
            ChangeSelection();
            Selected = this;
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
                Selected = this;
                ChangeSelection();
            }
                
        }
    }

    private void ChangeSelection()
    {
        if (BackgroundImage.fillAmount == 0) BackgroundImage.fillAmount = 1;
        else BackgroundImage.fillAmount = 0;
    }

    public string InventoryType()
    {
        return inventoryUI.Inventory.GetInventoryType();
    }
}
