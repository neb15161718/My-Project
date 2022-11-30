using UnityEngine;
using UnityEngine.SceneManagement;

public class HubButton : MonoBehaviour
{
    public void ToHub()
    {
        SceneManager.LoadScene("HubWorld");
        Time.timeScale = 1;
    }
}
