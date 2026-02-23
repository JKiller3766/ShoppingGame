using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTransaction : MonoBehaviour
{
    public Text Text;

    private int oldMoney;

    private bool changingMoney;

    private float timer = 0f;

    private void FixedUpdate()
    {
        if (changingMoney)
        {

            if (oldMoney > Shop.Money)
            {
                oldMoney--;
                Text.text = "" + (oldMoney);
                Text.color = Color.red;
            }

            else if (oldMoney < Shop.Money)
            {
                oldMoney++;
                Text.text = "" + (oldMoney);
                Text.color = Color.green;
            }

            if (oldMoney == Shop.Money)
            {
                changingMoney = false;
                timer = Timer.TimePast;
            }

        }

        if (timer + 0.3f < Timer.TimePast && !(Text.color == Color.white))
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
