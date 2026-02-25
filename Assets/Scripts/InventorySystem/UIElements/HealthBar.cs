using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HealthBar : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image healthBar;

    public static int OldHealth;

    private bool changingHealth;


    void FixedUpdate()
    {
        if (changingHealth)
        {
            if (OldHealth > Player.Health)
            {
                OldHealth--;

                float fillAmount = ((float)OldHealth) / ((float)Player.MaxHealth);
                healthBar.fillAmount = fillAmount;
            }
            else if (OldHealth < Player.Health)
            {
                OldHealth++;

                float fillAmount = ((float)OldHealth) / ((float)Player.MaxHealth);
                healthBar.fillAmount = fillAmount;
            }

            if (OldHealth == Player.Health)
            {
                changingHealth = false;
            }
        }
    }

    private void OnEnable()
    {
        Player.OnPlayerChangeHealth += ChangeHealth;
    }

    private void OnDisable()
    {
        Player.OnPlayerChangeHealth -= ChangeHealth;
    }

    private void ChangeHealth(int oldHealth)
    {
        changingHealth = true;
        OldHealth = oldHealth;
    }
	
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.ModifyHealth(10, false);
    }
}
