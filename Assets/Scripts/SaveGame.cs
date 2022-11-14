using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
        File.WriteAllText(Application.persistentDataPath + "/Save.json", collectibles.stars.ToString());
    }

    public void Load()
    {
        collectibles.stars = int.Parse(File.ReadAllText(Application.persistentDataPath + "/Save.json"));
    }
}
