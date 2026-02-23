using System;
using System.Reflection.Emit;
using UnityEngine;

public static class Shop
{
    public static event Action<int> OnTransaction;

    [SerializeField]
    public static int Money = 100; 

    public static bool TakeMoney(int Cost)
    {
        if (Money - Cost >= 0)
        {
            int OldMoney = Money;
            Money -= Cost;
            OnTransaction?.Invoke(OldMoney);
            return true;
        }

        else return false;
    }

    public static void AddMoney(int Cost)
    {
        int OldMoney = Money;
        Money += Cost;
        OnTransaction?.Invoke(OldMoney);
    }

}
