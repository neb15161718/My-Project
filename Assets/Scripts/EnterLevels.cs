using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLevels : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlainPlainsEntrance")
        {
            SceneManager.LoadScene("PlainPlains");
        }
        else if (other.gameObject.name == "CloudyCloudsEntrance")
        {
            SceneManager.LoadScene("CloudyClouds");
        }
    }
}
