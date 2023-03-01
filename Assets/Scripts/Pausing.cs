using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Pausing : MonoBehaviour
{
    public bool paused;
    int levelDisplaying;
    public TextMeshProUGUI pauseText;
    public Button saveButton;
    public Button loadButton;
    public Button menuButton;
    public Button hubButton;
    public Button starButton;
    public Button nextButton;
    public Button backButton;
    public TextMeshProUGUI objectiveText1;
    public TextMeshProUGUI objectiveText2;
    public TextMeshProUGUI objectiveText3;
    Scene currentScene;
    public static Pausing Instance;
    Attacking attacking;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        paused = false;
        attacking = GetComponent<Attacking>();
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
                menuButton.gameObject.SetActive(true);
                starButton.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -200);
                starButton.gameObject.SetActive(true);
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
            menuButton.gameObject.SetActive(false);
            hubButton.gameObject.SetActive(false);
            starButton.gameObject.SetActive(false);
            objectiveText1.gameObject.SetActive(false);
            objectiveText2.gameObject.SetActive(false);
            objectiveText3.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DisplayStars()
    {
        pauseText.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        loadButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        hubButton.gameObject.SetActive(false);
        starButton.gameObject.SetActive(false);
        objectiveText1.gameObject.SetActive(true);
        objectiveText2.gameObject.SetActive(true);
        objectiveText3.gameObject.SetActive(true);
        if (SceneManager.GetActiveScene().name == "HubWorld")
        {
            nextButton.gameObject.SetActive(true);
            levelDisplaying = 0;
            ChangeLevelDisplayed();
        }
    }

    public void Next()
    {
        levelDisplaying += 1;
        ChangeLevelDisplayed();
        if (levelDisplaying > 0)
        {
            backButton.gameObject.SetActive(true);
        }
    }

    public void Back()
    {
        levelDisplaying -= 1;
        ChangeLevelDisplayed();
        if (levelDisplaying == 0)
        {
            backButton.gameObject.SetActive(false);
        }
    }

    void ChangeLevelDisplayed()
    {
        if (levelDisplaying == 0)
        {
            objectiveText1.text = "1 - Grunts";
            objectiveText2.text = "2 - Steps";
            objectiveText3.text = "3 - Jump";
            int i = 0;
            foreach (string stars in Collectibles.starList)
            {
                if (Collectibles.starList != null)
                {
                    if (Collectibles.starList[i] == "1")
                    {
                        if (i == 0)
                        {
                            objectiveText1.SetText("\u2713 " + objectiveText1.text);
                        }
                        if (i == 1)
                        {
                            objectiveText2.SetText("\u2713 " + objectiveText2.text);
                        }
                        if (i == 2)
                        {
                            objectiveText3.SetText("\u2713 " + objectiveText3.text);
                        }
                    }
                }
                i++;
            }
        }
        else if (levelDisplaying == 1)
        {
            objectiveText1.text = "1 - Diamonds";
            objectiveText2.text = "2 - Mummies";
            objectiveText3.text = "3 - Platforms";
            int i = 0;
            foreach (string stars in Collectibles.starList)
            {
                if (Collectibles.starList != null)
                {
                    if (Collectibles.starList[i] == "1")
                    {
                        if (i == 3)
                        {
                            objectiveText1.SetText("\u2713 " + objectiveText1.text);
                        }
                        if (i == 4)
                        {
                            objectiveText2.SetText("\u2713 " + objectiveText2.text);
                        }
                        if (i == 5)
                        {
                            objectiveText3.SetText("\u2713 " + objectiveText3.text);
                        }
                    }
                }
                i++;
            }
        }
        else if (levelDisplaying == 2)
        {
            objectiveText1.text = "1 - Soldiers";
            objectiveText2.text = "2 - ";
            objectiveText3.text = "3 - ";
            int i = 0;
            foreach (string stars in Collectibles.starList)
            {
                if (Collectibles.starList != null)
                {
                    if (Collectibles.starList[i] == "1")
                    {
                        if (i == 3)
                        {
                            objectiveText1.SetText("\u2713 " + objectiveText1.text);
                        }
                        if (i == 4)
                        {
                            objectiveText2.SetText("\u2713 " + objectiveText2.text);
                        }
                        if (i == 5)
                        {
                            objectiveText3.SetText("\u2713 " + objectiveText3.text);
                        }
                    }
                }
                i++;
            }
        }
    }

    public void ToHub()
    {
        SceneManager.LoadScene("HubWorld");
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
