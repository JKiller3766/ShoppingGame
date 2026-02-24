using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class HealthBar : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image healthBar;

    private void OnEnable()
    {
        Player.OnPlayerChangeHealth += ChangeHealth;
    }

    private void OnDisable()
    {
        Player.OnPlayerChangeHealth -= ChangeHealth;
    }

    private void ChangeHealth()
    {
        float fillAmount =  ((float) Player.Health) / ((float)Player.MaxHealth);
        healthBar.fillAmount = fillAmount;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.ModifyHealth(5, false);
    }
}
