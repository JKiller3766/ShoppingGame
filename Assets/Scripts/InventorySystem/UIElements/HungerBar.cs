using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HungerBar : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image hungerBar;

    public static int OldHunger;

    private bool changingHunger;

    void FixedUpdate()
    {
        if (changingHunger)
        {
            if (OldHunger > Player.Hunger)
            {
                OldHunger--;

                float fillAmount = ((float)OldHunger) / ((float)Player.MaxHunger);
                hungerBar.fillAmount = fillAmount;
            }
            else if (OldHunger < Player.Hunger)
            {
                OldHunger++;

                float fillAmount = ((float)OldHunger) / ((float)Player.MaxHunger);
                hungerBar.fillAmount = fillAmount;
            }

            if (OldHunger == Player.Hunger)
            {
                changingHunger = false;
            }
        }
    }

    private void OnEnable()
    {
        Player.OnPlayerChangeHunger += ChangeHunger;
    }

    private void OnDisable()
    {
        Player.OnPlayerChangeHunger -= ChangeHunger;
    }

    private void ChangeHunger(int oldHunger)
    {
        changingHunger = true;
        OldHunger = oldHunger;
    }
	
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.ModifyHunger(10, false);
    }
}
