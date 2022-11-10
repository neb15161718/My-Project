using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Pausing : MonoBehaviour
{
    public bool paused = false;
    public TextMeshProUGUI pauseText;
    public Button saveButton;

    void Start()
    {
        pauseText.enabled = false;
        saveButton.gameObject.SetActive(false);
    }

    void Update()
    {

    }
        
    public void Pause(InputAction.CallbackContext context)
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            paused = true;
            pauseText.enabled = true;
            saveButton.gameObject.SetActive(true);
        }
        else
        {   
            Time.timeScale = 1;
            paused = false;
            pauseText.enabled = false;
            saveButton.gameObject.SetActive(false);
        }
    }
}
