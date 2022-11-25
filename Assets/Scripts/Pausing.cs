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
    public Button starButton;
    public TextMeshProUGUI objectiveText1;
    public TextMeshProUGUI objectiveText2;
    public TextMeshProUGUI objectiveText3;
    Scene currentScene;
    public static Pausing Instance;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Instance = this;
    }

    void Update()
    {

    }
        
    public void Pause(InputAction.CallbackContext context)
    {
        if (paused == false & Attacking.Instance.dead == false)
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
                starButton.gameObject.SetActive(true);
            }
        }
        else if (Attacking.Instance.dead == false)
        {   
            Time.timeScale = 1;
            paused = false;
            pauseText.enabled = false;
            saveButton.gameObject.SetActive(false);
            loadButton.gameObject.SetActive(false);
            hubButton.gameObject.SetActive(false);
            starButton.gameObject.SetActive(false);
            objectiveText1.enabled = false;
            objectiveText2.enabled = false;
            objectiveText3.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DisplayStars()
    {
        pauseText.enabled = false;
        hubButton.gameObject.SetActive(false);
        starButton.gameObject.SetActive(false);
        objectiveText1.enabled = true;
        objectiveText2.enabled = true;
        objectiveText3.enabled = true;
    }
}
