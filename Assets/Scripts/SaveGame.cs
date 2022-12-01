using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    public static int file;

    public void Save()
    {
        File.WriteAllText(Application.persistentDataPath + "/" + file + ".json", string.Join(",", Collectibles.starList));
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + file + ".json"))
        {
            Collectibles.starList = (File.ReadAllText(Application.persistentDataPath + "/" + file + ".json")).Split(",");
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
