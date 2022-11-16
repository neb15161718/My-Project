using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubButton : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ToHub()
    {
        SceneManager.LoadScene("HubWorld");
        Time.timeScale = 1;
    }
}
