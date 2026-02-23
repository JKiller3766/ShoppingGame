using System;
using System.Reflection.Emit;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerChangeHealth;
    public static event Action OnTransaction;

    public static int Health = 100;
    public static int Money = 100;

    public void ModifyMoney(int Cost, bool Add)
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

    private void AddMoney(int Cost) 
    {
        if (Shop.TakeMoney(Cost))
        {
            Money += Cost;
            OnTransaction?.Invoke();
        }
    }

    private void TakeMoney(int Cost)
    {
        if (Money - Cost >= 0)
        {
            Shop.AddMoney(Cost);
            Money -= Cost;
            OnTransaction?.Invoke();
        }
    }

    public void ModifyHealth(int Quantity, bool Healing)
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

    private void Heal(int Quantity)
    {
        Health += Quantity;
        OnPlayerChangeHealth?.Invoke();
    }

    private void Damage(int Quantity)
    {
        Health -= Quantity;
        OnPlayerChangeHealth?.Invoke();
        
        if (Health <= 0)
        {
            SceneManager.LoadScene("Ending");
        }
    }

}