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
    Attacking attacking;

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
        if (!paused & !attacking.dead)
        {
            Time.timeScale = 0;
            paused = true;
            pauseText.gameObject.SetActive(true);
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
        else if (!attacking.dead)
        {   
            Time.timeScale = 1;
            paused = false;
            pauseText.gameObject.SetActive(false);
            saveButton.gameObject.SetActive(false);
            loadButton.gameObject.SetActive(false);
            hubButton.gameObject.SetActive(false);
            starButton.gameObject.SetActive(false);
            objectiveText1.gameObject.SetActive(false);
            objectiveText2.gameObject.SetActive(false);
            objectiveText3.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DisplayStars()
    {
        pauseText.enabled = false;
        hubButton.gameObject.SetActive(false);
        starButton.gameObject.SetActive(false);
        objectiveText1.gameObject.SetActive(true);
        objectiveText2.gameObject.SetActive(true);
        objectiveText3.gameObject.SetActive(true);
    }
}
