using UnityEngine;
using UnityEngine.UI;

public class ShopTransaction : MonoBehaviour
{
    public Text Label;

    private void OnEnable()
    {
        Player.OnTransaction += ChangeMoneyText;
    }

    private void OnDisable()
    {
        Player.OnTransaction += ChangeMoneyText;
    }

    public void ChangeMoneyText()
    {
        Label.text = "" + Player.Money;
    }
}
