using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransaction : MonoBehaviour
{
    public Text TextMoney;
	
    public int OldMoney;
	
    private bool changingMoney;
    private float timer = 0.0f;

    void Awake()
    {
        TextMoney.text = "" + (Player.Money);
        TextMoney.color = Color.white;
    }

    private void FixedUpdate()
    {
        if (changingMoney)
        {
            if (OldMoney > Player.Money)
            {
                OldMoney--;
				
                TextMoney.color = Color.red;
                TextMoney.text = "" + (OldMoney);
            }
            else if (OldMoney < Player.Money)
            {
                OldMoney++;
				
                TextMoney.color = Color.green;
                TextMoney.text = "" + (OldMoney);
            }

            if (OldMoney == Player.Money)
            {
                changingMoney = false;
				
                timer = Timer.TimePast;
            }
        }

        if ((timer + 0.3f < Timer.TimePast) && (!(TextMoney.color == Color.white)) && (!changingMoney))
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

    private void ChangeMoney(int oldMoney)
    {
        OldMoney = oldMoney;
        changingMoney = true;
    }
}
