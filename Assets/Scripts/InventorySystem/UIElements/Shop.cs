using System;
using System.Reflection.Emit;
using UnityEngine;

public static class Shop
{
    public static event Action<int> OnTransaction;

    public static readonly int DefaultMoney = 100;
    [SerializeField]
    public static int Money = DefaultMoney;
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

    public static void Reset()
    {
        Money = DefaultMoney;
    }
}
