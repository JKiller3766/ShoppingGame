using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource MusicSource;
    public AudioClip Gameplay;

    private void Awake()
    {
        MusicSource.clip = Gameplay;
        MusicSource.Play();
    }
}