using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{
    public static float TimePast;

    [SerializeField]
    private float time;

    void Update()
    {
        TimePast += Time.deltaTime;
        time = TimePast;
    }
}