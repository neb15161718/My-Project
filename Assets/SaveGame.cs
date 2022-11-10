using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void Save()
    {
        Debug.Log("H");
    }
}
