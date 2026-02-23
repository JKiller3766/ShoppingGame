using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransaction : MonoBehaviour
{
    public Text TextMoney;
    //public Text TextHealth;

    private int oldMoney;
    private int oldHealth;

    private bool changingMoney;
    private bool changingHealth;

    private float timer = 0f;

    private void FixedUpdate()
    {
        if (changingMoney)
        {

            if (oldMoney > Player.Money)
            {
                oldMoney--;
                TextMoney.text = "" + (oldMoney);
                TextMoney.color = Color.red;
            }

            else if (oldMoney < Player.Money)
            {
                oldMoney++;
                TextMoney.text = "" + (oldMoney);
                TextMoney.color = Color.green;

            }

            if (oldMoney == Player.Money)
            {
                changingMoney = false;
                timer = Timer.TimePast;
            }

        }

        if (timer + 0.3f < Timer.TimePast && !(TextMoney.color == Color.white))
        {
            TextMoney.color = Color.white;
        }

    }

    private void OnEnable()
    {
        Player.OnPlayerChangeHealth += ChangeHealth;
        Player.OnTransaction += ChangeMoney;
    }

    private void OnDisable()
    {
        Player.OnPlayerChangeHealth -= ChangeHealth;
        Player.OnTransaction -= ChangeMoney;
    }

    public void ChangeHealth(int OldHealth)
    {
        oldHealth = OldHealth;

        changingHealth = true;
    }

    public void ChangeMoney(int OldMoney)
    {
        oldMoney = OldMoney;

        changingMoney = true;
    }
}
