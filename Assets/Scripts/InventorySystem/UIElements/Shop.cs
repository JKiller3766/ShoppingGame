using System;
using System.Reflection.Emit;
using UnityEngine;

public static class Shop
{
    public static event Action<int> OnTransaction;

    public static readonly int DefaultMoney = 150;
    public static int Money = DefaultMoney;


    public static bool TakeMoney(int cost)
    {
        if (Money - cost >= 0)
        {
            int OldMoney = Money;
            Money -= cost;
            OnTransaction?.Invoke(OldMoney);
            return true;
        }

        else return false;
    }

    public static void AddMoney(int cost)
    {
        int OldMoney = Money;
        Money += cost;
        OnTransaction?.Invoke(OldMoney);
    }

    public static void Reset()
    {
        Money = DefaultMoney;
    }
}
