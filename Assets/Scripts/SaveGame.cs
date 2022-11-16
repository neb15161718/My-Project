using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance;
    Collectibles collectibles;
    public TextAsset saveJson;

    void Start()
    {
        Instance = this;
        collectibles = GetComponent<Collectibles>();
    }

    void Update()
    {
        
    }

    public void Save()
    {
        File.WriteAllText(Application.persistentDataPath + "/Save.json", string.Join(",", collectibles.starList));
    }

    public void Load()
    {
        collectibles.starList = (File.ReadAllText(Application.persistentDataPath + "/Save.json")).Split(",");
        Debug.Log(collectibles.starList[0]);
        foreach (GameObject stars in GameObject.FindGameObjectsWithTag("Star"))
        {
            if (collectibles.starList[int.Parse(stars.name.Substring(stars.name.Length - 2))] == "1")
            {
                stars.gameObject.SetActive(false);
                collectibles.stars = collectibles.starList.Count(s => s == "1");
            }
        }
    }
}
