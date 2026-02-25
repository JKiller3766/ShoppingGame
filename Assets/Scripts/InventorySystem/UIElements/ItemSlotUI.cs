using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image Image;
    public Image BackgroundImage;
    public TextMeshProUGUI AmountText;

    public static ItemSlotUI Selected;

    private Canvas canvas;
    private Transform parent;
    private ItemBase item;
    private InventoryUI inventoryUI;

    private float velocidad = 75f;
    private float anguloMax = 5f;
    private float rotacionZ = 0f;
    private int direccion = 1;
    private bool giggling = false;

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

    void FixedUpdate()
    {
        if (giggling)
        {
            rotacionZ += velocidad * Time.deltaTime * direccion;

            if (Mathf.Abs(rotacionZ) >= anguloMax)
                direccion *= -1;

            transform.rotation = Quaternion.Euler(0, 0, rotacionZ);
        }
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

        transform.localScale = new Vector3(4.5f, 4.5f, 4.5f);

        if (Selected != null) Selected.ChangeSelection();

        Selected = this;
        BackgroundImage.fillAmount = 0;
        giggling = true;
        OnSelect?.Invoke();
    }
	
    

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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

        transform.localRotation = Quaternion.identity;

        transform.localScale = new Vector3(3f, 3f, 3f);

        Selected.BackgroundImage.fillAmount = 0;
        Selected = null;
        giggling = false;
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
            Selected = this;
            BackgroundImage.fillAmount = 1;
            transform.localScale = new Vector3(3.6f, 3.6f, 3.6f);
            OnSelect?.Invoke();
        }
        else
        {
            Selected = null;
            BackgroundImage.fillAmount = 0;
            transform.localScale = new Vector3(3f, 3f, 3f);
            OnDeselect?.Invoke();
        }
    }

    public string InventoryType()
    {
        return Selected.inventoryUI.Inventory.InventoryType;
    }

    public ItemBase GetItem()
    {
        return item;
    }
	
	public int GetItemPrice()
    {
        return item.Cost;
    }

    public bool IsFood()
    {
        return item is ItemFood;
    }

    public bool IsPotion()
    {
        return item is ItemPotion;
    }
}
