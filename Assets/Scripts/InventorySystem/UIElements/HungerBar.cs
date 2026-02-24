using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HungerBar : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image hungerBar;

    private void OnEnable()
    {
        Player.OnPlayerChangeHunger += ChangeHunger;
    }

    private void OnDisable()
    {
        Player.OnPlayerChangeHunger -= ChangeHunger;
    }

    private void ChangeHunger(int health)
    {
        float fillAmount =  ((float) Player.Hunger) / ((float)Player.MaxHunger);
        hungerBar.fillAmount = fillAmount;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.ModifyHunger(5, false);
    }
}
