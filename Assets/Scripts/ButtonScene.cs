using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour
{
    public static void ChangeScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public static void Prueba1Money(int asd)
    {
        Player.ModifyMoney(asd, true);
    }

    public static void Prueba2Money(int asd)
    {
        Player.ModifyMoney(asd, false);
    }
}
