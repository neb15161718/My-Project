using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainPlainsLoad : MonoBehaviour
{
    public GameObject allStars;

    void Start()
    {
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            stars.gameObject.SetActive(true);
            if (Collectibles.starList != null)
            {
                if (Collectibles.starList[int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))] == "1")
                {
                    stars.gameObject.SetActive(false);
                }
            }
        }
        if (Collectibles.starList == null)
        {
            Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        }
    }


    void Update()
    {
        
    }
}
