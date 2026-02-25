using System;
using System.Reflection.Emit;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Player
{
    public static event Action OnPlayerChangeHealth;
    public static event Action OnPlayerChangeHunger;
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
            int OldMoney = Money;
            Money += cost;
            OnTransaction?.Invoke(OldMoney);
        }
    }

    private static void TakeMoney(int cost)
    {
        if (Money - cost >= 0)
        {
            Shop.AddMoney(cost);
            int OldMoney = Money;
            Money -= cost;
            OnTransaction?.Invoke(OldMoney);
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
        int OldHealth = Health;
        Health += quantity;
		
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
		
        OnPlayerChangeHealth?.Invoke();
    }

    private static void Damage(int quantity)
    {
        int OldHealth = Health;
        Health -= quantity;
        OnPlayerChangeHealth?.Invoke();
        
        if (Health <= 0)
        {
            Die();
        }
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
        int OldHunger = Hunger;
        Hunger += quantity;
		
        if (Hunger > quantity)
        {
            Hunger = MaxHunger;
        }
		
        OnPlayerChangeHunger?.Invoke();
    }

    private static void Starve(int quantity)
    {
        int OldHunger = Hunger;
        Hunger -= quantity;
        OnPlayerChangeHunger?.Invoke();

        if (Hunger <= 0)
        {
            Die();
        }
    }

    public static void Reset()
    {
        Money = DefaultMoney;
        Health = MaxHealth;
        Hunger = MaxHunger;
    }

    private static void Die()
    {
        Reset();
        Shop.Reset();
        SceneManager.LoadScene("Ending");
    }
}