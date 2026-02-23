using System.Reflection.Emit;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    public Label Label;

    public static int Money = 100; 

    public static bool TakeMoney(int Cost)
    {
        if (Money - Cost > 0)
        {
            Money -= Cost;
            return true;
        }

        else return false;
    }

    public static void AddMoney(int Cost)
    {
        Money += Cost;
    }

}
