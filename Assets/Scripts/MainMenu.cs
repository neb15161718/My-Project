using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuText;
    public GameObject playButton;
    public GameObject file1Button;
    public GameObject file2Button;
    public GameObject file3Button;
    public TextAsset saveJson;
    public GameObject allStars;

    void Start()
    {
        file1Button.SetActive(false);
        file2Button.SetActive(false);
        file3Button.SetActive(false);
    }

    void Update()
    {

    }

    public void Play()
    {
        mainMenuText.SetActive(false);
        playButton.SetActive(false);
        file1Button.SetActive(true);
        file2Button.SetActive(true);
        file3Button.SetActive(true);
    }

    public void LoadFile(int number)
    {
        SaveGame.number = number;
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + number + ".json"))
        {
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
        else
        {
            Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            File.WriteAllText(Application.persistentDataPath + "/" + number + ".json", string.Join(",", Collectibles.starList));
            SceneManager.LoadScene("HubWorld");
        }
    }
}