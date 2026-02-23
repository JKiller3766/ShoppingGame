using UnityEngine;
using UnityEngine.UI;

public class PlayerTransaction : MonoBehaviour
{
    public Text Label;

    private void OnEnable()
    {
        Player.OnPlayerChangeHealth += ChangeHealth;
    }

    private void OnDisable()
    {
        Player.OnPlayerChangeHealth += ChangeHealth;
    }

    public void ChangeHealth()
    {
        Label.text = "" + Player.Health;
    }
}
