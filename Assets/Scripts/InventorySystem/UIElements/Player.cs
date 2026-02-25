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

    [SerializeField]
    public static readonly int MaxHealth = 100;
    public static int Health = MaxHealth;

    public static readonly int MaxHunger = 100;
    public static int Hunger = MaxHealth;

    public static readonly int DefaultMoney = 100;
    public static int Money = DefaultMoney;

    public static void ModifyMoney(int Cost, bool Add)
    {
        if (Add)
        {
            AddMoney(Cost);
        }

        else
        {
            TakeMoney(Cost);
        }
    }

    private static void AddMoney(int Cost) 
    {
        if (Shop.TakeMoney(Cost))
        {
            int OldMoney = Money;
            Money += Cost;
            OnTransaction?.Invoke(OldMoney);
        }
    }

    private static void TakeMoney(int Cost)
    {
        if (Money - Cost >= 0)
        {
            Shop.AddMoney(Cost);
            int OldMoney = Money;
            Money -= Cost;
            OnTransaction?.Invoke(OldMoney);
        }
    }

    public static void ModifyHealth(int Quantity, bool Healing)
    {
        if (Healing)
        {
            Heal(Quantity);
        }

        else
        {
            Damage(Quantity);
        }
    }

    private static void Heal(int Quantity)
    {
        int OldHealth = Health;
        Health += Quantity;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        OnPlayerChangeHealth?.Invoke();
    }

    private static void Damage(int Quantity)
    {
        int OldHealth = Health;
        Health -= Quantity;
        OnPlayerChangeHealth?.Invoke();
        
        if (Health <= 0)
        {
            Die();
        }
    }
    public static void ModifyHunger(int Quantity, bool Eating)
    {
        if (Eating)
        {
            Eat(Quantity);
        }

        else
        {
            Starve(Quantity);
        }
    }

    private static void Eat(int Quantity)
    {
        int OldHunger = Hunger;
        Hunger += Quantity;
        if (Hunger > MaxHunger)
        {
            Hunger = MaxHunger;
        }
        OnPlayerChangeHunger?.Invoke();
    }

    private static void Starve(int Quantity)
    {
        int OldHunger = Hunger;
        Hunger -= Quantity;
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