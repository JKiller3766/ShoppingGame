using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransaction : MonoBehaviour
{
    public Text TextMoney;

    private int oldMoney;

    private bool changingMoney;

    private float timer = 0f;

    void Awake()
    {
        TextMoney.text = "" + (Shop.Money);
        TextMoney.color = Color.white;
    }

    private void FixedUpdate()
    {
        if (changingMoney)
        {

            if (oldMoney > Player.Money)
            {
                oldMoney--;
                TextMoney.color = Color.red;
                TextMoney.text = "" + (oldMoney);
            }

            else if (oldMoney < Player.Money)
            {
                oldMoney++;
                TextMoney.color = Color.green;
                TextMoney.text = "" + (oldMoney);

            }

            if (oldMoney == Player.Money)
            {
                changingMoney = false;
                timer = Timer.TimePast;
            }

        }

        if (timer + 0.3f < Timer.TimePast && !(TextMoney.color == Color.white) && !changingMoney)
        {
            TextMoney.color = Color.white;
        }

    }

    private void OnEnable()
    {
        Player.OnTransaction += ChangeMoney;
    }

    private void OnDisable()
    {
        Player.OnTransaction -= ChangeMoney;
    }

    private void ChangeMoney(int OldMoney)
    {
        oldMoney = OldMoney;
        changingMoney = true;
    }
}
