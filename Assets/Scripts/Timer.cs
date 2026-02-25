using UnityEngine;
using UnityEngine.PlayerLoop;

public class Timer : MonoBehaviour
{
    public static float TimePast;

    [SerializeField]
    private float time;

    public void Awake()
    {
        TimePast = 0.0f;
    }
	
    void Update()
    {
        TimePast += Time.deltaTime;
        time = TimePast;
    }
}