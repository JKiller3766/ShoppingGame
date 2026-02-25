using System;
using System.Reflection.Emit;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Player
{
    public static event Action<int> OnPlayerChangeHealth;
    public static event Action OnPlayerDamage;

    public static event Action<int> OnPlayerChangeHunger;
    public static event Action OnPlayerStarve;

    public static event Action<int> OnTransaction;

    public static readonly int MaxHealth = 100;
    public static int Health = MaxHealth;

    public static readonly int MaxHunger = 100;
    public static int Hunger = MaxHealth;

    public static readonly int DefaultMoney = 100;
    public static int Money = DefaultMoney;

    public static void ModifyMoney(int cost, bool adding)
    {
        if (adding)
        {
            AddMoney(cost);
        }
        else
        {
            TakeMoney(cost);
        }
    }

    private static void AddMoney(int cost) 
    {
        if (Shop.TakeMoney(cost))
        {
            int oldMoney = Money;
            Money += cost;

            OnTransaction?.Invoke(oldMoney);
        }
    }

    private static void TakeMoney(int cost)
    {
        if (Money - cost >= 0)
        {
            Shop.AddMoney(cost);

            int oldMoney = Money;
            Money -= cost;

            OnTransaction?.Invoke(oldMoney);
        }
    }

    public static void ModifyHealth(int quantity, bool healing)
    {
        if (healing)
        {
            Heal(quantity);
        }
        else
        {
            Damage(quantity);
        }
    }

    private static void Heal(int quantity)
    {
        int oldHealth = Health;
        Health += quantity;
		
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
		
        OnPlayerChangeHealth?.Invoke(oldHealth);
    }

    private static void Damage(int quantity)
    {
        int oldHealth = Health;
        Health -= quantity;

        OnPlayerChangeHealth?.Invoke(oldHealth);
        OnPlayerDamage?.Invoke();
    }
	
    public static void ModifyHunger(int quantity, bool eating)
    {
        if (eating)
        {
            Eat(quantity);
        }
        else
        {
            Starve(quantity);
        }
    }

    private static void Eat(int quantity)
    {
        int oldHunger = Hunger;
        Hunger += quantity;
		
        OnPlayerChangeHunger?.Invoke(oldHunger);
    }

    private static void Starve(int quantity)
    {
        int oldHunger = Hunger;
        Hunger -= quantity;

        OnPlayerChangeHunger?.Invoke(oldHunger);
        OnPlayerStarve?.Invoke();
    }

    public static void Reset()
    {
        Money = DefaultMoney;
        Health = MaxHealth;
        Hunger = MaxHunger;
    }
}