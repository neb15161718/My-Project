using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    public static int number;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Save()
    {
        File.WriteAllText(Application.persistentDataPath + "/" + number + ".json", string.Join(",", Collectibles.starList));
    }

    public void Load()
    {
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
            Time.timeScale = 1;
        }
    }
}
