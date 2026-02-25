using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void FixedUpdate()
    {
        if ((Player.Health <= 0) && (HealthBar.OldHealth <= 0)) Die();

        if ((Player.Hunger <= 0) && (HungerBar.OldHunger <= 0)) Die();
    }

    private static void Die()
    {
        Player.Reset();
        Shop.Reset();
        SceneManager.LoadScene("Ending");
    }
}
