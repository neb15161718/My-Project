using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public Button renameFileButton;
    Button button;
    public TMP_InputField inputField;
    string[] names;
    bool deleting;
    bool copying;
    bool renaming;
    int copyingFile;
    int fileNumber;
    string fileName;

    void Start()
    {
        deleting = false;
        copying = false;
        renaming = false;
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
        renameFileButton.gameObject.SetActive(true);
        if (File.Exists(Application.persistentDataPath + "/names.json"))
        {
            names = (File.ReadAllText(Application.persistentDataPath + "/names.json")).Split(",");
            TextMeshProUGUI file1text = file1Button.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI file2text = file2Button.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI file3text = file3Button.GetComponentInChildren<TextMeshProUGUI>();
            file1text.text = names[0];
            file2text.text = names[1];
            file3text.text = names[2];

        }
        else
        {
            names = new string[] { "File 1", "File 2", "File 3" };
            File.WriteAllText(Application.persistentDataPath + "/names.json", string.Join(",", names));
        }
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
            copying = false;
            renaming = false;
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
            deleting = false;
            renaming = false;
        }
    }

    public void Rename()
    {
        if (renaming == true)
        {
            renaming = false;
        }
        else
        {
            renaming = true;
            deleting = false;
            copying = false;
        }
    }
    
    public void renameFile()
    {
        if (inputField.text != "")
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = inputField.text;
            names[fileNumber - 1] = inputField.text;
            File.WriteAllText(Application.persistentDataPath + "/names.json", string.Join(",", names));
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = fileName;
        }
        renaming = false;
    }

    public void Settings()
    {
        mainMenuText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
    }

    public void LoadFile(int number)
    {
        fileNumber = number;
        if (number == 1)
        {
            button = file1Button;
        }
        else if (number == 2)
        {
            button = file2Button;
        }
        else if (number == 3)
        {
            button = file3Button;
        }
        if (File.Exists(Application.persistentDataPath + "/" + number + ".json"))
        {
            if (deleting == false & copying == false & renaming == false)
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
                deleting = false;
            }
            else if (renaming == true)
            {
                fileName = button.GetComponentInChildren<TextMeshProUGUI>().text;
                inputField.gameObject.SetActive(true);
                inputField.transform.position = button.transform.position;
                inputField.textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        else
        {
            if (deleting == false & copying == false & renaming == false)
            {
                Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                File.WriteAllText(Application.persistentDataPath + "/" + number + ".json", string.Join(",", Collectibles.starList));
                SceneManager.LoadScene("HubWorld");
            }
        }
        if (copying == true)
        {
            if (copyingFile == 0)
            {
                if (File.Exists(Application.persistentDataPath + "/" + number + ".json"))
                {
                    copyingFile = number;
                }
            }
            else
            {
                File.WriteAllText(Application.persistentDataPath + "/" + number + ".json", File.ReadAllText(Application.persistentDataPath + "/" + copyingFile + ".json"));
                copyingFile = 0;
                copying = false;
            }
        }
    }
}