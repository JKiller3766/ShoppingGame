using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

    public Text Name;
    public Text Description;
    public Text Cost;
    public Text Restores;
    public Image Sprite;

    [SerializeField]
    private Sprite defaultSprite;

    private ItemBase itemSelected;

    public void OnEnable()
    {
        ItemSlotUI.OnSelect += AddInfoChart;
        ItemSlotUI.OnDeselect += RemoveInfoChart;
    }

    private void OnDisable()
    {
        ItemSlotUI.OnSelect -= AddInfoChart;
        ItemSlotUI.OnDeselect -= RemoveInfoChart;
    }

    private void AddInfoChart()
    {
        if (ItemSlotUI.Selected != null)
        {
            itemSelected = ItemSlotUI.Selected.GetItem();
            
            if (itemSelected != null)
            {
                Name.text = "Name: " + itemSelected.Name;
                Description.text = "Description: " + itemSelected.Description;
                Cost.text = "Cost: " + itemSelected.Cost;
                Sprite.sprite = itemSelected.ImageUI;
                if (itemSelected is ConsumableItem)
                {
                    Restores.text = "Restore: " + (itemSelected as ConsumableItem).Restore;
                }

                else
                {
                    Restores.text = "";
                }
            }

            
        }
    }

    private void RemoveInfoChart()
    {
        Name.text = "Name: ";
        Description.text = "Description: ";
        Cost.text = "Cost: ";
        Sprite.sprite = defaultSprite;
        Restores.text = "Restores: ";
    }
}
