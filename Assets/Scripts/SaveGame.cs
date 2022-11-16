using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

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
        //collectibles.starList = (File.ReadAllText(Application.persistentDataPath + "/Save.json")).Split(",");
        collectibles.starList = Array.ConvertAll(File.ReadAllText(Application.persistentDataPath + "/Save.json")).Split(","), element => bool.Parse(element));
        Debug.Log(collectibles.starList[0]);
        foreach (GameObject stars in GameObject.FindGameObjectsWithTag("Star"))
        {
            if (collectibles.starList[int.Parse(name.Substring(name.Length - 2))] == true)
            {
                stars.gameObject.SetActive(false);
            }
        }
    }
}
