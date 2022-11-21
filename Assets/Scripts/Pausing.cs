using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Pausing : MonoBehaviour
{
    public bool paused = false;
    public TextMeshProUGUI pauseText;
    public Button saveButton;
    public Button loadButton;
    public Button hubButton;
    Scene currentScene;
    void Start()
    {
        pauseText.enabled = false;
        saveButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
        hubButton.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
            Cursor.lockState = CursorLockMode.None;
            currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "HubWorld")
            {
                saveButton.gameObject.SetActive(true);
                loadButton.gameObject.SetActive(true);
            }
            else
            {
                hubButton.gameObject.SetActive(true);
            }
        }
        else
        {   
            Time.timeScale = 1;
            paused = false;
            pauseText.enabled = false;
            saveButton.gameObject.SetActive(false);
            loadButton.gameObject.SetActive(false);
            hubButton.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
