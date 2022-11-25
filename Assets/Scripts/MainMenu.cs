using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI mainMenuText;
    public Button playButton;
    public Button settingsButton;
    public Button file1Button;
    public Button file2Button;
    public Button file3Button;
    public Button deleteFileButton;
    public Button copyFileButton;
    bool deleting;
    bool copying;
    int copyingFile;

    void Start()
    {
        file1Button.gameObject.SetActive(false);
        file2Button.gameObject.SetActive(false);
        file3Button.gameObject.SetActive(false);
        deleteFileButton.gameObject.SetActive(false);
        copyFileButton.gameObject.SetActive(false);
        deleting = false;
        copying = false;
        copyingFile = 0;
    }

    void Update()
    {

    }

    public void Play()
    {
        mainMenuText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        file1Button.gameObject.SetActive(true);
        file2Button.gameObject.SetActive(true);
        file3Button.gameObject.SetActive(true);
        deleteFileButton.gameObject.SetActive(true);
        copyFileButton.gameObject.SetActive(true);
    }

    public void Delete()
    {
        if (deleting == true)
        {
            deleting = false;
        }
        else
        {
            deleting = true;
        }
    }

    public void Copy()
    {
        if (copying == true)
        {
            copying = false;
        }
        else
        {
            copying = true;
        }
    }

    public void Settings()
    {
        mainMenuText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
    }

    public void LoadFile(int number)
    {
        if (File.Exists(Application.persistentDataPath + "/" + number + ".json"))
        {
            if (deleting == false & copying == false)
            {
                SaveGame.number = number;
                Collectibles.starList = (File.ReadAllText(Application.persistentDataPath + "/" + number + ".json")).Split(",");
                Collectibles.stars = 0;
                foreach (string star in Collectibles.starList)
                {
                    if (star == "1")
                    {
                        Collectibles.stars = Collectibles.stars + 1;
                    }
                }
                SceneManager.LoadScene("HubWorld");
            }
            else if (deleting == true)
            {
                File.Delete(Application.persistentDataPath + "/" + number + ".json");
            }
            else if (copying == true)
            {
                if (copyingFile == 0)
                {
                    copyingFile = number;
                }
                else
                {
                    File.WriteAllText(Application.persistentDataPath + "/" + number + ".json", File.ReadAllText(Application.persistentDataPath + "/" + copyingFile + ".json"));
                }
            }
        }
        else
        {
            if (deleting == false & copying == false)
            {
                Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                File.WriteAllText(Application.persistentDataPath + "/" + number + ".json", string.Join(",", Collectibles.starList));
                SceneManager.LoadScene("HubWorld");
            }
        }
    }
}