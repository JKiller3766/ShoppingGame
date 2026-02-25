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
        Localizer.OnLanguageChange += AddInfoChart;
    }

    private void OnDisable()
    {
        ItemSlotUI.OnSelect -= AddInfoChart;
        ItemSlotUI.OnDeselect -= RemoveInfoChart;
        Localizer.OnLanguageChange -= AddInfoChart;
    }

    private void AddInfoChart()
    {
        if (ItemSlotUI.Selected != null)
        {
            itemSelected = ItemSlotUI.Selected.GetItem();
            
            if (itemSelected != null)
            {
                LocalizeText [] localizerObject = GetComponentsInChildren<LocalizeText>();

                Debug.Log(itemSelected.Name);
                Debug.Log(LocalizeText.GetText(itemSelected.Name));

                Name.text = (LocalizeText.GetText(localizerObject[0].TextKey) + " " + "LocalizeText.GetText(itemSelected.Name)");

                Description.text = LocalizeText.GetText(localizerObject[1].TextKey) + " " + LocalizeText.GetText(itemSelected.Description);
                Cost.text = LocalizeText.GetText(localizerObject[2].TextKey) + " " + itemSelected.Cost;
                Sprite.sprite = itemSelected.ImageUI;
				
                if (itemSelected is ConsumableItem)
                {
                    Restores.text = LocalizeText.GetText(localizerObject[3].TextKey) + " " + (itemSelected as ConsumableItem).Restore;
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
