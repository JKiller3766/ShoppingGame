using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopTransaction : MonoBehaviour
{
    public Text Text;

    public int OldMoney;
	
    private bool changingMoney;
    private float timer = 0.0f;

    void Awake()
    {
        Text.text = "" + (Shop.Money);
        Text.color = Color.white;
    }

    void FixedUpdate()
    {
        if (changingMoney)
        {
            if (OldMoney > Shop.Money)
            {
                OldMoney--;
				
                Text.color = Color.red;
                Text.text = "" + (OldMoney);
            }
            else if (OldMoney < Shop.Money)
            {
                OldMoney++;
				
                Text.color = Color.green;
                Text.text = "" + (OldMoney);
            }

            if (OldMoney == Shop.Money)
            {
                changingMoney = false;
				
                timer = Timer.TimePast;
            }
        }

        if ((timer + 0.3f < Timer.TimePast) && (!(Text.color == Color.white)) && (!changingMoney))
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

    public void ChangeMoneyText(int oldMoney)
    {
        OldMoney = oldMoney;

        changingMoney = true;
    }
}
