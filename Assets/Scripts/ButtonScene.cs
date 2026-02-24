using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour
{
    public static void ChangeScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
