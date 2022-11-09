using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public static Collectibles Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    public void AddStar()
    {
        Debug.Log("H");
    }
}
