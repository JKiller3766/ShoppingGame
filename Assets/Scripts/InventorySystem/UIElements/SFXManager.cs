using System.Reflection;
using UnityEditor.Build.Content;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource SFXSource;
    public AudioClip SelectionSound;
    public AudioClip TransactionSound;
    public AudioClip ConsumableSound;
    public AudioClip DamageSound;
    public AudioClip StarvingSound;

    private void OnEnable()
    {
        ShoppingButtons.OnConsumableUse += PlayConsumableSound;
        ItemSlotUI.OnSelect += PlaySelectionSound;
        Player.OnTransaction += PlayTransactionSound;
        Player.OnPlayerDamage += PlayDamageSound;
        Player.OnPlayerStarve += PlayStarvingSound;

    }

    private void OnDisable()
    {
        ShoppingButtons.OnConsumableUse -= PlayConsumableSound;
        ItemSlotUI.OnSelect -= PlaySelectionSound;
        Player.OnTransaction -= PlayTransactionSound;
        Player.OnPlayerDamage -= PlayDamageSound;
        Player.OnPlayerStarve -= PlayStarvingSound;

    }
    public void PlaySelectionSound()
    {
        SFXSource.PlayOneShot(SelectionSound);
    }
    public void PlayTransactionSound(int oldMoney)
    {
        SFXSource.PlayOneShot(TransactionSound);
    }
    public void PlayDamageSound()
    {
        SFXSource.PlayOneShot(DamageSound);
    }

    public void PlayStarvingSound()
    {
        SFXSource.PlayOneShot(StarvingSound);
    }

    public void PlayConsumableSound()
    {
        SFXSource.PlayOneShot(ConsumableSound);
    }
}
