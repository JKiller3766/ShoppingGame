using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTransaction : MonoBehaviour
{
    public Text Text;

    private int oldMoney;

    private bool changingMoney;

    private float timer = 0f;

    void Awake()
    {
        Text.text = "" + (Shop.Money);
        Text.color = Color.white;
    }

    void FixedUpdate()
    {
        if (changingMoney)
        {

            if (oldMoney > Shop.Money)
            {
                oldMoney--;
                Text.color = Color.red;
                Text.text = "" + (oldMoney);
            }

            else if (oldMoney < Shop.Money)
            {
                oldMoney++;
                Text.color = Color.green;
                Text.text = "" + (oldMoney);
            }

            if (oldMoney == Shop.Money)
            {
                changingMoney = false;
                timer = Timer.TimePast;
            }

        }

        if (timer + 0.3f < Timer.TimePast && !(Text.color == Color.white) && !changingMoney)
        {
            Text.color = Color.white;
        }
    }
    private void OnEnable()
    {
        Shop.OnTransaction += ChangeMoneyText;
    }

    private void OnDisable()
    {
        Shop.OnTransaction += ChangeMoneyText;
    }

    public void ChangeMoneyText(int OldMoney)
    {
        oldMoney = OldMoney;

        changingMoney = true;
    }
}
