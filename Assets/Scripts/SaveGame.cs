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
    public GameObject allStars;

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
        collectibles.stars = 0;
        Transform[] starList = allStars.gameObject.GetComponentsInChildren<Transform>(true);
        foreach(Transform stars in starList)
        {
            if (stars.gameObject.name != ("Stars"))
            {
                stars.gameObject.SetActive(true);
                if (collectibles.starList[int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))] == "1")
                {
                    stars.gameObject.SetActive(false);
                    collectibles.stars = collectibles.stars + 1;
                }
            }
        }
    }
}
